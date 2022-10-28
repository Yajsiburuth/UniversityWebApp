using System;
using DAL.Models;

namespace DAL.ViewModels
{
    public class StudentSummary
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string NationalId { get; set; }
        public int UserId { get; set; }
        public Status Status { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public Grade result { get; set; }
    }
}