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
    public class LoanController : Controller
    {
        private LibrarySystem _library;

        public LoanController(LibrarySystem library)
        {
            _library = library;
        }

        public IActionResult Create(Guid id) {
            var loanViewModel = new LoanViewModel() {
                BookId = id,
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
