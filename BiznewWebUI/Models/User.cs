using Microsoft.AspNetCore.Identity;


namespace BiznewWebUI.Models
{
    public class User:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoUrl { get; set; } = "~/admin/img/undraw_profile.svg";
        public string? AboutAuthor { get; set; }
        public List<Article> Articles { get; set; }
        public List<ArticleComments> ArticleComments { get; set; }
        public List<Category> Categories { get; set; }
        public List<Tag> Tags { get; set; }
        public List<UserActions> ActionsUser { get; set; }



    }
}
