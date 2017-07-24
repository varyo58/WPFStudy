namespace ViewSample
{
    using System.Windows;
    using ViewSample.View;

    /// <summary> 
    /// App.xaml の相互作用ロジック 
    /// </summary> 
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            ViewSample.Properties.Settings.Default.ExecCount++;
            ViewSample.Properties.Settings.Default.Save();
            var v = new MainView();
            //var v = new EventSample();
            v.Show();
        }
    }
}