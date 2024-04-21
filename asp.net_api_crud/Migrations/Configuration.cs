namespace asp.net_api_crud.Migrations
{
    using asp.net_api_crud.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<asp.net_api_crud.Models.BookDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(asp.net_api_crud.Models.BookDbContext context)
        {
            context.Authors.AddOrUpdate(x => x.AuthorId,
                new Author() { AuthorId =1, AuthorName="Bankimchandra Chatropaddhay"},
                new Author() { AuthorId = 2, AuthorName="Rabindranath Tagore"}  
                );
            context.Books.AddOrUpdate(x => x.BookId,
                new Book() { BookId = 1, Title = "AnandaMath", PublishedDate = new DateTime(1970, 01, 01), Price = 180.00m, Genre = "Novel", Onsale = true, Picture = "B1.jpg", AuthorId = 1 },
                new Book() { BookId = 2, Title = "Sheser Kobita", PublishedDate = new DateTime(1970, 01, 01), Price = 180.00m, Genre = "Novel", Onsale = true, Picture = "B3.jpg", AuthorId = 2 });
        }
    }
}
