using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommnadTest.Common
{
    class OkCommand : ICommand
    {
        public bool CanExecute(object parameter) { return true; }
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            MessageBox.Show("コマンドが実行されました。");
        }
    }
}
