using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;

using ItemListLivet.Models;
using ItemListLivet.Model;
using System.Windows.Data;
using System.Windows;
using ItemListLivet.Views;

namespace ItemListLivet.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        public MainWindowViewModel()
        {
            init();
        }
        private void init()
        {
            dt = DateTime.Now;
            items = new Items();
            SearchCategory = "";
        }

        private Items items;
        public Items Items
        {
            get
            {
                return this.items;
            }
            set
            {
                this.items = value;
                RaisePropertyChanged("Items");
            }
        }

        private Item item;
        public Item Item
        {
            get
            {
                return this.item;
            }
            set
            {
                this.item = value;
                RaisePropertyChanged("Item");
                this.OpenSubWindowCommand.RaiseCanExecuteChanged();
            }
        }

        private string searchStr;
        public String SearchStr
        {
            get
            {
                return this.searchStr;
            }

            set
            {
                this.searchStr = value;
                searchItems();
                RaisePropertyChanged("SearchStr");
            }
        }
        private String searchCategory;
        public String SearchCategory
        {
            get
            {
                return this.searchCategory;
            }
            set
            {
                this.searchCategory = value;
                searchItems();
                RaisePropertyChanged("SearchCategory");
            }

        }
        private DateTime dt;
        public DateTime Dt
        {
            get
            {
                return this.dt;
            }
            set
            {
                this.dt = value;
                RaisePropertyChanged("Dt");
            }
        }

        /// <summary>
        /// viewにバインドするカテゴリのリスト
        /// </summary>
        public List<Category> Categorys
        {
            get
            {
                return Item.getCategoryList();
            }
        }
        public List<String> CategoryStrs
        {
            get
            {
                return Item.getCategoryStrList(); // 検索用(選択なし("")を含む)
            }
        }





        //コマンド

        /// <summary>
        /// 行追加コマンド
        /// </summary>
        public ViewModelCommand AddRowCommand
        {
            get
            {
                return new ViewModelCommand(addRow, canAddRow);
            }
        }

        private void addRow()
        {
            this.Items.ItemList.Add(new Item());
            this.SearchStr = "";
            this.SearchCategory = "";
            //var collectionView = CollectionViewSource.GetDefaultView(this.Items.ItemList);
            //collectionView.Filter = x => { return true; };
        }
        private bool canAddRow()
        {
            return this.Items.ItemList.Count < 30;
        }

        /// <summary>
        /// 値上げコマンド
        /// </summary>
        public ViewModelCommand PriceUpCommand
        {
            get
            {
                return new ViewModelCommand(priceUp);
            }
        }
        private void priceUp()
        {
            foreach (var item in this.Items.ItemList)
            {
                item.Price = (int)Math.Floor(item.Price * 1.1);
            }
        }

        /// <summary>
        /// 日付の更新コマンド
        /// </summary>
        public ViewModelCommand RefreshDtCommand
        {
            get
            {
                return new ViewModelCommand(refreshDt);
            }
        }
        private void refreshDt()
        {
            this.Dt = DateTime.Now;
        }


        /// <summary>
        /// 商品名でフィルタするコマンド
        /// </summary>
        public ViewModelCommand SearchItemNameCommand
        {
            get
            {
                return new ViewModelCommand(searchItems);
            }
        }
        private void searchItems()
        {
            var collectionView = CollectionViewSource.GetDefaultView(this.Items.ItemList);
            collectionView.Filter = x =>
            {
                var item = (Item)x;
                bool checkItemName;
                checkItemName = SearchStr == null || item.ItemName == null ? true : item.ItemName.Contains(SearchStr);

                bool checkCategory;
                if (searchCategory.Equals(""))
                {
                    checkCategory = true;
                }
                else
                {
                    Category c = (Category)Enum.Parse(typeof(Category), searchCategory);
                    checkCategory = item == null ? true : c.Equals(item.Category);
                }

                return checkItemName && checkCategory;
            };
        }

        /// <summary>
        /// チェックが入っている行を削除するコマンド
        /// </summary>
        public ViewModelCommand DeleteRowCommand
        {
            get
            {
                return new ViewModelCommand(deleteItem);
            }
        }
        private void deleteItem()
        {
            Console.WriteLine("deleteRow");
            // 削除フラグがたっているものを削除します
            //foreach (Item item in this.Items.ItemList.Where(d => d.DeleteFlg).ToList())
            //{
            //    Items.ItemList.Remove(item);
            //}

            if (MessageBox.Show("チェックされている行を削除します。", "キャプション", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }
            Items.removeItems();
        }

        /// <summary>
        /// 意味もなくメッセージを表示するコマンド
        /// </summary>
        public ViewModelCommand ShowMsg
        {
            get
            {
                return new ViewModelCommand(showMsg);
            }
        }
        private void showMsg()
        {
            Console.WriteLine("showMsg");
            MessageBox.Show("hello！", "キャプション");
        }

        /// <summary>
        /// サブウィンドウを開く
        /// </summary>
        private ViewModelCommand openSubWindowCommand;
        public ViewModelCommand OpenSubWindowCommand
        {
            get
            {
                if(openSubWindowCommand == null)
                {
                    openSubWindowCommand = new ViewModelCommand(openSubWindow, CanEdit);
                }
                return openSubWindowCommand;
            }
        }

        private void openSubWindow()
        {
            var win = new SubWindow();

            // IsSelectの行を取得(DatagridのSelectionModeがsingleである前提)
            var index = this.Items.ItemList.Select((d, i) => new { Index = i, Item = d }).Where(d => d.Item.IsSelected).Select(i => i.Index).ToList();
            if (this.Item == null)
            {
                MessageBox.Show("行が選択されていません。", "キャプション");
                return;
            }
            

            using (var vm = new SubWindowViewModel(this.Item))
            {
                Messenger.Raise(new TransitionMessage(vm, "EditCommand"));
            }

        }
        public bool CanEdit()
        {
            return this.Item != null;
        }

    }
}
