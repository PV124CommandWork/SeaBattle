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
    /// Interaction logic for UC_AcceptBattle.xaml
    /// </summary>
    public partial class UC_AcceptBattle : UserControl
    {
        public string Login;
        public UC_AcceptBattle(string name, string login)
        {
            InitializeComponent();
            ViklikLB.Content = $"{name} викликає вас на бій!";
            Login = login;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            SeaBattleServerComunication.SendToServer.SendConfirmBattle(Login);
            ChangeUserControler.ToFillBattlefield();
        }

        private void Deny_Click(object sender, RoutedEventArgs e)
        {
            SeaBattleServerComunication.SendToServer.SendBattleCanceled(Login);
            ChangeUserControler.CloseUserControler(this);
        }
    }
}
