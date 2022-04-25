using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Chats
{
    /// <summary>
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        ServerConnect server;
        LibProtocol.Online tmp;
        public Guid idUser;
        MainWindow main;
        public UserPage(Guid guid, string? name, string? surname, byte[] photo, MainWindow main)
        {
            InitializeComponent();
            this.main = main;
            idUser = guid;
            Name.Text = $"{name} {surname}";
            if (photo != null)
            {
                BitmapImage imgsource = new BitmapImage();
                imgsource.BeginInit();
                imgsource.StreamSource = new MemoryStream(photo);
                imgsource.EndInit();
                Photo.Source = imgsource;
            }
            server = new ServerConnect();
            server.onError += mess => MessageBox.Show(mess);
            Task.Run(()=> Online());
        }
        public void Online()
        {
            while (true)
            {
                server.Connect();
                server.MonOnline();
                Dispatcher.Invoke(() => server.waitResponse((res) => tmp = (LibProtocol.Online)res.data));
                Dispatcher.Invoke(() => ListOnline.Items.Clear());
                Dispatcher.Invoke(() =>
                {
                    ListOnline.Items.Clear();
                    foreach (var item in tmp.OnlineUser)
                    {
                        if (!String.IsNullOrEmpty(item))
                        {
                            ListOnline.Items.Add(item);

                        }
                    }
                    foreach (var mess in tmp.Message)
                    {
                        if (!ListMessage.Items.Contains(mess))
                        {
                            ListMessage.Items.Add(mess);
                        }
                        if (ListMessage.Items.Count == 0)
                        {
                            ListMessage.Items.Add(mess);

                        }
                    }
                });
                Thread.Sleep(1000);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            server.Connect();
            server.SendMessage(WraitMess.Text, idUser);
            WraitMess.Text = "";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            server.Connect();
            server.AccExit(idUser);
            Content = null;
            main.Close();
        }
    }
}
