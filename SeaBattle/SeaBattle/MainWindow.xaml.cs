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
using SeaBattle.UserControls;
using SeaBattleServerComunication;

namespace SeaBattle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void ChangeUcDelegate(UserControl userControl);
        public static ChangeUcDelegate ChangeUc;
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

            ChangeUc += ChangeUC;
            ChangeUc.Invoke(new UC_LoginPage());
        }
        public void ChangeUC(UserControl userControl)
        {
            this.Dispatcher.Invoke(() =>
            {
                MainGrid.Children.Clear();
                MainGrid.Children.Add(userControl);
            });
        }
    }
}
