using ItemListLivet.Model;
using Livet;
using Livet.Commands;
using Livet.Messaging.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItemListLivet.ViewModels
{
    class SubWindowViewModel : ViewModel
    {


        public SubWindowViewModel(Item item)
        {
            this.Item = item;
        }

        private Item origin;
        private Item item;
        public Item Item
        {
            get
            {
                return item;
            }
            set
            {
                if( origin == value)
                {
                    return;
                }
                origin = value;
                this.item = new Item() { ItemName = origin.ItemName, Category = origin.Category, Price = origin.Price, CreateUser = origin.CreateUser };
                RaisePropertyChanged("Item");
            }
        }


        public List<Category> Categorys
        {
            get
            {
                return Item.getCategoryList();
            }
        }

        private ViewModelCommand updateCommand;

        public ViewModelCommand UpdateCommand
        {
            get
            {
                if (updateCommand == null)
                {
                    updateCommand = new ViewModelCommand(Update);
                }
                return updateCommand;
            }
        }

        public void Update()
        {
            origin.ItemName = item.ItemName;
            origin.Category = item.Category;
            origin.Price = item.Price;
            origin.CreateUser = item.CreateUser;

            Messenger.Raise(new WindowActionMessage(WindowAction.Close, "Close"));

        }



    }
}
