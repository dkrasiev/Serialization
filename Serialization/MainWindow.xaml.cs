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

namespace Serialization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataSerializer _serializer;
        private object _data;

        public MainWindow()
        {
            InitializeComponent();
            _serializer = new DataSerializer();
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                LoadedFile.Text = ofd.FileName;
                using (FileStream fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
                {
                    StreamReader sr = new StreamReader(fs);
                    _data = sr.ReadToEnd();
                    LoadedData.Text = _data.ToString();
                }
                MessageBox.Show("Выбран файл: " + ofd.FileName, "Succes");
            }
            else
            {
                ShowError();
            }
        }

        private void BinarySerialization_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog ofd = new SaveFileDialog();

            if (ofd.ShowDialog() == true)
            {
                _serializer.BinarySerialize(_data, ofd.FileName);
            }
            else
            {
                ShowError();
            }
        }

        private void XMLSerialization_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog ofd = new SaveFileDialog();

            if (ofd.ShowDialog() == true)
            {
                _serializer.XmlSerialize(_data, ofd.FileName);
            }
            else
            {
                ShowError();
            }
        }

        private void JSONSerialization_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog ofd = new SaveFileDialog();

            if (ofd.ShowDialog() == true)
            {
                _serializer.JsonSerialize(_data, ofd.FileName);
            }
            else
            {
                ShowError();
            }
        }

        private void ShowError() 
        {
            MessageBox.Show("Error");
        }
    }
}
