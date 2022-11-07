using SeaBattleServerComunication;
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
    /// Логика взаимодействия для UC_FriendList.xaml
    /// </summary>
    public partial class UC_FriendList : UserControl
    {
        public static List<string> Friends;
        public static int SelectedFriend = -1;
        public UC_FriendList(List<string> friends)
        {
            InitializeComponent();
            Friends = friends;
            Settings.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Images\gear_icon.png"));
            Statistics.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Images\statistics_icon.png"));
            LoadFriends();
            foreach (var item in FriendsManagementDP.Children)
            {
                (item as Button).IsEnabled = false;
            }
            FriendsManagementDP.Opacity = 0;
            SendToServer.SendFriendsRequest();
        }
        public void LoadFriends()
        {
            FriendList.Children.Clear();
            foreach (var item in Friends)
            {
                Label label = new Label();
                Border border = new Border();
                border.Child = label;
                label.Content = item;
                border.Height = 40;
                label.FontSize = 20;
                border.MouseLeftButtonDown += Friend_Click;
                border.Background = new SolidColorBrush(Color.FromRgb(77, 77, 77));
                label.Foreground = Brushes.DarkCyan;
                border.BorderBrush = Brushes.DarkCyan;
                border.BorderThickness = new Thickness(2, 0, 0, 2);
                label.HorizontalAlignment = HorizontalAlignment.Center;
                FriendList.Children.Add(border);
            }
        }

        private void Friend_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in FriendList.Children)
            {
                (item as Border).Background = new SolidColorBrush(Color.FromRgb(77, 77, 77));
                ((item as Border).Child as Label).Foreground = Brushes.DarkCyan;
            }
            (sender as Border).Background = Brushes.DarkCyan;
            ((sender as Border).Child as Label).Foreground = new SolidColorBrush(Color.FromRgb(77, 77, 77));
            SelectedFriend = FriendList.Children.IndexOf(sender as Border);
            foreach (var item in FriendsManagementDP.Children)
            {
                (item as Button).IsEnabled = true;
            }
            FriendsManagementDP.Opacity = 1;
        }

        private void AddFriendButton_Click(object sender, RoutedEventArgs e)
        {
            SendToServer.SendFriendRequest(textBoxLogin.Text);
        }
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainWindowInstance.Dispatcher.Invoke(() =>
            {
                MainWindow.MainWindowInstance.MainGrid.Children.Add(new UC_Settings());
            });
        }
        private void StatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainWindowInstance.Dispatcher.Invoke(() =>
            {
                MainWindow.MainWindowInstance.MainGrid.Children.Add(new UC_Rewards());
            });
        }
        private void InviteToBattle_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ShowRewards_Click(object sender, RoutedEventArgs e)
        {

        }
        private void DeleteFriend_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedFriend != -1)
            {
                FriendList.Children.RemoveAt(SelectedFriend);
                SelectedFriend = -1;
                FriendsManagementDP.Opacity = 0;
                foreach (var item in FriendsManagementDP.Children)
                {
                    (item as Button).IsEnabled = false;
                }
            }
        }

    }
}