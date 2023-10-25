namespace BiznewWebUI.Models
{
    public class Article : BaseEntity
    {


        public string Title { get; set; }


        public string Content { get; set; }


        public string PhotoUrl { get; set; }
        public string CategoryId { get; set; }
        public int ViewCount { get; set; }
        public string SeoUrl { get; set; }
        public bool Status { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public Category Category { get; set; }

        public List<ArticleTag> ArticleTags { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
     
        public List<LeaveComment> LeaveComments { get; set; }
       
        public List<ArticleComments> ArticleComments { get; set; }
        public AdevortsArticle AdevortsArticles { get; set; }
       






    }
}
