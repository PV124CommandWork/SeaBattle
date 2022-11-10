using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
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
using MaterialDesignThemes.Wpf;
using ShipsClass;

namespace SeaBattle.UserControls
{
    /// <summary>
    /// Interaction logic for UC_Battlefield.xaml
    /// </summary>
    public partial class UC_Battlefield : UserControl
    {
        public static List<Ship> Ships;
        public static bool Move { get; private set; } = false;
        public Coords<int> Target = new Coords<int>() { X = -1, Y = -1};
        public UC_Battlefield()
        {
            InitializeComponent();

            Ships = new List<Ship>();

            for (int i = 0; i < 10; i++)
            {
                Player1Field.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(50) });
                Player2Field.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(50) });
            }
            for (int i = 0; i < 10; i++)
            {
                Player1Field.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50) });
                Player2Field.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50) });
            }
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Border border = new Border();
                    Grid.SetColumn(border, i);
                    Grid.SetRow(border, j);
                    border.BorderBrush = new SolidColorBrush(Colors.Gray);
                    border.BorderThickness = new Thickness(2);
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
                    border.MouseDown += SelectCell_MouseDown;
                    border.BorderBrush = new SolidColorBrush(Colors.Gray);
                    border.Background = new SolidColorBrush(Colors.White);
                    border.BorderThickness = new Thickness(2);
                    Player2Field.Children.Add(border);
                }
            }









            Ships.Add(new Ship(new Deck(2, 2), 4, false));
        }

        private void SelectCell_MouseDown(object sender, MouseButtonEventArgs e)
        {
            foreach(var item in Player2Field.Children)
            {
                (item as Border).BorderBrush = new SolidColorBrush(Colors.Gray);
            }
            (sender as Border).BorderBrush = new SolidColorBrush(Colors.Red);
            Target = new Coords<int>() { X= Grid.GetColumn(sender as Border), Y = Grid.GetRow(sender as Border) };
        }

        private void Attack_Click(object sender, RoutedEventArgs e)
        {
            ShowShoot(new Shoot(2, 5));
        }

        public void ShowShips(List<Ship> ships)
        {
            Ships = ships;
            foreach (var ship in Ships)
            {
                AddShip(ship);
            }
        }

        public void ChangeMove(bool move)
        {
            Move = move;
            if (Move)
            {
                MoveLabel.Content = "Your move!";
                MoveLabel.Foreground = Brushes.Lime;
                AttackButton.IsEnabled = true;
                AttackButton.Visibility = Visibility.Visible;
            }
            else
            {
                MoveLabel.Content = "Waiting for enemys move...";
                MoveLabel.Foreground = Brushes.Black;
                AttackButton.Visibility = Visibility.Hidden;
                AttackButton.IsEnabled = false;
            }
        }

        public void AddShip(Ship ship)
        {
            Image img = new Image();
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            if (!(ship.IsHorisontal) && ship.DecksCount != 1)
            {
                bitmapImage.Rotation = Rotation.Rotate90;
            }
            bitmapImage.UriSource = new Uri(Directory.GetCurrentDirectory() + @"\Images\" + ship.DecksCount + "deck.png");
            bitmapImage.EndInit();
            img.Source = bitmapImage;
            Grid.SetColumn(img, ship.Decks[0].Coords.X);
            Grid.SetRow(img, ship.Decks[0].Coords.Y);
            if (ship.DecksCount != 1)
            {
                if (ship.IsHorisontal)
                {
                    Grid.SetColumnSpan(img, ship.DecksCount);
                }
                else
                {
                    Grid.SetRowSpan(img, ship.DecksCount);
                }
            }
            Player1Field.Children.Add(img);
        }

        public void ShowShoot(Shoot shoot)
        {
            bool isPlayerMove = true;
            if (shoot.ReturnedValue == null)
            {
                shoot.Damage(ref Ships);
                isPlayerMove = false;
            }
            Image img = new Image();
            Grid.SetColumn(img, shoot.Coords.X);
            Grid.SetRow(img, shoot.Coords.Y);
            switch (shoot.ReturnedValue)
            {
                case -1:
                    {
                        img.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Images\MissIcon.png"));
                        break;
                    }
                case 0:
                    {
                        img.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Images\DamagedIcon.png"));
                        break;
                    }
                case 1:
                    {
                        //int shipIndex = Ships.FindIndex(s=>s.Decks.FindIndex(d => d.Coords.X == shoot.Coords.X && d.Coords.Y == shoot.Coords.Y)!=-1);
                        Coords<int> from = Ships[shoot.DestroyedShipIndex].Decks[0].Coords;
                        Coords<int> to = Ships[shoot.DestroyedShipIndex].Decks[Ships[shoot.DestroyedShipIndex].Decks.Count - 1].Coords;
                        for (int x = from.X - 1; x <= to.X + 1; x++)
                        {
                            for (int y = from.Y - 1; y <= to.Y + 1; y++)
                            {
                                if (y != from.Y - 1 && y != to.Y + 1 && x != from.X - 1 && x != to.X + 1)
                                {
                                    continue;
                                }
                                ShowShoot(new Shoot(x, y));
                            }
                        }
                        img.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Images\DamagedIcon.png"));
                        break;
                    }
                default: return;
            }
            if (isPlayerMove)
            {
                Player2Field.Children.Add(img);
            }
            else 
            {
                Player1Field.Children.Add(img);
            }
        }
    }
}
