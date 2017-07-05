using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Specialized;
using System.ComponentModel;
using Livet;
using System.Data.SqlClient;

namespace ItemListLivet.Model
{

    public enum Category
    {
        未分類,
        食品,
        生活用品,
        雑貨,
    }

    public class Item : NotificationObject
    {
        // SQLServer接続文字列
        public static readonly String CONSTR = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\user\\Documents\\Visual Studio 2015\\test.mdf\";Integrated Security=True;Connect Timeout=30";

        public int Id { get; set; }

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
            //var list = new List<Item>
            //{
            //    new Item("商品１", Category.生活用品, 100, "小林"),
            //    new Item("商品２", Category.食品, 200, "小林"),
            //    new Item("商品３", Category.生活用品, 300, "小林"),
            //    new Item("hoge", Category.未分類, 1, "ほげ"),
            //    new Item("hoge", Category.雑貨, 2, "ほげ"),
            //    new Item("hoge", Category.未分類, 3, "ほげ"),
            //    new Item("hogehoge", Category.未分類, 10, "ほげ"),
            //    new Item("ABC", Category.生活用品, 300, "小林"),
            //    new Item("BBB", Category.生活用品, 300, "小林"),
            //    new Item("CCC", Category.雑貨, 400, "こばやし"),
            //    new Item("商品A", Category.生活用品, 300, "香川"),
            //    new Item("商品B", Category.雑貨, 400, "徳島"),
            //    new Item("商品C", Category.雑貨, 500, "愛媛"),
            //    new Item("商品D", Category.食品, 600, "高知"),

            //};

            //return list;

            //            getItemById(2);
            //updateItem(2, "商品名ににに", "かてごり", 999, "hoge");
            //insertItem("dummy", "kategori", 999, "sakuseisya");

            return getItemListFromDB();

        }

        public static List<Item> getItemListFromDB()
        {
            var list = new List<Item>();

            SqlConnection con = new SqlConnection(CONSTR);
            con.Open();
            try
            {
                String sqlstr = "select * from Item";
                SqlCommand com = new SqlCommand(sqlstr, con);
                SqlDataReader sdr = com.ExecuteReader();

                while(sdr.Read() == true)
                {

                    int id = (int)sdr["id"];
                    String itemName = (String)sdr["itemName"];
                    int price = (int)sdr["price"];
                    String categoryStr = (String)sdr["category"];
                    Category category;
                    switch (categoryStr)
                    {
                        case "食品":
                            category = Category.食品;
                            break;
                        case "雑貨":
                            category = Category.雑貨;
                            break;
                        case "生活用品":
                            category = Category.生活用品;
                            break;
                        default:
                            category = Category.未分類;
                            break;
                    }
                    String createUser = (String)sdr["createUser"];
                    list.Add(new Item { Id = id, ItemName = itemName, Category = category, Price = price, CreateUser = createUser });
                }
            }
            finally
            {
                con.Close();
            }

            return list;

        }


        public static Item getItemById(int searchId)
        {
            Item item = null;

            SqlConnection con = new SqlConnection(CONSTR);
            con.Open();
            try
            {
                
                SqlCommand com = con.CreateCommand();
                com.CommandText = @"select * from Item where id = @ID";
                com.Parameters.Add(new SqlParameter("@ID", searchId));
                SqlDataReader sdr = com.ExecuteReader();
                while (sdr.Read() == true)
                {

                    int id = (int)sdr["id"];
                    String itemName = (String)sdr["itemName"];
                    int price = (int)sdr["price"];
                    String categoryStr = (String)sdr["category"];
                    Category category;
                    switch (categoryStr)
                    {
                        case "食品":
                            category = Category.食品;
                            break;
                        case "雑貨":
                            category = Category.雑貨;
                            break;
                        case "生活用品":
                            category = Category.生活用品;
                            break;
                        default:
                            category = Category.未分類;
                            break;
                    }
                    String createUser = (String)sdr["createUser"];

                    item = new Item();
                    item.Id = id;
                    item.ItemName = itemName;
                    item.Category = category;
                    item.Price = price;
                    item.CreateUser = createUser;
                }
            }
            finally
            {
                con.Close();
            }

            return item == null ? null : item;

        }

        public static void updateItem(int id, String itemName, string category, int price, String createUser)
        {

            SqlConnection con = new SqlConnection(CONSTR);
            con.Open();
            try
            {

                SqlCommand com = con.CreateCommand();
                com.CommandText = @"update Item set itemName = @ItemName, category = @Category, price = @Price, createUser = @CreateUser where id = @Id";

                com.Parameters.Add(new SqlParameter("@ItemName", itemName));
                com.Parameters.Add(new SqlParameter("@Category", category));
                com.Parameters.Add(new SqlParameter("@Price", price));
                com.Parameters.Add(new SqlParameter("@CreateUser", createUser));
                com.Parameters.Add(new SqlParameter("@Id", id));
                
                int count = com.ExecuteNonQuery();

            }
            finally
            {
                con.Close();
            }

            return;

        }

        public static void insertItem(String itemName, string category, int price, String createUser)
        {

            SqlConnection con = new SqlConnection(CONSTR);
            con.Open();
            try
            {

                SqlCommand com = con.CreateCommand();
                com.CommandText = @"INSERT INTO Item (itemName, price, category, createUser) VALUES (@ItemName, @Price, @Category, @CreateUser)";

                com.Parameters.Add(new SqlParameter("@ItemName", itemName));
                com.Parameters.Add(new SqlParameter("@Category", category));
                com.Parameters.Add(new SqlParameter("@Price", price));
                com.Parameters.Add(new SqlParameter("@CreateUser", createUser));

                int count = com.ExecuteNonQuery();

            }
            finally
            {
                con.Close();
            }

            return;

        }

        public static void deleteItem(int id)
        {

            SqlConnection con = new SqlConnection(CONSTR);
            con.Open();
            try
            {

                SqlCommand com = con.CreateCommand();
                com.CommandText = @"delete from Item where id = @Id";
                com.Parameters.Add(new SqlParameter("@Id", id));


                int count = com.ExecuteNonQuery();

            }
            finally
            {
                con.Close();
            }

            return;

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
