using System;
using System.Windows;
using System.Windows.Browser;

namespace FileStreaming
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void btnReport1_Click(object sender, RoutedEventArgs e)
        {
            Get("DownloadFile.ashx?reportid=1");
            //Get(@"Files/report1.xlsx");
        }

        private void btnReport2_Click(object sender, RoutedEventArgs e)
        {
            Get("DownloadFile.ashx?reportid=2");
            //Get(@"Files/report2.docx");
        }

        private void btnReport3_Click(object sender, RoutedEventArgs e)
        {
            Get("DownloadFile.ashx?reportid=3");
            //Get(@"Files/report3.pdf");
        }

        private void Get(string file)
        {
            HtmlPage.Window.Navigate(new Uri(file, UriKind.Relative), "_blank");
        }
    }
}
