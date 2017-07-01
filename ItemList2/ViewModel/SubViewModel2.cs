using ItemList2.Model;
using ItemList2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItemList2.ViewModel
{
    public class SubViewModel2
    {
        public SubViewModel2()
        {

            this.item = new Item();

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
