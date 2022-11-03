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
        public UC_AcceptBattle(string Login)
        {
            InitializeComponent();
            ViklikLB.Content = $"{Login} викликає вас на бій!" ;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Deny_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainWindowInstance.Dispatcher.Invoke(() =>
            {
                MainWindow.MainWindowInstance.MainGrid.Children.Remove(this);
            });
        }
    }
}
