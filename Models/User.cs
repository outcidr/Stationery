﻿using System.ComponentModel.DataAnnotations;

namespace Stationery.Models
{

    public enum UserRole
    {
        Admin,
        User
    }


    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
        public UserRole Role { get; set; }
    }
}
