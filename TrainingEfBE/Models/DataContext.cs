﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingEfBE.Models
{
    public partial class DataContext : DbContext
    {
        public virtual DbSet<User> User { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}