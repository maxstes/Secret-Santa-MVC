﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Secret_Santa_MVC.Data.Entities
{
    public class Room
    {
        [Key]
        public int IdRoom { get; set; }
        public string? NameRoom { get; set; }
        public string? Description { get; set; }
        public float Budget { get; set; }
        public string? LinkRoom { get; set; }

        public int UserId { get; set; }
        //public List<User> Users { get; set; } = new();
    }
}
