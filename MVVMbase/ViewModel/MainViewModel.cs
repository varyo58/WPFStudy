namespace MVVMbase.ViewModel
{
    using MVVMbase.Common;

    /// <summary> 
    /// MainViewのViewModel 
    /// </summary> 
    public class MainViewModel : ViewModelBase
    {
        private string str;

        public MainViewModel()
        {
            
        }

        public string Str
        {
            get
            {
                return this.str;
            }

            set
            {
                this.str = value;
            }
        }

    }
}