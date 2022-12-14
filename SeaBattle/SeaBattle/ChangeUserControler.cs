using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using SeaBattle.UserControls;
using SeaBattleServerComunication;
using ShipsClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SeaBattle {
    public class ChangeUserControler {
        public static void CloseUserControler(UserControl userControl) {
            MainWindow.MainWindowInstance.Dispatcher.Invoke(() => {
                MainWindow.MainWindowInstance.MainGrid.Children.Remove(userControl);
            });
        }
        public static void ToFriends() {
            MainWindow.MainWindowInstance.Dispatcher.Invoke(() =>
            {
                MainWindow.MainWindowInstance.MainGrid.Children.Clear();
                MainWindow.MainWindowInstance.MainGrid.Children.Add(new UC_FriendList(new List<string>()));
            });
        }
        public static void ToLoginPage() {
            MainWindow.MainWindowInstance.Dispatcher.Invoke(() =>
            {
                MainWindow.MainWindowInstance.MainGrid.Children.Clear();
                MainWindow.MainWindowInstance.MainGrid.Children.Add(new UC_LoginPage());
            });
        }
        public static void ShowRewards(Request request) {
            MainWindow.MainWindowInstance.Dispatcher.Invoke(() =>
            {
                //if (MainWindow.MainWindowInstance.MainGrid.Children[1].GetType() == new UC_Rewards().GetType())
                //{
                (MainWindow.MainWindowInstance.MainGrid.Children[1] as UC_Rewards).Init(int.Parse(request.Data[0]), int.Parse(request.Data[1]));
                //}
            });
        }
        public static void ShowFriends(Request request) {
            MainWindow.MainWindowInstance.Dispatcher.Invoke(() =>
            {
                UC_FriendList.Friends.Clear();
                UC_FriendList.Friends.AddRange(JsonConvert.DeserializeObject<IEnumerable<string>>(request.Data[0]));
                (MainWindow.MainWindowInstance.MainGrid.Children[0] as UC_FriendList).LoadFriends();
            });
        }
        public static void ToRegisterPage() {
            MainWindow.MainWindowInstance.Dispatcher.Invoke(() => {
                MainWindow.MainWindowInstance.MainGrid.Children.Clear();
                MainWindow.MainWindowInstance.MainGrid.Children.Add(new UC_RegisterPage());
            });
        }
        public static void ToSettings() {
            MainWindow.MainWindowInstance.Dispatcher.Invoke(() => {
                MainWindow.MainWindowInstance.MainGrid.Children.Add(new UC_Settings());
            });
        }
        public static void ToStatisticts() {
            MainWindow.MainWindowInstance.Dispatcher.Invoke(() => {
                MainWindow.MainWindowInstance.MainGrid.Children.Add(new UC_Rewards());
            });
        }
        public static void ToLogoutConfirm() {
            MainWindow.MainWindowInstance.Dispatcher.Invoke(() => {
                MainWindow.MainWindowInstance.MainGrid.Children.Add(new UC_LogoutConfirm());
            });
        }
        public static void EnableCodeTextBox()
        {
            MainWindow.MainWindowInstance.Dispatcher.Invoke(() =>
            {
                (MainWindow.MainWindowInstance.MainGrid.Children[0] as UC_RegisterPage).CodeTB.IsEnabled = true;
            });
        }
        public static void ToAcceptBattle(Request request)
        {
            MainWindow.MainWindowInstance.Dispatcher.Invoke(() =>
            {
                MainWindow.MainWindowInstance.MainGrid.Children.Add(new UC_AcceptBattle(request.Data[0], request.Login));
            });
        }
        public static void CancelBattle()
        {
            MainWindow.MainWindowInstance.Dispatcher.Invoke(() =>
            {
                MainWindow.MainWindowInstance.MainGrid.Children.RemoveAt(1);
            });
        }

        public static void ToFillBattlefield()
        {
            MainWindow.MainWindowInstance.Dispatcher.Invoke(() => {
                MainWindow.MainWindowInstance.MainGrid.Children.Clear();
                MainWindow.MainWindowInstance.MainGrid.Children.Add(new UC_FillBattlefield());
            });
        }
        public static void PlayerReady()
        {
            MainWindow.MainWindowInstance.Dispatcher.Invoke(() => {
                (MainWindow.MainWindowInstance.MainGrid.Children[0] as UC_FillBattlefield).PlayerReadyLabel.Content = "Another player ready!";
                (MainWindow.MainWindowInstance.MainGrid.Children[0] as UC_FillBattlefield).PlayerReadyLabel.Foreground = Brushes.Lime;
                UC_FillBattlefield.IsAnotherPlayerReady = true;
            });
        }
        public static void ToBattleField(List<ShipsClass.Ship> ships, bool move)
        {
            MainWindow.MainWindowInstance.Dispatcher.Invoke(() => {
                UC_Battlefield bf = new UC_Battlefield();
                bf.ShowShips(ships);
                bf.ChangeMove(move);
                MainWindow.MainWindowInstance.MainGrid.Children.Clear();
                MainWindow.MainWindowInstance.MainGrid.Children.Add(bf);
            });
        }
        public static void ShowShootData(Shoot shoot)
        {
            MainWindow.MainWindowInstance.Dispatcher.Invoke(() => {
                (MainWindow.MainWindowInstance.MainGrid.Children[0] as UC_Battlefield).ShowShoot(shoot);
            });
        }
        public static void ToEndScreen(string login, string p1FieldData, string p2FieldData)
        {
            MainWindow.MainWindowInstance.Dispatcher.Invoke(() => {
                MainWindow.MainWindowInstance.MainGrid.Children.Clear();
                UC_EndScreen endScreen = new UC_EndScreen();
                endScreen.Load(login == Settings.Login, 0, 0);
                endScreen.LoadField(p1FieldData, p2FieldData);
                MainWindow.MainWindowInstance.MainGrid.Children.Add(endScreen);
            });
        }
    }
}
