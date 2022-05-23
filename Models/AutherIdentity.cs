using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using Microsoft.AspNet.Identity.EntityFramework;

namespace IAproject.Models
{
    public class AutherIdentity :IdentityDbContext<myIdentityUser>
    {
        public AutherIdentity() : base("DefaultConnectionz")
        {

        }

        public System.Data.Entity.DbSet<IAproject.Models.Auther> Authers { get; set; }

        public System.Data.Entity.DbSet<IAproject.Models.Paper> Papers { get; set; }

        public System.Data.Entity.DbSet<IAproject.Models.papersAuther> papersAuthers { get; set; }

        public System.Data.Entity.DbSet<IAproject.Models.likes> likes { get; set; }

        public System.Data.Entity.DbSet<IAproject.Models.Comments> Comments { get; set; }

        //  public System.Data.Entity.DbSet<IAproject.Models.papersAuther> papersAuhters { get; set; }
    }

    public class myIdentityUser : IdentityUser {

    }

}