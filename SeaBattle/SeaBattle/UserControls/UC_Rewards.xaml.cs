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
        public UC_Rewards(int victories, int battles)
        {
            InitializeComponent();
            Victories.Content = victories;
            Defeats.Content = battles-victories;
            Battles.Content = battles;
            Victories_p.Content = $"{Math.Round(victories / (double)battles * 100, 2, MidpointRounding.ToEven)}%";
            Defeats_p.Content = $"{Math.Round((battles - victories) / (double)battles * 100, 2, MidpointRounding.ToEven)}%";
        }
    }
}
