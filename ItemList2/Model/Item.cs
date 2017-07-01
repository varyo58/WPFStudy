using System;
using System.Collections.Generic;
using ItemList2.Common;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Specialized;
using System.ComponentModel;

namespace ItemList2.Model
{

    public enum Category
    {
        未分類,
        食品,
        生活用品,
        雑貨,
    }

    public class Item : ViewModelBase
    {
        private string itemName;
        public string ItemName {
            get
            {
                return this.itemName;
            }
            set
            {
                this.itemName = value;
                RaisePropertyChanged("ItemName");
            }
        }

        public Category category;
        public Category Category
        {
            get
            {
                return this.category;
            }

            set
            {
                this.category = value;
                RaisePropertyChanged("Category");
            }

        }

        //public int Price { get; set; }
        private int price;
        public int Price
        {
            get
            {
                return this.price;
            }
            set
            {
                this.price = value;
                RaisePropertyChanged("Price");
            }
        }
        private string createUser;
        public string CreateUser
        {
            get
            {
                return this.createUser;
            }
            set
            {
                this.createUser = value;
                RaisePropertyChanged("CreateUser");
            }
        }
        private bool deleteFlg;
        public bool DeleteFlg
        {
            get
            {
                return this.deleteFlg;
            }
            set
            {
                deleteFlg = value;
                RaisePropertyChanged("DeleteFlg");
            }
        }
        private bool isSelected;
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                isSelected = value;
                RaisePropertyChanged("IsSelected");
            }
        }
        public Item()
        {

        }
        public Item(String itemName, Category category, int price, String createUser)
        {
            this.ItemName = itemName;
            this.Category = category;
            this.Price = price;
            this.CreateUser = createUser;
            this.DeleteFlg = false;
        }


        public static List<Item> getItemList()
        {
            var list = new List<Item>
            {
                new Item("商品１", Category.生活用品, 100, "小林"),
                new Item("商品２", Category.食品, 200, "小林"),
                new Item("商品３", Category.生活用品, 300, "小林"),
                new Item("hoge", Category.未分類, 1, "ほげ"),
                new Item("hoge", Category.雑貨, 2, "ほげ"),
                new Item("hoge", Category.未分類, 3, "ほげ"),
                new Item("hogehoge", Category.未分類, 10, "ほげ"),
                new Item("ABC", Category.生活用品, 300, "小林"),
                new Item("BBB", Category.生活用品, 300, "小林"),
                new Item("CCC", Category.雑貨, 400, "こばやし"),
                new Item("商品A", Category.生活用品, 300, "香川"),
                new Item("商品B", Category.雑貨, 400, "徳島"),
                new Item("商品C", Category.雑貨, 500, "愛媛"),
                new Item("商品D", Category.食品, 600, "高知"),

            };
            return list;
        }

        public static List<Category> getCategoryList()
        {

            var list = new List<Category>();

            foreach (Category c in Enum.GetValues(typeof(Category)))
            {
                list.Add(c);
            }

            return list;

        }
        public static List<String> getCategoryStrList()
        {
            var list = new List<String>();

            list.Add("");
            foreach (Category c in Enum.GetValues(typeof(Category)))
            {
                list.Add(c.ToString());
            }

            return list;

        }

    }

    public class CategoryName
    {
        public string label { get; set; }
        public Category value { get; set; }

    }





}
