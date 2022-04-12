using System;
using System.Collections.Generic;
using System.Windows;
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
        private App currentApp = (App)Application.Current;

        public MainWindow()
        {
            InitializeComponent();
            SerializationButtons.IsEnabled = false;
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "TXT file (*.txt) | *.txt";

            if (ofd.ShowDialog() == true)
            {
                LoadedFile.Content = ofd.FileName;

                using (FileStream fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
                {
                    StreamReader sr = new StreamReader(fs);
                    string[] lines = sr.ReadToEnd().Split('\n');
                    currentApp.ClearData();

                    foreach (string line in lines)
                    {
                        if (string.IsNullOrEmpty(line)) continue;

                        var regex = new Regex("(?<name>.+)\\*\\*(?<price>.+)\\*\\*(?<movieName>.+)\\*\\*(?<description>.+)\\*\\*(?<director>.+)\\*\\*(?<date>.+)\\*\\*(?<something>.+)");

                        Match match = regex.Match(line);

                        if (match.Success)
                        {
                            string name = match.Groups["name"].Value;
                            int price = Int32.Parse(match.Groups["price"].Value);
                            string movieName = match.Groups["movieName"].Value;
                            string description = match.Groups["description"].Value;
                            string director = match.Groups["director"].Value;
                            DateTime date = DateTime.Parse(match.Groups["date"].Value);
                            Movie movie = new Movie(name, price, movieName, description, director, date);

                            currentApp.AddData(movie);
                        }

                        //currentApp.AddUser(new Movie(name, login, password, salt));
                    }
                }

                SerializationButtons.IsEnabled = true;
                ShowSucces();
            }
            else
            {
                ShowError();
            }
        }

        private void BinarySerialization_Click(object sender, RoutedEventArgs e)
        {
            List<Movie> users = currentApp.GetData();
            if (users.Count == 0)
            {
                ShowError();
                return;
            }

            SaveFileDialog ofd = new SaveFileDialog()
            {
                FileName = "result",
                Filter = "BIN file (*.bin) | *.bin"
            };

            if (ofd.ShowDialog() == true)
            {
                if (DataSerializer.BinarySerialize(users, ofd.FileName))
                {
                    ShowSucces();
                }
            }
        }

        private void XMLSerialization_Click(object sender, RoutedEventArgs e)
        {
            List<Movie> users = currentApp.GetData();
            if (users.Count == 0)
            {
                ShowError();
                return;
            }

            SaveFileDialog ofd = new SaveFileDialog()
            {
                FileName = "result",
                Filter = "XML file (*.xml) | *.xml"
            };

            if (ofd.ShowDialog() == true)
            {
                if (DataSerializer.XmlSerialize(users, ofd.FileName))
                {
                    ShowSucces();
                }
            }
        }

        private void JSONSerialization_Click(object sender, RoutedEventArgs e)
        {
            List<Movie> users = currentApp.GetData();
            if (users.Count == 0)
            {
                ShowError();
                return;
            }

            SaveFileDialog ofd = new SaveFileDialog()
            {
                FileName = "result",
                Filter = "JSON file (*.json) | *.json"
            };

            if (ofd.ShowDialog() == true)
            {
                if (DataSerializer.JsonSerialize(users, ofd.FileName))
                {
                    ShowSucces();
                }
            }
        }

        private void ShowError(string errorMessage = "Операция не выполнена")
        {
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ShowSucces(string succesMessage = "Операция выполнена успешно!")
        {
            MessageBox.Show(succesMessage, "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
