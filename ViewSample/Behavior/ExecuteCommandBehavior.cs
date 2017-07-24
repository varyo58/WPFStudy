using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ViewSample.Behavior
{
    class ExecuteCommandBehavior
    {

        /// <summary>
        /// ClickCommand:クリック時に指定したコマンドを実行する添付プロパティ
        /// </summary>
        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClickCommandProperty =
            DependencyProperty.RegisterAttached("ClickCommand", typeof(ICommand), typeof(ExecuteCommandBehavior), new PropertyMetadata(null, OnClickCommandChanged));
        public static ICommand GetClickCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(ClickCommandProperty);
        }
        public static void SetClickCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(ClickCommandProperty, value);
        }

        /// <summary>
        /// LostFocusCommand:LostFocus時に指定したコマンドを実行する添付プロパティ
        /// </summary>
        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LostFocusCommandProperty =
            DependencyProperty.RegisterAttached("LostFocusCommand", typeof(ICommand), typeof(ExecuteCommandBehavior), new PropertyMetadata(null, OnLostFocusCommandChanged));
        public static ICommand GetLostFocusCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(LostFocusCommandProperty);
        }
        public static void SetLostFocusCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(LostFocusCommandProperty, value);
        }


        public static object GetCommandParameter(DependencyObject obj)
        {
            return (object)obj.GetValue(CommandParameterProperty);
        }
        public static void SetCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(CommandParameterProperty, value);
        }
        // Using a DependencyProperty as the backing store for CommandParameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(ExecuteCommandBehavior), new PropertyMetadata(null));


        private static void OnClickCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // ClickCommand添付プロパティに変更が加わったら、マウス左クリック時のイベントを登録する。
            var element = d as UIElement;

            

            if (element == null)
                return;

            if (e.NewValue is ICommand)
            {
                element.MouseLeftButtonDown += element_Click;
                // TextBoxの場合クリックイベントが発火しないので追加
                if (element.GetType() == typeof(TextBox))
                {
                    element.AddHandler(TextBox.MouseLeftButtonDownEvent, new MouseButtonEventHandler(element_Click), true);
                }
            }
            else
            {
                element.MouseLeftButtonDown -= element_Click;
                if (element.GetType() == typeof(TextBox))
                {
                    element.RemoveHandler(TextBox.MouseLeftButtonDownEvent, new MouseButtonEventHandler(element_Click));
                }
            }
        }

        private static void OnLostFocusCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // LostFocusCommand添付プロパティに変更が加わったら、ロストフォーカス時のイベントを登録する。
            var element = d as UIElement;
            if (element == null)
                return;

            if (e.NewValue is ICommand)
            {
                element.LostFocus += element_LostFocus;
            }
            else
            {
                element.LostFocus -= element_LostFocus;
            }
        }


        static void element_Click(object sender, MouseButtonEventArgs e)
        {
            // プロパティに設定されたコマンドを実行する。
            var element = sender as UIElement;
            if (element == null)
                return;

            var cmd = GetClickCommand(element);
            var param = GetCommandParameter(element);
            if (cmd.CanExecute(param))
                cmd.Execute(param);
        }

        static void element_LostFocus(object sender, RoutedEventArgs e)
        {
            // プロパティに設定されたコマンドを実行する。
            var element = sender as UIElement;
            if (element == null)
                return;

            var cmd = GetLostFocusCommand(element);
            var param = GetCommandParameter(element);
            if (cmd.CanExecute(param))
                cmd.Execute(param);
        }


    }
}
