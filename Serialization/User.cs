using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Serialization
{
    [Serializable]
    public class User
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        public User(string name, string login, string password, string salt)
        {
            Name = name;
            Login = login;
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] data = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
                Password = Convert.ToBase64String(data);
            }
            Salt = salt;
        }

        public User() { }

        public override string ToString()
        {
            return $"{Name}, {Login}, {Password}, {Salt}";
        }
    }
}
