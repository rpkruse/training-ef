using Newtonsoft.Json;
using System;
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

        [StringLength(100)]
        public string Username { get; set; }


        [StringLength(900)]
        [JsonIgnore]
        public string Password { get; set; }
        [JsonProperty("Password")]
        private string AltPasswordSetter
        {
            set { Password = value; }
        }


        [JsonIgnore]
        public virtual List<Post> Posts { get; set; }
    }

    public partial class LoggedInUser {

        public LoggedInUser()
        {

        }
        public int UserID { get; set; }

        public string Username { get; set; }
    }
}
