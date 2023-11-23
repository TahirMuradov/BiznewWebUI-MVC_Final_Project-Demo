using System.ComponentModel.DataAnnotations;

namespace BiznewWebUI.Models
{
    public class LeaveComment
    {
        public Guid Id { get; set; }
        public string CommentContent { get; set; }
        public DateTime PublishDate { get; set; }
        [EmailAddress]
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}
