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

namespace SeaBattle.UserControls {
    /// <summary>
    /// Interaction logic for UC_LogoutConfirm.xaml
    /// </summary>
    public partial class UC_LogoutConfirm : UserControl {
        public UC_LogoutConfirm() {
            InitializeComponent();
        }

        private void Yes_Click(object sender, RoutedEventArgs e) {
            MainWindow.DisconnectingFromServer();
            MainWindow.ConnectingToServer();
            ChangeUserControler.ToLoginPage();
        }

        private void Decline_Click(object sender, RoutedEventArgs e) {
            ChangeUserControler.CloseUserControler(this);
        }
    }
}
