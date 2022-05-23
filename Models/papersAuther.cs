using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAproject.Models
{
    public class papersAuther
    {
        public int id { get; set; }
        public Guid paperID { get; set; }
        public Guid AuhterID { get; set; }
    }
}