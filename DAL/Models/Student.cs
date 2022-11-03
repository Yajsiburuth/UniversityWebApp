using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string NationalId { get; set; }
        public string GuardianName { get; set; }
        public int UserId { get; set; }
        public Status Status { get; set; }

    }
}
