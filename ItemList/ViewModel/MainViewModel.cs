namespace ItemList.ViewModel
{
    using System.Collections.ObjectModel;
    using ItemList.Common;
    using ItemList.Model;
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using System.ComponentModel;
    using System.Windows.Data;
    using System.Windows;

    /// <summary> 
    /// MainViewのViewModel 
    /// </summary> 
    public class MainViewModel : ViewModelBase
    {


        public MainViewModel()
        {
            items = new ObservableCollection<Item>(Item.getItemList());
            dt = DateTime.Now;
            totalPrice = items.Select(d => d.Price).Sum();
            SearchCategory = Category.食品;
        }

        private string searchStr;
        public String SearchStr {
            get
            {
                return this.searchStr;
            }

            set
            {
                this.searchStr = value;
                RaisePropertyChanged("SearchStr");
            }
        }
        public Category SearchCategory { get; set; }

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

        private int totalPrice;
        public int TotalPrice
        {
            get
            {
                return items.Select(d => d.Price).Sum();
            }
            set
            {
                this.totalPrice = value;
                RaisePropertyChanged("TotalPrice");
            }
        }

        /// <summary>
        /// viewにバインドする商品一覧
        /// </summary>
        private ObservableCollection<Item> items;
        // INotifyCollectionChangedインターフェースを実装したObservableCollectionを使う。
        // コレクションの変更(追加、削除)の変更は検知するが、コレクションの要素までは検知されない。
        public ObservableCollection<Item> Items
        {
            get
            {
                return this.items;
            }

            set
            {
                this.items = value;
                this.TotalPrice = Items.Select(d => d.Price).Sum();
                RaisePropertyChanged("Items");
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
            this.Items.Add(new Item());
            var collectionView = CollectionViewSource.GetDefaultView(this.Items);
            collectionView.Filter = x => { return true; };
        }
        private bool canAddRow()
        {
            return this.Items.Count < 15;
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
            foreach (var item in this.Items)
            {
                item.Price = (int)Math.Floor(item.Price * 1.1);
            }
            reCalc();
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
            //this.Items = new ObservableCollection<Item>( items.Where(i => i.ItemName.Contains("hoge")));
            var collectionView = CollectionViewSource.GetDefaultView(this.Items);
            collectionView.Filter = x =>
            {
                var item = (Item)x;
                bool check = SearchStr == null || item.ItemName == null ? true : item.ItemName.Contains(SearchStr);
                bool check2 = item == null ? true : SearchCategory.Equals(item.Category);
                return check && check2;
            };
            this.TotalPrice = Items.Select(d => d.Price).Sum();
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
            System.Diagnostics.Debug.WriteLine("showMsg");
            MessageBox.Show("hello！", "キャプション");
        }


        public DelegateCommand ReCalcCommand
        {
            get
            {
                return new DelegateCommand(reCalc);
            }
        }
        private void reCalc()
        {
            System.Diagnostics.Debug.WriteLine("reCalc");
            this.TotalPrice = Items.Select(d => d.Price).Sum();
        }



    }
}