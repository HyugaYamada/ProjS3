﻿using System.ComponentModel.DataAnnotations;

namespace khiemnguyen.WebApi.Models
{
    public class Message
    {
        [Key]
        public int id { get; set; }
        public int User_sent { get; set; }
        public int User_received { get; set; }
        public string Message_note { get; set; }
        public string Image { get; set; }
        public DateTime Timestamp { get; set; }

    }
}
