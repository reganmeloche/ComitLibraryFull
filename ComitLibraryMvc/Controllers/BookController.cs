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

        public IActionResult Details(Guid id) {
            var book = _library.GetBook(id);

            return View(book);
        }

        public IActionResult Edit(Guid id) {
            // Get the book from the librarySystem
            var book = _library.GetBook(id);

            // build the view model
            var bookViewModel = new BookViewModel() {
                Author = book.Author,
                Title = book.Title,
                Year = book.Year,
            };


            // send the view model
            ViewBag.IsEditing = true;
            return View("Form", bookViewModel);
        }

        public IActionResult Form() {
            ViewBag.IsEditing = false;
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(BookViewModel newBook) {

            if (ModelState.IsValid) {
                var bookToCreate = new Book() {
                    Title = newBook.Title,
                    Author = newBook.Author,
                    IsCheckedOut = false,
                    Id = Guid.NewGuid(),
                    Year = newBook.Year
                };
                _library.AddNewBook(bookToCreate);

                return RedirectToAction("Index");
            } else {
                return View("Form", newBook);
            }
        }


        [HttpPost]
        public IActionResult Update(BookViewModel updatedBook) {
            if (ModelState.IsValid) {
                var existingBook = _library.GetBook(updatedBook.Id.Value);
                var book = new Book() {
                    Title = updatedBook.Title,
                    Author = updatedBook.Author,
                    IsCheckedOut = existingBook.IsCheckedOut,
                    Id = updatedBook.Id.Value,
                    Year = updatedBook.Year,
                };
                _library.UpdateBook(book);
                return RedirectToAction("Index");
            } else {
                ViewBag.IsEditing = true;
                return View("Form", updatedBook);
            }

        }

        [HttpPost]
        public IActionResult Delete(Guid id) {
            _library.DeleteBookById(id);
            return RedirectToAction("Index");
        }


        public IActionResult Search() {
            var search = new SearchViewModel() {
                Title = "",
                SearchResults = new List<Book>()
            };
            return View(search);
        }

        [HttpPost]
        public IActionResult Search(SearchViewModel search) {
            search.SearchResults = _library.SearchForBook(search.Title);
            return View(search);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
