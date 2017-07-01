using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ItemList2.View
{
    /// <summary>
    /// SubView2.xaml の相互作用ロジック
    /// </summary>
    public partial class SubView2 : Window
    {
        public SubView2()
        {
            InitializeComponent();

            this.Closed += SubView2_Closed;

        }

        private void SubView2_Closed(object sender, EventArgs e)
        {
            MessageBox.Show("閉じます。", "キャプション");
        }
    }
}
