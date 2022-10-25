using System;
using UniversityWebApp.Models;

namespace UniversityWebApp.ViewModels
{
    public class StudentSummary
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DoB { get; set; }
        public string Nid { get; set; }
        public int UserId { get; set; }
        public Status status { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public ResultGrade result { get; set; }
        public int GuardianId { get; set; }
        public string GuardianFirstName { get; set; }
        public string GuardianLastName { get; set; }
    }
}