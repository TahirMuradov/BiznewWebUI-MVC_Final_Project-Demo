namespace BiznewWebUI.Models
{
    public class Article : BaseEntity
    {


        public string Title { get; set; }


        public string Content { get; set; }


        public string PhotoUrl { get; set; }
        public Guid CategoryId { get; set; }
        public int ViewCount { get; set; }
        public string SeoUrl { get; set; }
    
        public bool IsFeatured { get; set; } = false;
    
        public bool IsPublished { get; set; } =false;
        public string CreatedBy { get; set; }
        public Category Category { get; set; }

        public List<ArticleTag> ArticleTags { get; set; }


     
        public List<LeaveComment>? LeaveComments { get; set; }
       
        public List<ArticleComments>? ArticleComments { get; set; }
        public AdvortsArticle? AdvortsArticles { get; set; }
       






    }
}
