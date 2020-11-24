using System;
using System.ComponentModel.DataAnnotations;

namespace ComitLibraryMvc.Models
{
    public class PatronViewModel
    {
        public Guid? Id { get; set; }

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }
    }
}
