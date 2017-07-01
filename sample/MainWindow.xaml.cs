using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace hogehoge
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            System.Linq.Expressions.Expression<Func<int, int, int>> sample = (x, y) => x + y;
            this.DataContext = new {
                X = 10,
                Y = 20,
                管理者 = new { 姓 = "岩永", 名 = "信之" },
                コンテンツ = new[]
                {
                    new { タイトル = "C# 入門", URL = "csharp" , MEMO = "aaa"},
                    new { タイトル = "信号処理", URL = "dsp"  , MEMO = "bbb"},
                    new { タイトル = "力学", URL = "dynamics"  , MEMO = "ccc"},
                },
                sample



            };
        }
    }

}
