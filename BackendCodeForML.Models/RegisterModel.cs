﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendCodeForML.Models
{
    public class RegisterModel
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please enter your name")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter your email address"), EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter password"), DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password), Compare("Password", ErrorMessage = "Password not matched"), Required(ErrorMessage = "Fill this field")]
        public string ConfirmPassword { get; set; }

        // Foreign key for Role
        [Required]
        public int RoleId { get; set; }

        // Navigation property
        public virtual UserRoleModel Role { get; set; }
    }
}
