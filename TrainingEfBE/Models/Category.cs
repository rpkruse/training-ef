using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingEfBE.Models
{
    public partial class Category
    {
        public Category()
        {
        }

        [Key]
        [Column("categoryID")]
        public int CategoryID { get; set; }

        [StringLength(45)]
        public string Name { get; set; }

    }
}
