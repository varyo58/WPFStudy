using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommnadTest.Common;


namespace CommnadTest.ViewModel
{
    public class CommandWindowViewModel
    {

        public ICommand command { get; private set; }

        public CommandWindowViewModel()
        {
            this.command = new OkCommand();
        }
    }

}
