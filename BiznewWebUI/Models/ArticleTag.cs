﻿    namespace BiznewWebUI.Models
{
    public class ArticleTag
    {
        public Guid Id { get; set; }
        public Guid ArticleId { get; set; }
        public Article Article { get; set; }
        public Guid TagId { get; set; }
        public Tag Tags { get; set; }
    }
}
