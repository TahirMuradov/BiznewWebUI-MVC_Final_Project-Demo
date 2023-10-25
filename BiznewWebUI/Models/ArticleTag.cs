    namespace BiznewWebUI.Models
{
    public class ArticleTag
    {
        public string Id { get; set; }
        public string ArticleId { get; set; }
        public Article Article { get; set; }
        public string TagId { get; set; }
        public Tag Tags { get; set; }
    }
}
