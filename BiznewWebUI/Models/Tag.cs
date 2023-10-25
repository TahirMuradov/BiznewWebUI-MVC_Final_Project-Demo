namespace BiznewWebUI.Models
{
    public class Tag:BaseEntity
    {
   
        public string TagName { get; set; }
        public List<ArticleTag>? ArticleTag { get; set; }
    }
}
