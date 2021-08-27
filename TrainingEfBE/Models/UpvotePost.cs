using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingEfBE.Models
{
    public class UpvotePost
    {
        public int UserID { get; set; }

        public int PostID { get; set; }

        [Key]
        public int UpvotePostID { get; set; }
    }
}
