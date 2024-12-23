﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetSoLive_MVC.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Username/Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}