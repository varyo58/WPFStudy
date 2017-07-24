using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ViewSample.Behavior
{
    /// <summary>
    /// TextBox 添付ビヘイビア
    /// </summary>
    public class TextBoxBehaviors
    {

        /// <summary>
        /// True なら入力を数字のみに制限します。
        /// </summary>
        public static readonly DependencyProperty IsNumericProperty =
                    DependencyProperty.RegisterAttached(
                        "IsNumeric", typeof(bool),
                        typeof(TextBoxBehaviors),
                        new UIPropertyMetadata(false, IsNumericChanged)
                    );

        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static bool GetIsNumeric(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsNumericProperty);
        }

        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static void SetIsNumeric(DependencyObject obj, bool value)
        {
            obj.SetValue(IsNumericProperty, value);
        }

        private static void IsNumericChanged
            (DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {

            var textBox = sender as TextBox;
            if (textBox == null) return;

            // イベントを登録・削除 
            textBox.KeyDown -= OnKeyDown;
            textBox.TextChanged -= OnTextChanged;
            DataObject.RemovePastingHandler(textBox, TextBoxPastingEventHandler);
            var newValue = (bool)e.NewValue;
            if (newValue)
            {
                textBox.KeyDown += OnKeyDown;
                textBox.TextChanged += OnTextChanged;
                DataObject.AddPastingHandler(textBox, TextBoxPastingEventHandler); // ペーストを考慮
            }
        }

        static void OnKeyDown(object sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;

            if ((Key.D0 <= e.Key && e.Key <= Key.D9) ||
                (Key.NumPad0 <= e.Key && e.Key <= Key.NumPad9) ||
                (Key.Delete == e.Key) || (Key.Back == e.Key) || (Key.Tab == e.Key))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private static void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;

            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = "0";
            }
        }

        // クリップボード経由の貼り付けチェック
        private static void TextBoxPastingEventHandler(object sender, DataObjectPastingEventArgs e)
        {
            var textBox = (sender as TextBox);
            var clipboard = e.DataObject.GetData(typeof(string)) as string;
            clipboard = ValidateValue(clipboard);
            if (textBox != null && !string.IsNullOrEmpty(clipboard))
            {
                textBox.Text = clipboard;
            }
            e.CancelCommand();
            e.Handled = true;
        }

        private static string ValidateValue(string text)
        {
            string returntext = "";
            foreach (char c in text)
            {
                if (Regex.Match(c.ToString(), "^[-0-9]$").Success)
                    returntext += c.ToString();
                else if (c == '.')
                    returntext += c.ToString();
            }
            return returntext;
        }






        public static readonly DependencyProperty NgWordProperty =
                    DependencyProperty.RegisterAttached(
                        "NgWord", typeof(String),
                        typeof(TextBoxBehaviors),
                        new UIPropertyMetadata(null, NgWordChanged)
                    );

        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static String GetNgWord(DependencyObject obj)
        {
            return (String)obj.GetValue(NgWordProperty);
        }

        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static void SetNgWord(DependencyObject obj, String value)
        {
            obj.SetValue(NgWordProperty, value);
        }

        public static void NgWordChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;

            // イベントを登録・削除 
            var newValue = (String)e.NewValue;
            var handler = createEventHandler(newValue);
            if (String.IsNullOrEmpty(newValue))
            {
                textBox.LostFocus -= handler;
            }
            else
            {
                textBox.LostFocus += handler;
            }
            
                
                
            
        }
        //static void OnKeyDownCheck(object sender, KeyEventArgs e)
        //{
        //    var textBox = sender as TextBox;
        //    if (textBox == null) return;



        //    //var handler = createKeyEventHandler(textBox.Text);


        //}

        private static RoutedEventHandler createEventHandler(string checkStr)
        {
            // KeyEvent イベントをハンドルし、TextBoxの値に引数が含まれているかチェックする。
            return (sender, e) =>
            {

                var textBox = (TextBox)sender;
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    if (textBox.Text.Contains(checkStr))
                    {
                        MessageBox.Show("NGワード");
                        Console.WriteLine("NgWord");
                    }
                }
               
            };
        }

    }
}
