using System;
using System.ComponentModel.DataAnnotations;

namespace AnimeWebApp.Models
{
    public class DiscussionModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }  // 使用用户ID来识别用户

        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }  

        [Required(ErrorMessage = "Please enter a topic.")]
        [StringLength(200, ErrorMessage = "Topic cannot exceed 200 characters.")]
        [Display(Name = "Topic")]
        public string Topic { get; set; }

        [Required(ErrorMessage = "Content cannot be empty.")]
        [StringLength(4000, ErrorMessage = "Content cannot exceed 4000 characters.")]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Post Time")]
        public DateTime PostTime { get; set; } = DateTime.Now;  // 设置发帖时间为当前时间
    }
}
