using System;
using System.Drawing;

namespace BookStore.EntityFramework.DTOs
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Year { get; set; }
        public string Isbn { get; set; }
        public string Image { get; set; }
        public string Desc { get; set; }
    }
}
