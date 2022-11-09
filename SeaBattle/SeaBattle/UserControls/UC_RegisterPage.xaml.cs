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

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (UserNameTB.Text.Length < 6 || UserNameTB.Text.Length > 16)
            {
                MessageBox.Show("Нікнейм повинен містити від 6 до 16 символів");
                return;
            }
            if (LoginTB.Text.Length < 6 || LoginTB.Text.Length > 16)
            {
                MessageBox.Show("Логін повинен містити від 6 до 16");
                return;
            }
            if (PasswordPB.Password.Length < 6 || PasswordPB.Password.Length > 16)
            {
                MessageBox.Show("Пароль повинен містити від 6 до 16");
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
                if (CodeTB.IsEnabled == true)
                {
                    SeaBattleServerComunication.SendToServer.SendRegisterData(UserNameTB.Text, LoginTB.Text, PasswordPB.Password, EmailTB.Text, CodeTB.Text);
                }
                else
                {
                    SeaBattleServerComunication.SendToServer.SendRegisterData(UserNameTB.Text, LoginTB.Text, PasswordPB.Password, EmailTB.Text);
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
            ChangeUserControler.ToLoginPage();
        }
    }
}
