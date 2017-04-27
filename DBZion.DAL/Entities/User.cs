﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBZion.DAL.Entities
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        
        [Required]
        public string Surname { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string MiddleName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public ICollection<Order> Orders { get; set; }

        public User()
        {
            Orders = new List<Order>();
        }

        public User(string surname, string firstName, string middleName, string phoneNumber)
        {
            Surname = surname;
            FirstName = firstName;
            MiddleName = middleName;
            PhoneNumber = phoneNumber;
            Orders = new List<Order>();
        }

        /// <summary>
        /// Возвращает полное имя пользователя в формате "Фамилия Имя Отчество".
        /// </summary>
        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{Surname} {FirstName} {MiddleName}";
            }
        }
    }
}
