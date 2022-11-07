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
using Microsoft.SqlServer.Server;
using SeaBattle.UserControls;
using SeaBattleServerComunication;

namespace SeaBattle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        static MainWindow _obj;
        public static MainWindow MainWindowInstance
        {
            get
            {
                if (_obj == null)
                {
                    _obj = new MainWindow();
                }
                return _obj;
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            _obj = this;

            //btw here should be a method to read the config

            #region Connecting To The Server
            try
            {
                ServerConnection.Connect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Current.Shutdown();
            }
            #endregion

            MainGrid.Children.Add(new UC_LoginPage());
            Settings.Init();
        }
    }
}
