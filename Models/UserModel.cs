using System;
using System.ComponentModel.DataAnnotations;

namespace login_registration.Models
{
    public class User : BaseEntity
    {
        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$", 
         ErrorMessage = "First name can only contain letters")]
        [Display(Name = "First Name")]
        public string firstname {get; set;}
        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$", 
         ErrorMessage = "Last name can only contain letters")]
        [Display(Name = "Last Name")]
        public string lastname {get; set;}

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string email {get;set;}
        [Required]
        [MinLength(8)]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string password {get;set;}
        [Required]
        [MinLength(8)]
        [Compare("password", ErrorMessage="Password and Password Confirmation must match")]
        [Display(Name = "Password Confirmation")]
        [DataType(DataType.Password)]
        public string passwordconf {get;set;}

    }
}