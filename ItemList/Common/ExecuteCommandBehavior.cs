using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ItemList.Common
{
    class ExecuteCommandBehavior
    {


        public static ICommand GetCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandProperty);
        }
        public static void SetCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }
        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(ExecuteCommandBehavior), new PropertyMetadata(null, OnCommandChanged));


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


        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Command添付プロパティに変更が加わったら、マウス左クリック時のイベントを登録する。
            var element = d as UIElement;
            if (element == null)
                return;

            if (e.NewValue is ICommand)
            {
                //element.MouseLeftButtonDown += element_MouseLeftButtonDown;
                element.LostFocus += element_LostFocus;
            }
            else
            {
                //element.MouseLeftButtonDown -= element_MouseLeftButtonDown;
                element.LostFocus -= element_LostFocus;
            }

        }


        static void element_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // マウス左クリック時には、プロパティに設定されたコマンドを実行する。
            var element = sender as UIElement;
            if (element == null)
                return;

            var cmd = GetCommand(element);
            var param = GetCommandParameter(element);
            if (cmd.CanExecute(param))
                cmd.Execute(param);
        }

        static void element_LostFocus(object sender, RoutedEventArgs e)
        {
            // フォーカスアウト時には、プロパティに設定されたコマンドを実行する。
            var element = sender as UIElement;
            if (element == null)
                return;

            var cmd = GetCommand(element);
            var param = GetCommandParameter(element);
            if (cmd.CanExecute(param))
                cmd.Execute(param);
        }

    }
}
