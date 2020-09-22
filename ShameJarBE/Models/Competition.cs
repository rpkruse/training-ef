using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShameJarBE.Models
{
    public partial class Competition
    {
        public Competition()
        {
            // UserCompetitions = new HashSet<UserCompetition>();
            // Owner = new User();
        }

        [Key]
        [Column("CompetitionID")]
        public int CompetitionID { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime StartDate { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime EndDate { get; set; }

        [Required]
        [Column("OwnerID")]
        public int OwnerID { get; set; }

        [StringLength(45)]
        public string Winner { get; set; }

        [StringLength(45)]
        public string Loser { get; set; }

        [Required]
        [Column("IsActive")]
        public bool IsActive { get; set; }

        /*[ForeignKey("fk_oid_uid")]
        [InverseProperty("UserCompetition")]
        public User Owner { get; set; }*/

        /*[InverseProperty("Competition")]
        public ICollection<UserCompetition> UserCompetitions { get; set; }*/
    }
}
