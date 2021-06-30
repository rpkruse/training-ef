﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingEfBE.Models
{
    public partial class User
    {
        public User()
        {
        }

        [Key]
        [Column("UserID")]
        public int UserID { get; set; }

        [StringLength(45)]
        public string Username { get; set; }

        [StringLength(900)]
        public string Password { get; set; }
    }
}