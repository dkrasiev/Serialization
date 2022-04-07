﻿using System;
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
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new();

            if (ofd.ShowDialog() == true)
            {
                LoadedFile.Text = ofd.FileName;

                using (FileStream fs = new(ofd.FileName, FileMode.Open, FileAccess.Read))
                {
                    StreamReader sr = new(fs);
                    string[] lines = sr.ReadToEnd().Split('\n');
                    currentApp.ClearUsers();

                    foreach (string line in lines)
                    {
                        if (string.IsNullOrEmpty(line)) continue;

                        var regex = Regex.Match(line, "Имя.- (?<name>\\S+), login - (?<login>\\S+), password -  (?<password>\\S+), Соль - (?<salt>\\d+)");

                        string name = regex.Groups["name"].Value;
                        string login = regex.Groups["login"].Value;
                        string password = regex.Groups["password"].Value;
                        string salt = regex.Groups["salt"].Value;

                        currentApp.AddUser(new User(name, login, password, salt));
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
            List<User> users = currentApp.GetUsers();
            if (users.Count == 0)
            {
                ShowError();
                return;
            }

            SaveFileDialog ofd = new()
            {
                FileName = "result",
                Filter = "BIN file (*.bin)|.bin"
            };

            if (ofd.ShowDialog() == true)
            {
                DataSerializer.BinarySerialize(users, ofd.FileName);
            }
        }

        private void XMLSerialization_Click(object sender, RoutedEventArgs e)
        {
            List<User> users = currentApp.GetUsers();
            if (users.Count == 0)
            {
                ShowError();
                return;
            }

            SaveFileDialog ofd = new()
            {
                FileName= "result",
                Filter = "XML file (*.xml)|.xml"
            };

            if (ofd.ShowDialog() == true)
            {
                DataSerializer.XmlSerialize(users, ofd.FileName);
            }
        }

        private void JSONSerialization_Click(object sender, RoutedEventArgs e)
        {
            List<User> users = currentApp.GetUsers();
            if (users.Count == 0)
            {
                ShowError();
                return;
            }

            SaveFileDialog ofd = new()
            {
                FileName = "result",
                Filter = "JSON file (*.json)|.json"
            };

            if (ofd.ShowDialog() == true)
            {
                DataSerializer.JsonSerialize(users, ofd.FileName);
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
