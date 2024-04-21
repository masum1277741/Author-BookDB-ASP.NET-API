using asp.net_api_crud.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace asp.net_api_crud.Controllers
{
    public class AuthorsController : ApiController
    {
        private BookDbContext db = new BookDbContext();
        //GET:api/Authors
        [HttpGet]
        public IQueryable<Author>GetAuthors()
        {
            return db.Authors.Include(x=>x.Books).AsQueryable();
        }
        //GET:api/Authors/2
        [HttpGet]
        public IHttpActionResult GetAuthorById(int id)
        {
            var a = db.Authors.Include(x=>x.Books).FirstOrDefault(x=>x.AuthorId == id);
            if (a != null)
            {
                return Ok(a);
            }
            return BadRequest("Data not found!!");
        }
        [HttpPost]
        public IHttpActionResult PostAuthor(Author a)
        {
            if (ModelState.IsValid)
            {
                var author = new Author
                {
                    AuthorId = a.AuthorId,
                    AuthorName = a.AuthorName,
                };
                a.Books.ForEach(b =>
                author.Books.Add(new Book
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    PublishedDate = b.PublishedDate,
                    Price = b.Price,
                    Genre = b.Genre,
                    Onsale = b.Onsale,
                    Picture = b.Picture,
                    AuthorId = b.AuthorId,      
                })
                );
                db.Authors.Add( author );
                db.SaveChanges();
                return Ok(author);
            }
            return BadRequest("Invalid Data!!");
        }
        [Route("Image/Upload")]
        [HttpPost]
        public IHttpActionResult Upload()
        {
            var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
            if (file != null && file.ContentLength > 0)
            {
                string ext = Path.GetExtension(file.FileName);
                string f = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                string savePath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("/Images"), f);
                file.SaveAs(savePath);
                return Ok(f);
            }
            return BadRequest();
        }
        [HttpPut]
        public IHttpActionResult PutDevice(int id, Author model)
        {
            if (id != model.AuthorId) return BadRequest("Id Mislmatch!!");
            if (ModelState.IsValid)
            {
                var author = db.Authors.Include(x => x.Books).First(x => x.AuthorId == id);
                author.AuthorName = model.AuthorName;               
                db.Books.RemoveRange(author.Books);
                model.Books.ForEach(s =>
                {
                    author.Books.Add(new Book
                    {
                        Title = s.Title,
                        PublishedDate = s.PublishedDate,
                        Price = s.Price,
                        Genre = s.Genre,
                        Picture = s.Picture,
                    });
                });
                db.SaveChanges();
                return Ok(author);
            }
            return BadRequest("Data Invalid!!");
        }

        [HttpDelete]
        public IHttpActionResult DeleteAuthor(int id)
        {
            var d = db.Authors.FirstOrDefault(x => x.AuthorId == id);
            if (d == null) return NotFound();
            db.Authors.Remove(d);
            db.SaveChanges();
            return Ok("Delete");
        }
    }
}
