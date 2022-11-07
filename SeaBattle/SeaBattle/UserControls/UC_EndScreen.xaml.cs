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
    /// Логика взаимодействия для UC_EndScreen.xaml
    /// </summary>
    public partial class UC_EndScreen : UserControl {
        public UC_EndScreen() {
            InitializeComponent();
        }
        public void Load(bool isWon, int battles, int victories) {
            if(isWon == false) {
                WDlabel.Content = "Поразка.";
                WDlabel.Foreground = Brushes.Red;
            }
            battlesLabel.Content = battles;
            victoriesLabel.Content = $"Всього перемог: {victories}";
            defeatsLabel.Content = $"Всього поразок: {battles - victories}";
        }
        public void LoadField(string p1FieldData, string p2FieldData) {

        }

        private void Button_Click(object sender, RoutedEventArgs e) {

        }
    }
}
