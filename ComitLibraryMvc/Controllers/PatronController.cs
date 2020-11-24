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
    public class PatronController : Controller
    {
        private LibrarySystem _library;

        public PatronController(LibrarySystem library)
        {
            _library = library;
        }

        public IActionResult Index() {
            var patrons = _library.GetAllPatrons();
            return View(patrons);
        }

        public IActionResult Create(Guid id) {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(PatronViewModel newPatron) {
            if (ModelState.IsValid) {
                var patron = new Patron(Guid.NewGuid(), newPatron.FirstName, newPatron.LastName);
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
    }
}
