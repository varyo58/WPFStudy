﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ItemList2.Common
{
    /// <summary>
    /// NumericUpDown.xaml の相互作用ロジック
    /// </summary>
    public partial class NumericUpDown : UserControl
    {
        public NumericUpDown()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty ValueProperty =
    DependencyProperty.Register(
        "Value",
        typeof(int),
        typeof(NumericUpDown),
        new PropertyMetadata(0));

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            this.Value++;
        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            this.Value--;
        }
    }
}
