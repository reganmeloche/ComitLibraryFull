using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ComitLibraryMvc.Models;

using ComitLibrary;
using ComitLibrary.Models;

namespace ComitLibraryMvc.Controllers
{
    public class BookController : Controller
    {
        private LibrarySystem _library;

        public BookController(LibrarySystem library)
        {
            _library = library;
        }

        // GET Book/Index
        public IActionResult Index()
        {
            List<Book> books = _library.GetAllBooks();
            return View(books);
        }

        public IActionResult Form() {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(Book newBook) {
            _library.AddNewBook(newBook);
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
