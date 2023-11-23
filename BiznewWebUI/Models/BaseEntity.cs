using System.ComponentModel.DataAnnotations.Schema;

namespace BiznewWebUI.Models
{
    public class BaseEntity
    {

        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
     
        public string UserId { get; set; }
        public User User { get; set; }
        public string? UpdatedUserId { get; set; }
        public string? DeletedUserId { get; set; }
        public bool IsDeletControl { get; set; }=false;
        public bool IsDeleted { get; set; } = false;
       







    }
}
