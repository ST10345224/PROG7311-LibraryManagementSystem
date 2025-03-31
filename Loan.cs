using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    public class Loan
    {
        public string UserId { get; set; }
        public string ISBN { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal FineAmount { get; set; }

        public Loan(string userId, string isbn)
        {
            UserId = userId;
            ISBN = isbn;
            BorrowDate = DateTime.Now;
            DueDate = BorrowDate.AddDays(14); // Example: 2 weeks loan period
            ReturnDate = null;
            FineAmount = 0;
        }
    }
}
