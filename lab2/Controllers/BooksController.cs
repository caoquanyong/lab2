using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using lab2.Models;

namespace lab2.Controllers
{
    public class BooksController : Controller
    {
        private BookDB db = new BookDB();
        private AuthorDB Db = new AuthorDB();
        private static Book bb = new Book();
        // GET: Books
        public ActionResult Index(string searchString)
        {
            var authors = Db.Authors.ToList();
                         
            var books = db.Books.ToList();

            if (!String.IsNullOrEmpty(searchString))
            {

                for (int i= books.Count - 1; i >= 0; i--)
                {
                    Boolean exist = false;
                    foreach (var x in authors)
                    {
                        if (books[i].AuthorID==x.AuthorID &&x.Name.Contains(searchString))
                        {
                            exist = true;
                            break;
                        }

                    }
                    if (!exist)
                    {
                        books.RemoveAt(i);
                    }
                }
            }
                    
              
            return View(books);
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            var authors = from m in Db.Authors
                          select m;
            foreach (var x in authors)
            {

                if (x.AuthorID.Equals(book.AuthorID))
                {
                    ViewBag.aauthor =x;
                }
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ISBN,Title,AuthorID,Publisher,PublishDate,Price")] Book book)
        {
            bb = book;
            var authors = from m in Db.Authors
                          select m;
            if (ModelState.IsValid)
            { 
                foreach (var x in authors)
                 {
               
                
                    if (x.AuthorID.Equals(book.AuthorID))
                    {
                        db.Books.Add(book);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Create1");

                    }
                }
                
                return RedirectToAction("Create1");
            }
             
            return View(book);
       }




        public ActionResult Create1()
        {
            return View();
        }

        // POST: Books/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create1([Bind(Include = "ID,AuthorID,Name,Age,Country")] Author author)
        {
            author.AuthorID = bb.AuthorID;


            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    db.Books.Add(bb);
                    db.SaveChanges();
                }
                Db.Authors.Add(author);
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
 
            return View(author);
        }






        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);

            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        // POST: Books/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ISBN,Title,AuthorID,Publisher,PublishDate,Price")] Book book)
        {
            bb = book;
            var authors = from m in Db.Authors
                          select m;
            if (ModelState.IsValid)
            {
                foreach (var x in authors)
                {
                    if (x.AuthorID.Equals(book.AuthorID))
                    {
                        db.Entry(book).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Create1");
                    }
                }
             }
            return View(book);
            }



        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
