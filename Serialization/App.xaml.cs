using System.Collections.Generic;
using System.Windows;

namespace Serialization
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private List<User> users = new List<User>();

        public void AddUser(User user)
        {
            users.Add(user);
        }

        public List<User> GetUsers()
        {
            return new List<User>(users);
        }

        public void ClearUsers()
        {
            users.Clear();
        }
    }
}
