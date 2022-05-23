using IAproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAproject.ViewModel
{
    public class PaperAuthors
    {
        public List<Auther> auther{ get; set; }
        public Paper paper { get; set; }
    }
}