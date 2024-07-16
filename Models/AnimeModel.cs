using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace AnimeWebApp.Models
{
    public class AnimeModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(200)]
        public string? JapaneseTitle { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        [StringLength(100)]
        public string? Type { get; set; }

        [StringLength(100)]
        public string? Studios { get; set; }

        public DateTime? DateAired { get; set; }

        [StringLength(50)]
        public string? Status { get; set; }

        [StringLength(200)]
        public string? Genre { get; set; }

        [Range(0, 10)]
        public double? Score { get; set; }

        [Range(0, 10)]
        public double? Rating { get; set; }

        [StringLength(50)]
        public string? Duration { get; set; }

        [StringLength(50)]
        public string? Quality { get; set; }

        public int? Views { get; set; }

        public int? Votes { get; set; }

        [NotMapped]
        [Display(Name = "Upload File")]
        public IFormFile ImageFile { get; set; }

        public byte[]? ImageData { get; set; }

        [StringLength(100)]
        public string? ImageMimeType { get; set; }
    }
}
