using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    public class Librarian
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // Librarian or Admin

        public Librarian(string userId, string name, string password, string role)
        {
            UserId = userId;
            Name = name;
            Password = password;
            Role = role;
        }
    }
}
