using System.ComponentModel.DataAnnotations;

namespace BiznewWebUI.Models
{
    public class Category :BaseEntity
    {
 
        [MaxLength(50)]
        [Required(ErrorMessage = "Category name must not be empty")]
         public string CategoryName { get; set; }
        public List<Article>? Articles { get; set; }

    }
}
