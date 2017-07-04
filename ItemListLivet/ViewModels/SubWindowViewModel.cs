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


        public SubWindowViewModel(int id)
        {
            this.Item = Item.getItemById(id);
        }

        //private Item origin;
        private Item item;
        public Item Item
        {
            get
            {
                return item;
            }
            set
            {
                this.item = value;
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
            //origin.ItemName = item.ItemName;
            //origin.Category = item.Category;
            //origin.Price = item.Price;
            //origin.CreateUser = item.CreateUser;

            Item.updateItem(this.Item.Id, this.Item.ItemName, this.Item.Category.ToString(), this.Item.Price, this.Item.CreateUser);

            Messenger.Raise(new WindowActionMessage(WindowAction.Close, "Close"));

        }



    }
}
