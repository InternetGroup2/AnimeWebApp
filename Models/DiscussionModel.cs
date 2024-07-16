using System;
using System.ComponentModel.DataAnnotations;

namespace AnimeWebApp.Models
{
    public class DiscussionModel
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "Username cannot exceed 100 characters.")]
        [Display(Name = "Username")]
        public string UserName { get; set; }  // 用户名不强制要求，可以为空

        [Required(ErrorMessage = "Please enter a topic.")]
        [StringLength(200, ErrorMessage = "Topic cannot exceed 200 characters.")]
        [Display(Name = "Topic")]
        public string Topic { get; set; }

        [Required(ErrorMessage = "Content cannot be empty.")]
        [StringLength(4000, ErrorMessage = "Content cannot exceed 4000 characters.")]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Post Time")]
        public DateTime PostTime { get; set; }
    }
}
