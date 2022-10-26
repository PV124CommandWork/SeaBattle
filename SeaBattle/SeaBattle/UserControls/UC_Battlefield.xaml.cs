using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
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
    /// Interaction logic for UC_Battlefield.xaml
    /// </summary>
    public partial class UC_Battlefield : UserControl
    {
        public UC_Battlefield()
        {
            InitializeComponent();
            for (int i = 0; i < 10; i++)
            {
                Player1Field.ColumnDefinitions.Add(new ColumnDefinition());
                Player1FieldShips.ColumnDefinitions.Add(new ColumnDefinition());
                Player2Field.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < 10; i++)
            {
                Player1Field.RowDefinitions.Add(new RowDefinition());
                Player1FieldShips.RowDefinitions.Add(new RowDefinition());
                Player2Field.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Border border = new Border();
                    Grid.SetColumn(border, i);
                    Grid.SetRow(border, j);
                    border.BorderBrush = new SolidColorBrush(Colors.Gray);
                    border.BorderThickness = new Thickness(1);
                    Player1Field.Children.Add(border);
                }
            }
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Border border = new Border();
                    Grid.SetColumn(border, i);
                    Grid.SetRow(border, j);
                    border.BorderBrush = new SolidColorBrush(Colors.Gray);
                    border.BorderThickness = new Thickness(1);
                    Player2Field.Children.Add(border);
                }
            }
        }

        private void Attack_Click(object sender, RoutedEventArgs e)
        {
            Thread th = new Thread(() =>
            {
                for (int i = 1, col = 0; i <= 4; i++, col += i - 1)
                {
                    Dispatcher.Invoke(() => AddShip(col, 0, i));
                    Thread.Sleep(200);
                }
            });
            th.Start();
            Thread th2 = new Thread(() =>
            {
                th.Join();
                for (int i = 1; i <= 4; i++)
                {
                    Dispatcher.Invoke(() => AddShip(0, i, i));
                    Thread.Sleep(200);
                }
            });
            th2.Start();
            AddShip(5, 5, 3, true);
            AddShip(6, 5, 4, true);
            AddShip(7, 5, 1, true);
            AddShip(8, 5, 2, true);
        }

        private void Cacel_Click(object sender, RoutedEventArgs e)
        {

        }

        public void AddShip(int col, int row, int decks, bool isVertical = false)
        {
            Image img = new Image();
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            if (isVertical && decks != 1)
            {
                bitmapImage.Rotation = Rotation.Rotate90;
            }
            bitmapImage.UriSource = new Uri(@"E:\дз\ШАГ\SeaBattle\SeaBattle\SeaBattle\bin\Debug\Images\" + decks + "deck.png");
            bitmapImage.EndInit();
            img.Source = bitmapImage;
            Grid.SetColumn(img, col);
            Grid.SetRow(img, row);
            if (decks != 1)
            {
                if (isVertical)
                {
                    Grid.SetRowSpan(img, decks);
                }
                else
                {
                    Grid.SetColumnSpan(img, decks);
                }
            }
            Player1FieldShips.Children.Add(img);
        }
    }
}
