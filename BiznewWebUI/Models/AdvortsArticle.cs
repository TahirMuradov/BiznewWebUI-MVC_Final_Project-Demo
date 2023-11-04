namespace BiznewWebUI.Models
{
    public class AdvortsArticle
    {
        public string Id { get; set; }
        public string AdevortId { get; set; }
        public List<Advort> Adevorts { get; set;}
        public string ArticleId { get; set; }
        public List<Article> Articles { get; set;}
    }
}
