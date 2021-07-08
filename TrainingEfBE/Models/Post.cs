using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingEfBE.Models
{
    public partial class Post
    {
        public Post()
        {
        }

        [Key]
        [Column("PostID")]
        public int PostID { get; set; }

        [StringLength(10000)]
        public string Message { get; set; }

        
        public DateTime CreatedDate;

        public int Rating { get; set; }

        public int CreatedBy { get; set; }
    }

}
