using Newtonsoft.Json;
using ShipsClass;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для UC_EndScreen.xaml
    /// </summary>
    public partial class UC_EndScreen : UserControl
    {
        public UC_EndScreen()
        {
            InitializeComponent();
        }
        public void Load(bool isWon, int battles, int victories)
        {
            if (isWon == false)
            {
                WDlabel.Content = "Поразка.";
                WDlabel.Foreground = Brushes.Red;
            }
            battlesLabel.Content = battles;
            victoriesLabel.Content = $"Всього перемог: {victories}";
            defeatsLabel.Content = $"Всього поразок: {battles - victories}";
        }
        public void LoadField(string p1FieldData, string p2FieldData)
        {
            for (int i = 0; i < 10; i++)
            {
                SecondFieldGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(50) });
                FirstFieldGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(50) });
            }
            for (int i = 0; i < 10; i++)
            {
                SecondFieldGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50) });
                FirstFieldGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50) });
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
                    FirstFieldGrid.Children.Add(border);
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
                    border.BorderThickness = new Thickness(2);
                    SecondFieldGrid.Children.Add(border);
                }
            }

            List<Ship> ships = JsonConvert.DeserializeObject<List<Ship>>(p1FieldData);
            foreach (Ship ship in ships)
            {
                AddShip(ship, true);
            }
            ships = JsonConvert.DeserializeObject<List<Ship>>(p2FieldData);
            foreach (Ship ship in ships)
            {
                AddShip(ship, false);
            }
        }
        public void AddShip(Ship ship, bool firstField)
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
            if (firstField)
                FirstFieldGrid.Children.Add(img);
            else
                SecondFieldGrid.Children.Add(img);
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeUserControler.ToFriends();
        }
    }
}
