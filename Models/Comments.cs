﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAproject.Models
{
    public class Comments
    {
        public int id { get; set; }
        public Guid paperID { get; set; }
        public Guid AutherID { get; set; }
        public String Comment { get; set; }
    }
}