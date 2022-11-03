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
    /// Interaction logic for UC_RegisterPage.xaml
    /// </summary>
    public partial class UC_RegisterPage : UserControl
    {
        public UC_RegisterPage()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (UserNameTB.Text.Length < 6)
            {
                MessageBox.Show("Занадто короткий нікнейм");
                return;
            }
            if (LoginTB.Text.Length < 6)
            {
                MessageBox.Show("Занадто короткий логін");
                return;
            }
            if (PasswordPB.Password.Length < 6)
            {
                MessageBox.Show("Занадто короткий пароль");
                return;
            }
            if (!EmailTB.Text.Contains("@"))
            {
                MessageBox.Show("Некоректний email");
                return;
            }
            if (CodeTB.IsEnabled == true && CodeTB.Text.Length != 6)
            {
                MessageBox.Show("Невірний код!");
                return;
            }





            if (PasswordPB.Password == ConfirmPasswordPB.Password)
            {
               if(CodeTB.IsEnabled == true)
                {
                    SeaBattleServerComunication.SendToServer.SendRegisterData(UserNameTB.Text, LoginTB.Text, PasswordPB.Password, EmailTB.Text, CodeTB.Text);
                }
               else
                {
                    SeaBattleServerComunication.SendToServer.SendRegisterData(UserNameTB.Text, LoginTB.Text, PasswordPB.Password, EmailTB.Text);
                    CodeTB.IsEnabled = true;
                }
            }
            else
            {
                MessageBox.Show("Паролі не співпадають.");
                PasswordPB.Password = "";
                ConfirmPasswordPB.Password = "";
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainWindowInstance.Dispatcher.Invoke(() => {
                MainWindow.MainWindowInstance.MainGrid.Children.Clear();
                MainWindow.MainWindowInstance.MainGrid.Children.Add(new UC_LoginPage());
            });
        }
    }
}
