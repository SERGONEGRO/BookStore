using System;
using System.Drawing;

namespace BookStore.Domain.Models
{
    public class Book
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Author { get; }
        public string Year { get; }
        public string Isbn { get; }
        public string Image { get; }
        public string Desc { get; }
        public Book(Guid id, string name, string author, string year, string isbn, string desc)
        {
            Id = id;
            Name = name;
            Author = author;
            Year = year;
            Isbn = isbn;
            Image = "..//some-location";
            Desc = desc;
        }
    }
}
