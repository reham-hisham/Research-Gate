using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IAproject.Models
{
    public class Paper
    {
      
        public Guid id { get; set; }
        [Required(ErrorMessage = "You must enter your paper Title")]
        [Display(Name = "Title")]
        public String Title { get; set; }
        [Required(ErrorMessage = "You must enter your Content")]
        [Display(Name = "Content")]
        public String Content { get; set; }
        public virtual ICollection<Auther> authers { get; set; }

    }
}