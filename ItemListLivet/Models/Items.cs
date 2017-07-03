using Livet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ItemListLivet.Model
{
    public class Items : NotificationObject
    {

        public Items()
        {
            itemList = new ObservableCollection<Item>(Item.getItemList());
            Init_Item_PropertyChanged();
            itemList.CollectionChanged += ItemList_CollectionChanged;
        }

        private void ItemList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Replace)
            {
                foreach (Item item in e.OldItems)
                    item.PropertyChanged -= Item_PropertyChanged;
                foreach (Item item in e.NewItems)
                    item.PropertyChanged += Item_PropertyChanged;
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Item item in e.NewItems)
                    item.PropertyChanged += Item_PropertyChanged;
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (Item item in e.OldItems)
                    item.PropertyChanged -= Item_PropertyChanged;
            }
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            reCalc();
        }

        private void reCalc()
        {
            this.TotalPrice = itemList.Select(d => d.Price).Sum();
        }

        private void Init_Item_PropertyChanged()
        {
            foreach(Item item in ItemList)
            {
                item.PropertyChanged += Item_PropertyChanged;
            }
        }

        private ObservableCollection<Item> itemList;
        public ObservableCollection<Item> ItemList
        {
            get
            {
                return this.itemList;
            }

            set
            {
                this.itemList = value;
                this.TotalPrice = ItemList.Select(d => d.Price).Sum();
                //RaisePropertyChanged("Items");
            }
        }
        private int totalPrice;
        public int TotalPrice
        {
            get
            {
                return itemList.Select(d => d.Price).Sum();
            }
            set
            {
                this.totalPrice = value;
                RaisePropertyChanged("TotalPrice");
            }
        }

        public void removeItems()
        {
            // 削除フラグがたっているものを削除します
            foreach (Item item in this.ItemList.Where(d => d.DeleteFlg).ToList())
            {
                ItemList.Remove(item);
            }
            reCalc();
        }


    }
}
