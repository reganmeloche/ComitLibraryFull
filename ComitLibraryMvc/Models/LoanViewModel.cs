using System;
using System.ComponentModel.DataAnnotations;

namespace ComitLibraryMvc.Models
{
    public class LoanViewModel
    {

        [Required]
        public Guid BookId { get; set; }

        [Required]
        public Guid PatronId { get; set; }
    }
}
