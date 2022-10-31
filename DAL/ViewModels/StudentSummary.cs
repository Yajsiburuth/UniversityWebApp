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
        public string GuardianName { get; set; }
        public string NationalId { get; set; }
        public int UserId { get; set; }
        public string SubjectsTaken { get; set; }
        public int TotalResult { get; set; }
        public string Status { get; set; }
    }
}