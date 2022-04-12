using System.Collections.Generic;
using System.Windows;

namespace Serialization
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private List<Movie> data = new List<Movie>();

        public void AddData(Movie user)
        {
            data.Add(user);
        }

        public List<Movie> GetData()
        {
            return new List<Movie>(data);
        }

        public void ClearData()
        {
            data.Clear();
        }
    }
}
