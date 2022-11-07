using System.IO;
using Newtonsoft.Json;

namespace SeaBattle
{
    public class Config
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Config()
        {
            Width = 800;
            Height = 450;
        }
    }


    public class Settings
    {
        public static string Login = "";
        public static string Password = "";
        public static string Path { get; set; } = "config.txt";
        public static Config Config { get; set; } = new Config();
        public static void Init(string path = "config.txt")
        {
            Path = path;
            if (File.Exists(path))
            {
                string text = File.ReadAllText(path);
                Config = JsonConvert.DeserializeObject<Config>(text);
            }
            else
            {
                File.Create(path);
                Config = new Config();
            }
            if (Config == null)
            {
                Config = new Config();
            }
            MainWindow.MainWindowInstance.Dispatcher.Invoke(() =>
            {
                MainWindow.MainWindowInstance.Height = Config.Height;
                MainWindow.MainWindowInstance.Width = Config.Width;
            });
        }
        public static void WriteInFile()
        {
            string text = JsonConvert.SerializeObject(Config);
            File.WriteAllText(Path, text);
        }

        public static void SignIn(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
    public struct Pair
    {
        public int First { get; set; }
        public int Second { get; set; }
    }
}
