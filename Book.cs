using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int NumberOfCopies { get; set; }
        public int AvailableCopies { get; set; }

        public Book(string title, string author, string isbn, int numberOfCopies)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
            NumberOfCopies = numberOfCopies;
            AvailableCopies = numberOfCopies;
        }
    }
}
