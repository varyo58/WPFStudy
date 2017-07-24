using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace ViewSample.View
{
    /// <summary>
    /// MainView.xaml の相互作用ロジック
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Bold_Click(object sender, RoutedEventArgs e)
        {
            rtb.SelectAll();
            String str =  rtb.Selection.Text;
            Paragraph para = new Paragraph();
            para.Inlines.Add(new Bold(new Run(str)));
            FlowDocument doc = new FlowDocument(para);
            doc.FontSize = 20;
            rtb.Document = doc;
        }

        private void FileOpenButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "ファイルを開く";
            dialog.Filter = "全てのファイル(*.*)|*.*";
            if (dialog.ShowDialog() == true)
            {
                //this.textBlockFileName.Text = dialog.FileName;

                StreamReader sr = new StreamReader(dialog.FileName);
                FlowDocument fd = new FlowDocument();

                // かなり手抜き
                Paragraph title = new Paragraph();
                title.FontSize = 18;
                title.Inlines.Add(sr.ReadLine());

                //Run run = new Run(" - " + sr.ReadLine());
                //run.FontSize = 12;
                //title.Inlines.Add(run);

                fd.Blocks.Add(title);
                //sr.ReadLine();
                var p = new Paragraph();
                p.FontSize = 12;
                while (!sr.EndOfStream)
                {
                    var s = sr.ReadLine();
                    p.Inlines.Add(s);
                    LineBreak lb = new LineBreak();
                    p.Inlines.Add(lb);
                }
                fd.Blocks.Add(p);
                this.flowDoc.Document = fd;

            }
            else
            {
                //this.textBlockFileName.Text = "キャンセルされました";
            }
        }
        private void PopUp_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = !popup.IsOpen;
        }

        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {
            ResourceDictionary rd = new ResourceDictionary();

            foreach(DictionaryEntry t in this.Resources)
            {
                rd[t.Key] = t.Value;
            }

            if(checkBox1.IsChecked == true)
            {
                ResourceDictionary thema = Application.LoadComponent(new Uri("Dictionarys\\CustomButtons.xaml", UriKind.Relative)) as ResourceDictionary;
                rd.MergedDictionaries.Add(thema);
            }else
            {
                ResourceDictionary thema = Application.LoadComponent(new Uri("Dictionarys\\DefaultButtons.xaml", UriKind.Relative)) as ResourceDictionary;
                rd.MergedDictionaries.Add(thema);
            }


            this.Resources = rd;
        }
    }
}
