using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IAproject.Models
{
    public class Auther
    {
        [Required(ErrorMessage = "You must enter your First Name")]
        [Display(Name = "First Name")]
        public String FirstName { get; set; }
        [Required(ErrorMessage = "You must enter your Last Name")]
        [Display(Name = "Last Name")]
        public String LastName { get; set; }
        [Required(ErrorMessage = "You must enter your Email")]
        [Display(Name = "Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$")]
        public String Email { get; set; }
        [Required(ErrorMessage = "You must enter your Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        //[RegularExpression(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$")]
        public String Password { get; set; }
        public String University { get; set; }
        public String Department { get; set; }
        [DataType(DataType.PhoneNumber)]
        public String mobile { get; set; }
        [DataType(DataType.Upload)]
        [Display(Name = "Upload Profile Image")]
        [Required(ErrorMessage = "Please choose Image to upload.")]
        public String ProfileImage { get; set; }
       // [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
      
        public Guid Id { get; set; }

        public virtual ICollection<Paper> papers { get; set; }

    }
}