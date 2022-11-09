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
    /// Interaction logic for UC_Rewards.xaml
    /// </summary>
    public partial class UC_Rewards : UserControl
    {
        public UC_Rewards()
        {
            InitializeComponent();
        }

        public void Init(int victories, int battles)
        {
            Victories.Content = victories;
            Defeats.Content = battles - victories;
            Battles.Content = battles;
            if(victories == 0 && battles == 0) {
                Victories_p.Content = "--";
                Defeats_p.Content = "--";
            }
            else {
                Victories_p.Content = $"{Math.Round(victories / (double)battles * 100, 2, MidpointRounding.ToEven)}%";
                Defeats_p.Content = $"{Math.Round((battles - victories) / (double)battles * 100, 2, MidpointRounding.ToEven)}%";
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e) {
            ChangeUserControler.CloseUserControler(this);
        }
    }
}
