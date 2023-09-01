﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrueDogStore.Models;

namespace TrueDogStore.Models
{
    public class User : IdentityUser
    {
        
        public string? Nickname { get; set; }
        public string? Password { get; set; }
        [ForeignKey("User_Info")]
        public int User_InfoId { get; set; }

        public User_Info? User_Info { get; set; }

    }
}
