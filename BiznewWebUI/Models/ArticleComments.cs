namespace BiznewWebUI.Models
{
    public class ArticleComments
    {
        public string Id { get; set; }
        public string CommentContent { get; set; }
        public DateTime PublishDate { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
