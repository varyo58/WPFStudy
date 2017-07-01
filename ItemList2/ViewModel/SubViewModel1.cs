using ItemList2.Common;
using ItemList2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItemList2.ViewModel
{
    public class SubViewModel1 :ViewModelBase
    {

        public SubViewModel1()
        {
            item = new Item("", Category.未分類, 0, "");
        }
        public SubViewModel1(Item item)
        {
            this.item = item;
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
            }
        }

        public List<Category> Categorys
        {
            get
            {
                return Item.getCategoryList();
            }
        }


    }
}
