using System;
using System.Collections.Generic;
using ItemList.Common;

namespace ItemList.Model
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
        public string ItemName { get; set; }
        public Category category;
        public Category Category {
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
        public string CreateUser { get; set; }
        public bool DeleteFlg { get; set; }
        
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
                new Item("hoge", Category.未分類, 2, "ほげ"),
                new Item("hoge", Category.未分類, 3, "ほげ"),
                new Item("hogehoge", Category.未分類, 10, "ほげ"),
                new Item("ABC", Category.生活用品, 300, "小林"),
                new Item("BBB", Category.生活用品, 300, "小林"),
                new Item("CCC", Category.雑貨, 400, "こばやし"),

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

    }

    public class CategoryName
    {
        public string label { get; set; }
        public Category value { get; set; }

    }





}
