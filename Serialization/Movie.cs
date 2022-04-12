using System;
using System.Text;
using System.Security.Cryptography;

namespace Serialization
{
    [Serializable]
    public class Movie
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string MovieName { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public DateTime Date { get; set; }

        public Movie(string name, int price, string movieName, string description, string director, DateTime date)
        {
            Name = name;
            Price = price;
            MovieName = movieName;
            Description = description;
            Director = director;
            Date = date;
        }

        private Movie() { }

        public override string ToString()
        {
            return $"{Name}, {Price}, {MovieName}, {Description}, {Director}, {Date}";
        }
    }
}
