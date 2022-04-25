using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chats
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RegistrationForm registrationForm;
        ServerConnect server;
        public MainWindow()
        {
            InitializeComponent();

            server = new ServerConnect();
        }

        public void NetworkLogin()
        {
            if (Dispatcher.Invoke(() => Login.Text) == "")
            {
                MessageBox.Show("Enter Login");
                return;
            }
            if (Dispatcher.Invoke(() => EntPssword.Password) == "")
            {
                MessageBox.Show("Enter Password");
                return;
            }
            server.onError += mess => MessageBox.Show(mess);
            server.Connect();
            Dispatcher.Invoke(() => server.LoginAccaunt(Login.Text, EntPssword.Password));
            LibProtocol.Models.User user = new LibProtocol.Models.User();
            server.waitResponse((res) => user = (LibProtocol.Models.User)res.data);
            if (!String.IsNullOrEmpty(user.Name))
            {
                Dispatcher.Invoke(() => LoginForm.Children.Remove(GroupLogin));
                Dispatcher.Invoke(() => GroupLogin.Width = 0);
                
                Dispatcher.Invoke(()=>
                    FPage.Content = new UserPage(user.Id, user?.Name, user?.Surname, user?.Phpto, this));

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            registrationForm = new RegistrationForm();
            registrationForm.Owner = this;
            registrationForm.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            Task.Run(() => NetworkLogin());
        }

    }
}
