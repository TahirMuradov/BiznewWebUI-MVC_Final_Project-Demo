using System.ComponentModel.DataAnnotations.Schema;

namespace BiznewWebUI.Models
{
    public class BaseEntity
    {

        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
     
        public string UserId { get; set; }
        public User User { get; set; }
        public string? UpdatedUserId { get; set; }
   
    
       





    }
}
