using System;
using System.Collections.Generic;
using System.Linq;

using Livet;
using Livet.Commands;
using Livet.Messaging;
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
            Dt = DateTime.Now;
            Items = new Items();
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

        private Item selectedItem;
        public Item SelectedItem
        {
            get
            {
                return this.selectedItem;
            }
            set
            {
                this.selectedItem = value;
                RaisePropertyChanged("Item");
                this.OpenEditWindowCommand.RaiseCanExecuteChanged();
                this.DeleteItemCommand.RaiseCanExecuteChanged();
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
        /// データを再検索してリセットするコマンド
        /// </summary>
        public ViewModelCommand ReSearchCommand
        {
            get
            {
                return new ViewModelCommand(() => Items = new Items());
            }
        }


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
        /// 編集用サブウィンドウを開く
        /// </summary>
        private ViewModelCommand openEditWindowCommand;
        public ViewModelCommand OpenEditWindowCommand
        {
            get
            {
                if (openEditWindowCommand == null)
                {
                    openEditWindowCommand = new ViewModelCommand(openEditWindow, CanEdit);
                }
                return openEditWindowCommand;
            }
        }

        private void openEditWindow()
        {

            // IsSelectの行を取得(DatagridのSelectionModeがsingleである前提)
            var index = this.Items.ItemList.Select((d, i) => new { Index = i, Item = d }).Where(d => d.Item.IsSelected).Select(i => i.Index).ToList();
            if (this.SelectedItem == null)
            {
                MessageBox.Show("行が選択されていません。", "キャプション");
                return;
            }

            using (var vm = new SubWindowViewModel(this.SelectedItem.Id))
            {
                Messenger.Raise(new TransitionMessage(vm, TransitionMode.Modal, "EditCommand"));
                init();
            }
        }

        public bool CanEdit()
        {
            return this.SelectedItem != null;
        }

        /// <summary>
        /// 新規用サブウィンドウを開く
        /// </summary>
        private ViewModelCommand openNewWindowCommand;
        public ViewModelCommand OpenNewWindowCommand
        {
            get
            {
                if (openNewWindowCommand == null)
                {
                    openNewWindowCommand = new ViewModelCommand(openNewWindow);
                }
                return openNewWindowCommand;
            }
        }

        private void openNewWindow()
        {

            using (var vm = new SubWindowViewModel())
            {
                Messenger.Raise(new TransitionMessage(vm, TransitionMode.Modal, "EditCommand"));
                init();
            }
        }

        /// <summary>
        /// 選択行を削除する
        /// </summary>
        private ViewModelCommand deleteItemCommand;
        public ViewModelCommand DeleteItemCommand
        {
            get
            {
                if (deleteItemCommand == null)
                {
                    deleteItemCommand = new ViewModelCommand(deleteItem, CanEdit);
                }
                return deleteItemCommand;
            }
        }

        private void deleteItem()
        {

            Item.deleteItem(SelectedItem.Id);
            SelectedItem = null;
            init();

        }


        public ViewModelCommand ClosedCommand
        {
            get
            {
                return new ViewModelCommand(() => MessageBox.Show("Closedイベントをviewmodelで検知しました。"));
            }
        }

    }
}
