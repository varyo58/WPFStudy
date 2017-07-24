using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace ViewSample.View
{
    /// <summary>
    /// EventSample.xaml の相互作用ロジック
    /// </summary>
    public partial class EventSample : Window
    {
        public EventSample()
        {
            InitializeComponent();
            //this.PreviewMouseRightButtonDown += Window_PreviewMouseRightButtonDown;
        }

        private void Window_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            //後続のイベントの処理をキャンセル
            e.Handled = true; 
            Debug.WriteLine("Window_PreviewMouseRightButtonDown");
        }

        private void Window_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("Window_MouseRightButtonDown");
        }

        private void GroupBox_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            //後続のイベントの処理をキャンセル
            //e.Handled = true; 
            Debug.WriteLine("GroupBox_PreviewMouseRightButtonDown");
        }

        private void GroupBox_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("GroupBox_MouseRightButtonDown");
        }

        private void Button_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("Button_PreviewMouseRightButtonDown");
        }

        private void Button_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("Button_MouseRightButtonDown");
        }

        private void StackPanel_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("StackPanel_Click");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Button_Click");
        }
    }
}
