using IAproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAproject.ViewModel
{
    public class AutherProfile
    {
        public List<Paper> paper { get; set; }
        public Auther auther { get; set; }
    }
}