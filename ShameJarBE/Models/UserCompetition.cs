using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShameJarBE.Models
{
    public partial class UserCompetition
    {
        public UserCompetition()
        {
            //Competition = new Competition();
            //User = new User();
        }

        [Key]
        [Column("UserCompetitionID")]
        public int UserCompetitionID { get; set; }

        [Column("CompetitionID")]
        public int CompetitionID { get; set; }

        [Column("UserID")]
        public int UserID { get; set; }

        [Column("Score")]
        public int Score { get; set; }

        [Column("RuleJSON")]
        public JsonObject RuleJSON { get; set; }

        /*[ForeignKey("fk_cid_comp")]
        [InverseProperty("UserCompetition")]
        public Competition Competition { get; set; }

        [ForeignKey("fk_uid_user")]
        [InverseProperty("UserCompetition")]
        public User User { get; set; }*/
    }
}
