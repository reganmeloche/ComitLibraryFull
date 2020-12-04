using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ComitLibraryMvc.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

using ComitLibrary;
using ComitLibrary.Models;

namespace ComitLibraryMvc.Controllers
{
    public class LoanController : Controller
    {
        private LibrarySystem _library;

        public LoanController(LibrarySystem library)
        {
            _library = library;
        }

        public IActionResult Index() {
            var loans = _library.GetAllActiveLoans();
            return View(loans);
        }

        public IActionResult Create(Guid id) {
            var patrons = _library.GetAllPatrons();
            var patronList = patrons.Select(x => new {x.Id, x.FullName}).ToList();
            var selectList = new SelectList(patronList, "Id","FullName");
            
            var book = _library.GetBook(id);

            var loanViewModel = new LoanViewModel() {
                BookId = id,
                PatronList = selectList,
                Title = $"{book.Title} ({book.Author})"
            };
            return View(loanViewModel);
        }
        
        [HttpPost]
        public IActionResult Create(LoanViewModel newLoan) {
            if (ModelState.IsValid) {
                _library.CheckoutBook(newLoan.PatronId, newLoan.BookId);
                return RedirectToAction("Index", "Book");
            } else {
                return View(newLoan);
            }
        }

        public IActionResult Return(Guid id) { 
            _library.ReturnBook(id);
            return RedirectToAction("Details", "Book", new { id });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
