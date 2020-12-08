using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ComitLibraryMvc.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

using ComitLibrary;
using ComitLibrary.Models;

namespace ComitLibraryMvc.Controllers
{
    [Authorize]
    public class LoanController : Controller
    {
        private LibrarySystem _library;
        private UserManager<IdentityUser> _userManager;

        public LoanController(LibrarySystem library, UserManager<IdentityUser> userManager)
        {
            _library = library;
            _userManager = userManager;
        }

        public IActionResult Index() {
            var loans = _library.GetAllActiveLoans(UserId());
            return View(loans);
        }

        public IActionResult Create(Guid id) {
            var patrons = _library.GetAllPatrons(UserId());
            var patronList = patrons.Select(x => new {x.Id, x.FullName}).ToList();
            var selectList = new SelectList(patronList, "Id","FullName");
            
            var book = _library.GetBook(id, UserId());

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
                _library.CheckoutBook(newLoan.PatronId, newLoan.BookId, UserId());
                return RedirectToAction("Index", "Book");
            } else {
                return View(newLoan);
            }
        }

        public IActionResult Return(Guid id) { 
            _library.ReturnBook(id, UserId());
            return RedirectToAction("Details", "Book", new { id });
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
