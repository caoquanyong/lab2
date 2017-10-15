using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace lab2.Models
{
    public class AuthorDB : DbContext
    {
        public AuthorDB():base("name=DefaultConnection"){ }
        public DbSet<Author> Authors { get; set; }
    }
}