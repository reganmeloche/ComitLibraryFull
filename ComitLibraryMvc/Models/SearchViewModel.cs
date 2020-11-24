using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using ComitLibrary.Models;

namespace ComitLibraryMvc.Models
{
    public class SearchViewModel
    {
        [Required]
        public string Title { get; set; }
        public List<Book> SearchResults {get;set;}

    }
}
