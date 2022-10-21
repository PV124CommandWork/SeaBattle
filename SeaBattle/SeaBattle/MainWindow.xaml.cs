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

using SeaBattleServerComunication;

namespace SeaBattle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //btw here should be a method to read the config

            #region Connecting To The Server
            try
            {
                ServerConnection.Connect();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Current.Shutdown();
            }
            #endregion

        }

        private void SendRequest_Click(object sender, RoutedEventArgs e)
        {
            SendToServer.SendLoginData("Toha229", "123456");
        }
    }
}
