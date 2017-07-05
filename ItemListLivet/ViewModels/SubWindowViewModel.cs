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

        private int mode;// 1:新規、2:更新
        public int Mode
        {
            get { return mode; }
        }

        public SubWindowViewModel()
        {
            mode = 1;
            this.Item = new Item();
        }
        public SubWindowViewModel(int id)
        {
            mode = 2;
            this.Item = Item.getItemById(id);
        }

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

        //private ViewModelCommand updateCommand;
        //public ViewModelCommand UpdateCommand
        //{
        //    get
        //    {
        //        if (updateCommand == null)
        //        {
        //            updateCommand = new ViewModelCommand(Update);
        //        }
        //        return updateCommand;
        //    }
        //}


        private ViewModelCommand upsertCommand;
        public ViewModelCommand UpsertCommand
        {
            get
            {
                if (upsertCommand == null)
                {
                    switch (mode)
                    {
                        case 1:
                            upsertCommand = new ViewModelCommand(Insert);
                            break;
                        case 2:
                            upsertCommand = new ViewModelCommand(Update);
                            break;
                    }
                }
                return upsertCommand;
            }
        }

        private void Update()
        {

            Item.updateItem(this.Item.Id, this.Item.ItemName, this.Item.Category.ToString(), this.Item.Price, this.Item.CreateUser);

            Messenger.Raise(new WindowActionMessage(WindowAction.Close, "Close"));

        }

        private void Insert()
        {

            Item.insertItem(this.Item.ItemName, this.Item.Category.ToString(), this.Item.Price, this.Item.CreateUser);

            Messenger.Raise(new WindowActionMessage(WindowAction.Close, "Close"));

        }


    }
}
