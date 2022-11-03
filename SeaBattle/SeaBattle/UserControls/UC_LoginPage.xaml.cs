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

namespace SeaBattle.UserControls
{
    /// <summary>
    /// Interaction logic for UC_LoginPage.xaml
    /// </summary>
    public delegate void ShowExceptionDelegate(string ex);
    public partial class UC_LoginPage : UserControl
    {
        public static ShowExceptionDelegate showException;
        public UC_LoginPage()
        {
            InitializeComponent();
            TeamAvatar.Source = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + @"\Images\TeamAvatar.png"));
            showException = new ShowExceptionDelegate(ShowException);
        }

        private void FieldText_Changed(object sender, TextChangedEventArgs e)
        {
            if ((sender as TextBox).Text == "")
            {
                UsernameLabel.Content = "Username";
            }
            else
            {
                UsernameLabel.Content = "";
            }
        }

        private void FieldText_Changed(object sender, RoutedEventArgs e)
        {
            if ((sender as PasswordBox).Password == "")
            {
                PasswordLabel.Content = "Password";
            }
            else
            {
                PasswordLabel.Content = "";
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            SeaBattleServerComunication.SendToServer.SendLoginData(Username_TB.Text, Password_PB.Password);
        }
        public void ShowException(string ex)
        {
            this.Dispatcher.Invoke(() =>
            {
                UsernameLabel.Foreground = new SolidColorBrush(Colors.Red);
                UsernameHighlight.Background = new SolidColorBrush(Colors.Red);
            });
            this.Dispatcher.Invoke(() =>
            {
                Password_PB.Foreground = new SolidColorBrush(Colors.Red);
                PasswordHighlight.Background = new SolidColorBrush(Colors.Red);
                Password_PB.Password = "";
            });
            MessageBox.Show(ex);
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainWindowInstance.Dispatcher.Invoke(() => {
                MainWindow.MainWindowInstance.MainGrid.Children.Clear();
                MainWindow.MainWindowInstance.MainGrid.Children.Add(new UC_RegisterPage());
            });
        }
    }
}
