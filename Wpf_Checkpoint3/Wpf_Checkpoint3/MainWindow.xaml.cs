using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
using Nancy;
using Newtonsoft.Json;

namespace Wpf_Checkpoint3
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Search_file_btn_Click(object sender, RoutedEventArgs e)
        {
            File_TextBlock.Text = String.Empty;
            var client = new WebClient();

            var text = client.DownloadString($"http://localhost:1234/file/{Filepath_TextBox.Text}");
            File_TextBlock.Text = text;

        }

        private void CreateFile_btn_Click(object sender, RoutedEventArgs e)
        {
            File_TextBlock.Text = String.Empty;
            string filepath = $"http://localhost:1234/file/{Filepath_TextBox.Text}";

            WebRequest request = WebRequest.Create(filepath);
            request.Method = "PUT";
            request.ContentLength = 0;
            request.ContentType = "application/xml";
            Stream dataStream = request.GetRequestStream();
            dataStream.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string returnString = response.StatusCode.ToString();
            File_TextBlock.Text = returnString;

        }


        private void DeleteFile_btn_Click(object sender, RoutedEventArgs e)
        {
            string filepath = $"http://localhost:1234/file/{Filepath_TextBox.Text}";

            WebRequest request = WebRequest.Create(filepath);
            request.Method = "DELETE";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            File_TextBlock.Text = response.ToString() + " File deleted";

        }

    }
}
