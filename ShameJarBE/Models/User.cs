using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShameJarBE.Models
{
    public partial class User
    {
        public User()
        {
            // UserCompetitions = new HashSet<UserCompetition>();
        }

        [Key]
        [Column("UserID")]
        public int UserID { get; set; }

        [StringLength(45)]
        public string Username { get; set; }

        [StringLength(900)]
        public string Password { get; set; }

        /*[InverseProperty("Competition")]
        public ICollection<UserCompetition> UserCompetitions { get; set; }*/
    }
}
