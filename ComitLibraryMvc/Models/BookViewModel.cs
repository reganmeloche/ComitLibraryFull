using System;
using System.ComponentModel.DataAnnotations;

namespace ComitLibraryMvc.Models
{
    public class BookViewModel
    {
        public Guid? Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Title { get; set; }

        [Required]
        [StringLength(30)]
        public string Author { get; set; }
    }
}
