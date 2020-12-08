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
    public class PatronController : Controller
    {
        private LibrarySystem _library;
        private UserManager<IdentityUser> _userManager;

        public PatronController(LibrarySystem library, UserManager<IdentityUser> userManager)
        {
            _library = library;
            _userManager = userManager;
        }

        public IActionResult Index() {
            var patrons = _library.GetAllPatrons(UserId());
            return View(patrons);
        }

        public IActionResult Create(Guid id) {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(PatronViewModel newPatron) {
            if (ModelState.IsValid) {
                var patron = new Patron(Guid.NewGuid(), newPatron.FirstName, newPatron.LastName, UserId());
                _library.AddPatron(patron);
                return RedirectToAction("Index");
            } else {
                return View(newPatron);
            }
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
