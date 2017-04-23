using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBZion.DAL.Entities
{
    public class User
    {
        public int UserID { get; set; }
        
        public string Surname { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public List<Order> Orders;

        public User()
        {

        }

        public User(string surname, string firstName, string middleName)
        {
            Surname = surname;
            FirstName = firstName;
            MiddleName = middleName;
        }
    }
}
