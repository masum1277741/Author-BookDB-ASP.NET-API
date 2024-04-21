using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace asp.net_api_crud.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        [Required,StringLength(50),Display(Name = "Author Name")]
        public string AuthorName { get; set; }
        public ICollection<Book> Books { get; set; }= new List<Book>();
    }
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        [Required, Display(Name = "Published Date"),Column(TypeName ="date"),DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime PublishedDate { get; set; }
        [Required,Column(TypeName ="money")]
        public decimal Price { get; set; }
        public string Genre { get; set; }
        public bool Onsale { get; set; }
        public string Picture { get; set; }
        //fk
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
    public class BookDbContext : DbContext
    {
        public DbSet<Book> Books { get; set;}
        public DbSet<Author> Authors { get; set;}
    }
}