using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ComitLibraryMvc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

using ComitLibrary;
using ComitLibrary.Models;

namespace ComitLibraryMvc.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private LibrarySystem _library;
        private UserManager<IdentityUser> _userManager;

        public BookController(LibrarySystem library, UserManager<IdentityUser> userManager)
        {
            _library = library;
            _userManager = userManager;

        }

        // GET Book/Index
        public IActionResult Index()
        {
            List<Book> books = _library.GetAllBooks(UserId());
            return View(books);
        }

        public IActionResult Details(Guid id) {
            
            var book = _library.GetBook(id, UserId());

            return View(book);
        }

        public IActionResult Edit(Guid id) {
            // Get the book from the librarySystem
            var book = _library.GetBook(id, UserId());

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
                    Year = newBook.Year,
                    UserId = UserId(),
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
                var existingBook = _library.GetBook(updatedBook.Id.Value, UserId());
                var book = new Book() {
                    Title = updatedBook.Title,
                    Author = updatedBook.Author,
                    IsCheckedOut = existingBook.IsCheckedOut,
                    Id = updatedBook.Id.Value,
                    Year = updatedBook.Year,
                    UserId = UserId()
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
            _library.DeleteBookById(id, UserId());
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
            search.SearchResults = _library.SearchForBook(search.Title, UserId());
            return View(search);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private Guid UserId() {
            string stringUserId = _userManager.GetUserId(User);
            return Guid.Parse(stringUserId);
        }
    }
}
