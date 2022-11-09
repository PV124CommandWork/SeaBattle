using SeaBattleServerComunication;
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
    /// Interaction logic for UC_WaitForPlayer.xaml
    /// </summary>
    public partial class UC_WaitForPlayer : UserControl
    {
        public string User;
        public UC_WaitForPlayer(string user)
        {
            InitializeComponent();
            User = user;
        }

        private void Deny_Click(object sender, RoutedEventArgs e)
        {
            SendToServer.SendBattleCanceled(User);
            ChangeUserControler.CloseUserControler(this);
        }
    }
}
