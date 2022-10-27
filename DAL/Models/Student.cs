﻿using System;

namespace DAL.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DoB { get; set; }
        public int GuardianId { get; set; }
        public string Nid { get; set; }
        public int UserId { get; set; }
        public Status Status { get; set; }

    }
}