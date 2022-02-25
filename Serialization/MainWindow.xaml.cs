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
using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace Serialization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataSerializer _serializer;
        private readonly List<User> _users = new();

        public MainWindow()
        {
            InitializeComponent();
            _serializer = new DataSerializer();
        }

        private async void Load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new();
            if (ofd.ShowDialog() == true)
            {
                LoadedFile.Text = ofd.FileName;

                using (FileStream fs = new(ofd.FileName, FileMode.Open, FileAccess.Read))
                {
                    StreamReader sr = new(fs);
                    string[] data = sr.ReadToEnd().Split('\n');

                    int k = 0;
                    foreach (string line in data)
                    {
                        k++;
                        if (String.IsNullOrEmpty(line)) continue;

                        string name = Regex.Match(line, "(Имя - )[А-я]+").Value.Replace("Имя - ", "");
                        string login = Regex.Match(line, "(login - )[A-zА-я]*").Value.Replace("login - ", "");
                        string password = Regex.Match(line, "(\\s\\s)\\w*").Value.Trim();
                        string salt = Regex.Match(line, "\\d+").Value;

                        _users.Add(new User(name, login, password, salt));

                        if (k % 1000000 == 0) ShowSucces("Миллион");
                    }
                }

                ShowSucces();
            }
            else
            {
                ShowError();
            }
        }

        private void BinarySerialization_Click(object sender, RoutedEventArgs e)
        {
            if (_users.Count == 0)
            {
                ShowError();
                return;
            }

            SaveFileDialog ofd = new();

            if (ofd.ShowDialog() == true)
            {
                _serializer.BinarySerialize(_users, ofd.FileName);
            }
        }

        private void XMLSerialization_Click(object sender, RoutedEventArgs e)
        {
            if (_users.Count == 0)
            {
                ShowError();
                return;
            }

            SaveFileDialog ofd = new();

            if (ofd.ShowDialog() == true)
            {
                _serializer.XmlSerialize(typeof(List<User>), _users, ofd.FileName);
            }
        }

        private void JSONSerialization_Click(object sender, RoutedEventArgs e)
        {
            if (_users.Count == 0)
            {
                ShowError();
                return;
            }

            SaveFileDialog ofd = new();

            if (ofd.ShowDialog() == true)
            {
                _serializer.JsonSerialize(typeof(List<User>), _users, ofd.FileName);
            }
        }

        private void ShowError(string errorMessage="Операция не выполнена")
        {
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ShowSucces(string succesMessage="Операция выполнена успешно!")
        {
            MessageBox.Show(succesMessage, "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
