using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    public class LibraryManagementSystem
    {
        private Dictionary<string, Book> books = new Dictionary<string, Book>();
        private Dictionary<string, User> users = new Dictionary<string, User>();
        public List<Loan> loans = new List<Loan>();
        private Dictionary<string, Librarian> librarians = new Dictionary<string, Librarian>();

        public void AddBook(string title, string author, string isbn, int numberOfCopies)
        {
            if (!books.ContainsKey(isbn))
            {
                books.Add(isbn, new Book(title, author, isbn, numberOfCopies));
                Console.WriteLine($"Book '{title}' added.");
            }
            else
            {
                Console.WriteLine($"Book with ISBN {isbn} already exists.");
            }
        }

        public Book GetBookDetails(string isbn)
        {
            if (books.ContainsKey(isbn))
            {
                return books[isbn];
            }
            return null;
        }

        public List<Book> SearchBook(string query, string searchBy = "title")
        {
            query = query.ToLower();
            List<Book> results = new List<Book>();
            foreach (var book in books.Values)
            {
                if (searchBy.ToLower() == "title" && book.Title.ToLower().Contains(query))
                {
                    results.Add(book);
                }
                else if (searchBy.ToLower() == "author" && book.Author.ToLower().Contains(query))
                {
                    results.Add(book);
                }
            }
            return results;
        }

        public void AddUser(string userId, string name, string contactInfo)
        {
            if (!users.ContainsKey(userId))
            {
                users.Add(userId, new User(userId, name, contactInfo));
                Console.WriteLine($"User '{name}' added.");
            }
            else
            {
                Console.WriteLine($"User with ID {userId} already exists.");
            }
        }

        public User GetUserDetails(string userId)
        {
            if (users.ContainsKey(userId))
            {
                return users[userId];
            }
            return null;
        }

        public void BorrowBook(string userId, string isbn)
        {
            if (users.ContainsKey(userId) && books.ContainsKey(isbn) && books[isbn].AvailableCopies > 0)
            {
                loans.Add(new Loan(userId, isbn));
                books[isbn].AvailableCopies--;
                Console.WriteLine($"Book '{books[isbn].Title}' borrowed by user {userId}. Due date: {loans.Last().DueDate.ToShortDateString()}");
            }
            else if (!users.ContainsKey(userId))
            {
                Console.WriteLine($"User with ID {userId} not found.");
            }
            else if (!books.ContainsKey(isbn))
            {
                Console.WriteLine($"Book with ISBN {isbn} not found.");
            }
            else
            {
                Console.WriteLine($"Book '{books[isbn].Title}' is currently unavailable.");
            }
        }

        public void ReturnBook(string userId, string isbn)
        {
            Loan loan = loans.FirstOrDefault(l => l.UserId == userId && l.ISBN == isbn && l.ReturnDate == null);
            if (loan != null)
            {
                loan.ReturnDate = DateTime.Now;
                if (loan.ReturnDate > loan.DueDate)
                {
                    TimeSpan overdueDays = loan.ReturnDate.Value - loan.DueDate;
                    loan.FineAmount = overdueDays.Days * 0.50m; // Example: $0.50 fine per overdue day
                    Console.WriteLine($"Book '{books[isbn].Title}' returned. Fine: ${loan.FineAmount:F2}");
                }
                else
                {
                    Console.WriteLine($"Book '{books[isbn].Title}' returned.");
                }
                if (books.ContainsKey(isbn))
                {
                    books[isbn].AvailableCopies++;
                }
            }
            else
            {
                Console.WriteLine($"No active loan found for user {userId} and ISBN {isbn}.");
            }
        }

        public DateTime? CheckDueDate(string userId, string isbn)
        {
            Loan loan = loans.FirstOrDefault(l => l.UserId == userId && l.ISBN == isbn && l.ReturnDate == null);
            return loan?.DueDate;
        }

        public decimal CheckFine(string userId, string isbn)
        {
            Loan loan = loans.FirstOrDefault(l => l.UserId == userId && l.ISBN == isbn && l.ReturnDate == null);
            if (loan != null && DateTime.Now > loan.DueDate)
            {
                TimeSpan overdueDays = DateTime.Now - loan.DueDate;
                return overdueDays.Days * 0.50m;
            }
            return 0;
        }

        public void AddLibrarian(string userId, string name, string password, string role)
        {
            if (!librarians.ContainsKey(userId))
            {
                librarians.Add(userId, new Librarian(userId, name, password, role));
                Console.WriteLine($"Librarian '{name}' added as {role}.");
            }
            else
            {
                Console.WriteLine($"Librarian with ID {userId} already exists.");
            }
        }

        public bool AuthenticateUser(string userId, string password, string role)
        {
            if (librarians.ContainsKey(userId))
            {
                return librarians[userId].Password == password && librarians[userId].Role.ToLower() == role.ToLower();
            }
            return false;
        }
    }
}
