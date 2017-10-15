using System.Data.Entity;

namespace lab2.Models
{
    public class BookDB : DbContext
    {
        public BookDB():base("name=DefaultConnection"){}
        public DbSet<Book> Books{ get; set; }
    }
}