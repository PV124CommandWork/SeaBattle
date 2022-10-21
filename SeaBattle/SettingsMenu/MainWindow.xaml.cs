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
using System.IO;
using System.Text.Json;
namespace SettingsMenu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        public Pair showDialog()
        {
            base.ShowDialog();
            return new Pair { first = int.Parse(WidthBox.Text), second = int.Parse(HeightBox.Text)};
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            WidthBox.Text = "1024";
            HeightBox.Text = "1024";
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    public class Config { 
        public int width { get; set; }
        public int height { get; set; }
        public Config()
        {
            width = 1024;
            height = 1024;
        }
    }


    public class Settings
    {
        
        public string path { get; set; } = "config.txt";
        public Config config { get; set; } = new Config();
        private MainWindow SettingsMenu = new MainWindow();
        public Settings(string path = "config.txt")
        {
            if (File.Exists(path))
            {
                string text = File.ReadAllText(path);
                config = JsonSerializer.Deserialize<Config>(text);
            }
            else
            {
                File.Create(path);
                config = new Config(); 
            }
            if(config == null)
            {
                config = new Config();
            }
            SettingsMenu.HeightBox.Text = config.height.ToString();
            SettingsMenu.WidthBox.Text = config.width.ToString();
            
        }
        public void writeInFile()
        {
            string text = JsonSerializer.Serialize(config);
            File.WriteAllText(path, text);
        }

        public Pair showSettingsWindow()
        {
            Pair result = SettingsMenu.showDialog();
            writeInFile();
            return result;
        }
    }
    public struct Pair
    {
        public int first { get; set; }
        public int second { get; set; }
    }
}
