using IAproject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

public class DefaultConnection : DbContext
{

    public DbSet<papersAuther> papersAuhters { get; set; }
    public DbSet<Auther> auther { get; set; }
    public DbSet<Paper> paper { get; set; }
    public DbSet<likes> likes { get; set; }
    public DbSet<dislike> dislikes { get; set; }
    public DbSet<Comments> comments { get; set; }

}