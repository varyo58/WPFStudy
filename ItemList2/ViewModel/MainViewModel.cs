namespace ItemList2.ViewModel
{
    using System.Collections.ObjectModel;
    using ItemList2.Common;
    using ItemList2.Model;
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using System.ComponentModel;
    using System.Windows.Data;
    using System.Windows;
    using View;

    /// <summary> 
    /// MainViewのViewModel 
    /// </summary> 
    public class MainViewModel : ViewModelBase
    {


        public MainViewModel()
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
        public DelegateCommand AddRowCommand
        {
            get
            {
                return new DelegateCommand(addRow, canAddRow);
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
        public DelegateCommand PriceUpCommand
        {
            get
            {
                return new DelegateCommand(priceUp);
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
        public DelegateCommand RefreshDtCommand
        {
            get
            {
                return new DelegateCommand(refreshDt);
            }
        }
        private void refreshDt()
        {
            this.Dt = DateTime.Now;
        }


        /// <summary>
        /// 商品名でフィルタするコマンド
        /// </summary>
        public DelegateCommand SearchItemNameCommand
        {
            get
            {
                return new DelegateCommand(searchItems);
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
        public DelegateCommand DeleteRowCommand
        {
            get
            {
                return new DelegateCommand(deleteItem);
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

            if(MessageBox.Show("チェックされている行を削除します。", "キャプション", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }
            Items.removeItems();
        }

        /// <summary>
        /// 意味もなくメッセージを表示するコマンド
        /// </summary>
        public DelegateCommand ShowMsg
        {
            get
            {
                return new DelegateCommand(showMsg);
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
        public DelegateCommand OpenSubWindowCommand
        {
            get
            {
                return new DelegateCommand(openSubWindow);
            }
        }
        private void openSubWindow()
        {
            var win = new SubView1();

            // IsSelectの行を取得(DatagridのSelectionModeがsingleである前提)
            var index = this.Items.ItemList.Select((d, i) => new { Index = i, Item = d }).Where(d => d.Item.IsSelected).Select(i => i.Index).ToList();
            if (index.Count == 0)
            {
                MessageBox.Show("行が選択されていません。", "キャプション");

            }
            else if (index.Count > 1)
            {
                MessageBox.Show("行が複数選択されています。", "キャプション");
            }
            else
            {
                win.DataContext = new SubViewModel1(this.Items.ItemList[index[0]]);
                win.Show();
            }
        }

        public DelegateCommand OpenSubWindow2Command
        {
            get
            {
                return new DelegateCommand(openSubWindow2);
            }
        }
        private void openSubWindow2()
        {
            var win = new SubView2();

            var subViewModel = new SubViewModel2();
            win.DataContext = subViewModel;
            win.ShowDialog();
            if (subViewModel.Item != null)
            {
                this.Items.ItemList.Add(subViewModel.Item);
            }
        }
    }
}