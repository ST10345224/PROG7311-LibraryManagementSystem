namespace LibraryManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            LibraryManagementSystem library = new LibraryManagementSystem();

            // Add books
            library.AddBook("The Hitchhiker's Guide to the Galaxy", "Douglas Adams", "978-0345391803", 5);
            library.AddBook("Pride and Prejudice", "Jane Austen", "978-0141439518", 3);

            // Add users
            library.AddUser("user123", "Alice Smith", "alice.smith@example.com");
            library.AddUser("user456", "Bob Johnson", "bob.johnson@example.com");

            // Search for books
            Console.WriteLine("\nSearch results for 'Hitchhiker':");
            var searchResults = library.SearchBook("Hitchhiker");
            foreach (var book in searchResults)
            {
                Console.WriteLine($"{book.Title} by {book.Author}");
            }

            // Borrow a book
            library.BorrowBook("user123", "978-0345391803");
            var hitchhikerDetails = library.GetBookDetails("978-0345391803");
            Console.WriteLine($"Available copies of 'The Hitchhiker's Guide to the Galaxy': {hitchhikerDetails?.AvailableCopies}");

            // Check due date
            DateTime? dueDate = library.CheckDueDate("user123", "978-0345391803");
            if (dueDate.HasValue)
            {
                Console.WriteLine($"Due date for 'The Hitchhiker's Guide to the Galaxy' borrowed by user123: {dueDate.Value.ToShortDateString()}");
            }

            // Return a book
            library.ReturnBook("user123", "978-0345391803");
            hitchhikerDetails = library.GetBookDetails("978-0345391803");
            Console.WriteLine($"Available copies of 'The Hitchhiker's Guide to the Galaxy': {hitchhikerDetails?.AvailableCopies}");

            // Borrow another book
            library.BorrowBook("user456", "978-0141439518");

            // Simulate passing of time for fine calculation (for demonstration purposes)
            // In a real application, this wouldn't be needed in this way.
            var overdueDate = DateTime.Now.AddDays(20);
            var borrowDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 30);
            var overdueLoan = new Loan("user456", "978-0141439518");
            overdueLoan.BorrowDate = borrowDate;
            overdueLoan.DueDate = borrowDate.AddDays(14);
            library.loans.Remove(library.loans.Last()); // Remove the initially created loan
            library.loans.Add(overdueLoan);

            Console.WriteLine($"\nSimulating return after due date...");
            library.ReturnBook("user456", "978-0141439518");

            // Manage librarian/admin roles
            library.AddLibrarian("librarian1", "Eve Reader", "password123", "Librarian");
            library.AddLibrarian("admin1", "John Admin", "adminpass", "Admin");

            Console.WriteLine($"\nAuthentication check:");
            Console.WriteLine($"Is 'Eve Reader' a librarian? {library.AuthenticateUser("librarian1", "password123", "librarian")}");
            Console.WriteLine($"Is 'John Admin' an admin? {library.AuthenticateUser("admin1", "adminpass", "admin")}");
            Console.WriteLine($"Is 'Eve Reader' an admin? {library.AuthenticateUser("librarian1", "password123", "admin")}");
        }
    }
}
