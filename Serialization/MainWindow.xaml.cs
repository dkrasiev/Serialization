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
                string filePath = ofd.FileName;
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    StreamReader sr = new StreamReader(fs);
                    _data = sr.ReadToEnd();
                }
                MessageBox.Show("Выбран файл: " + filePath, "Succes");
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
                _serializer.XMLSerialize(_data, ofd.FileName);
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
                _serializer.JSONSerialize(_data, ofd.FileName);
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
