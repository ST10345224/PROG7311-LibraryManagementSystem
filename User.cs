using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    public class User
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string ContactInfo { get; set; }

        public User(string userId, string name, string contactInfo)
        {
            UserId = userId;
            Name = name;
            ContactInfo = contactInfo;
        }
    }
}
