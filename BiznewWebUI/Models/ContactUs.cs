﻿namespace BiznewWebUI.Models
{
    public class ContactUs
    {
        public Guid Id { get; set; }

        public  string Name { get; set; }
        public string Email {  get; set; } 
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool isUnseen { get; set; }
        public DateTime SendDate { get; set; }

    }
}
