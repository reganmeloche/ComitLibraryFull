using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

using ComitLibrary.Models;

namespace ComitLibraryMvc.Models
{
    public class LoanViewModel
    {

        [Required]
        public Guid BookId { get; set; }

        [Required]
        public Guid PatronId { get; set; }
        public string Title {get;set;}

        public SelectList PatronList {get;set;}
    }
}
