using MaterialDesignColors.Recommended;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using ShipsClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for UC_FillBattlefield.xaml
    /// </summary>
    public partial class UC_FillBattlefield : UserControl
    {
        public static List<Ship> Ships;
        private int selectedShip = 0;
        public static bool IsAnotherPlayerReady = false;
        public UC_FillBattlefield()
        {
            InitializeComponent();

            Ships = new List<Ship>();
            Img_BG.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Images\FillBG.png"));

            for (int i = 0; i < 10; i++)        //Задаємо розмір сітки
            {
                Player1Field.ColumnDefinitions.Add(new ColumnDefinition());
                Player1Field.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < 10; i++)        //Вигляд сітки
            {
                for (int j = 0; j < 10; j++)
                {
                    Border border = new Border();
                    border.MouseDown += Border_MouseDown;
                    Grid.SetColumn(border, i);
                    Grid.SetRow(border, j);
                    border.Background = new SolidColorBrush(Colors.White);
                    border.BorderBrush = new SolidColorBrush(Colors.Gray);
                    border.BorderThickness = new Thickness(1);
                    Player1Field.Children.Add(border);
                }
            }

            for (int i = 4; i >= 1; i--)        //Додаємо кораблі для вибору
            {
                for (int j = 0; j <= 4 - i; j++)
                {
                    StackShip(i);
                }
            }
        }
        #region Events for ships
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)        //Подія для сітки, яка ставить корабель на поле
        {
            if (selectedShip != 0)
            {
                Ship ship = new Ship(new Deck(Grid.GetColumn((sender as Border)), Grid.GetRow((sender as Border))), selectedShip, IsHorisontalCB.IsChecked ?? false);

                foreach (var decks in ship.Decks)
                {
                    if (decks.Coords.X >= 10 || decks.Coords.Y >= 10)
                    {
                        return;
                    }
                }
                foreach (var placedShip in Ships)
                {
                    foreach (var placedDeck in placedShip.Decks)
                    {
                        foreach (var decks in ship.Decks)
                        {
                            if ((decks.Coords.X >= placedDeck.Coords.X - 1 && decks.Coords.X <= placedDeck.Coords.X + 1
                            && decks.Coords.Y >= placedDeck.Coords.Y - 1 && decks.Coords.Y <= placedDeck.Coords.Y + 1))
                            {
                                return;
                            }
                        }
                    }
                }
                for (int i = 0; i < ShipsList_SP.Children.Count; i++)
                {
                    if (int.Parse(System.IO.Path.GetFileName(((ShipsList_SP.Children[i] as Image).Source as BitmapImage).UriSource.LocalPath)[0].ToString()) == selectedShip)
                    {
                        ShipsList_SP.Children.Remove(ShipsList_SP.Children[i]);
                        break;
                    }
                }
                AddShip(ship);
                selectedShip = 0;
                if (ShipsList_SP.Children.Count == 0)
                {
                    ConfirmButton.IsEnabled = true;
                }
            }
        }

        private void Ship_Clicked(object sender, MouseButtonEventArgs e)        //Подія, що вибирає корабель, який потрібно поставити
        {
            selectedShip = int.Parse(System.IO.Path.GetFileName(((sender as Image).Source as BitmapImage).UriSource.LocalPath)[0].ToString());
        }
        private void ShipOnField_Clicked(object sender, MouseButtonEventArgs e)     //Подія, щоб прибрати корабель з поля
        {
            int palubes = int.Parse(System.IO.Path.GetFileName(((sender as Image).Source as BitmapImage).UriSource.LocalPath)[0].ToString());
            int x = Grid.GetColumn(sender as Image);
            int y = Grid.GetRow(sender as Image);

            ConfirmButton.IsEnabled = false;

            Player1Field.Children.Remove(sender as Image);
            foreach (var ship in Ships)
            {
                if (ship.Decks[0].Coords.X == x && ship.Decks[0].Coords.Y == y)
                {
                    Ships.Remove(ship);
                    break;
                }
            }

            StackShip(palubes);
        }
        #endregion
        #region Ship images management
        public void AddShip(Ship ship)      //Ставить корабель на поле
        {
            Ships.Add(ship);
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
            img.MouseDown += ShipOnField_Clicked;
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

        private void StackShip(int palubes)         //Додати нерозміщений корабель
        {
            Image shipImg = new Image();
            shipImg.Height = 30;
            shipImg.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Images\" + palubes + "deck.png"));
            shipImg.HorizontalAlignment = HorizontalAlignment.Left;
            shipImg.MouseDown += Ship_Clicked;
            ShipsList_SP.Children.Add(shipImg);
        }
        #endregion

        private void Ready_Click(object sender, RoutedEventArgs e)
        {
            SeaBattleServerComunication.SendToServer.SendPlayerReady(JsonConvert.SerializeObject(Ships));
        }
    }
}
