﻿using System.ComponentModel.DataAnnotations;

namespace JhipsterBlazor.Models
{
    public class RegisterModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Your username cannot be longer than 50 characters.")]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
