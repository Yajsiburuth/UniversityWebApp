using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Models;

namespace DAL.ViewModels
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
        [Range(typeof(DateTime), "1/1/1900", "1/1/2006", ErrorMessage = "You must be at least 17 years old but not immortal")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "National Id card number")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "National Id card number cannot be empty")]
        [DataType(DataType.Text)]
        [RegularExpression("[A-Z][0-9]{12}[A-Z0-9]", ErrorMessage = "Invalid National Id format")]
        public string NationalId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Guardian name must be set")]
        [DataType(DataType.Text)]
        public string GuardianName { get; set; }

        [Display(Name = "SubjectIdList")]
        [Required(ErrorMessage = "You need to choose at least 1 Subject and Max 3 Subjects"), MinLength(1), MaxLength(3)]
        public string[] SubjectId { get; set; }

        [Required(ErrorMessage = "You need to insert at least 1 Grade and Max 3 Grades for each Subject"), MinLength(1), MaxLength(3)]
        public string[] Grade {get; set; }

    }
}