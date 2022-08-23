using Bookstore.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.ViewModels
{
    public class BookAuthorViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 5)]
        public string Description { get; set; }

        public string ImageName { get; set; }

        public IFormFile File { get; set; }

        [Required(ErrorMessage = "Please select a valid author")]
        public int AuthorId { get; set; }

        public List<AuthorViewModel> Authors { get; set; }
    }
}
