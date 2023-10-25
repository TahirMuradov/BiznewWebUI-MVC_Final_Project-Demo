namespace BiznewWebUI.Models
{
    public class AdevortsArticle
    {
        public string Id { get; set; }
        public string AdevortId { get; set; }
        public List<Adevort> Adevorts { get; set;}
        public string ArticleId { get; set; }
        public List<Article> Articles { get; set;}
    }
}
