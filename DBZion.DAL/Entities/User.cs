using System.Collections.Generic;

namespace DBZion.DAL.Entities
{
    public class User
    {
        public int UserID { get; set; }
        
        public string Surname { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string PhoneNumber { get; set; }

        public List<Order> Orders;

        public User()
        {

        }

        public User(string surname, string firstName, string middleName, string phoneNumber)
        {
            Surname = surname;
            FirstName = firstName;
            MiddleName = middleName;
            PhoneNumber = phoneNumber;
        }

        public string FullName
        {
            get
            {
                return $"{Surname} {FirstName} {MiddleName}";
            }
        }
    }
}
