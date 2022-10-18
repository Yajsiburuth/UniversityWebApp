using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniversityWebApp.Models;

namespace UniversityWebApp.ViewModels
{
    public class StudentViewModel
    {
        [Display(Name = "Firstname")]
        [Required (AllowEmptyStrings = false, ErrorMessage = "First Name cannot be empty")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Display(Name = "Lastname")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Lastname cannot be empty")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Display(Name = "Phone Number")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone number cannot be empty")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Date of birth")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Date of birth must be set")]
        [DataType(DataType.Date)]
        public DateTime DoB { get; set; }

        [Display(Name = "National Id card number")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "National Id card number cannot be empty")]
        [DataType(DataType.Text)]
        [MinLength(14), MaxLength(14)]
        public string Nid { get; set; }

        [Display(Name = "Subject")]
        public int SelectedSubject { get; set; }
        public IEnumerable<Subject> subjectList { get; set; }

        //[Display(Name = "Subject 1 Name")]
        //[DataType(DataType.Text)]
        //public string SubjectName1 { get; set; }

        //[Display(Name = "Subject 1 Result")]
        //[DataType(DataType.Text)]
        //[MinLength(0), MaxLength(1)]
        //public char SubjectResult1 { get; set; }

        [Display(Name = "Guardian's firstname")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Guardian's firstname cannot be empty")]
        [DataType(DataType.Text)]
        public string GuardianFirstName { get; set; }

        [Display(Name = "Guardian's lastname")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Guardian's lastname cannot be empty")]
        [DataType(DataType.Text)]
        public string GuardianLastName { get; set; }

        [Display(Name = "Guardian's phone Number")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Guardian's phone number cannot be empty")]
        [DataType(DataType.PhoneNumber)]
        public string GuardianPhoneNumber { get; set; }


        public List<Grade> Grades { get; set; }

    }
}