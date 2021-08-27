using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingEfBE.Models
{
    public partial class Room
    {
        public Room()
        {
        }

        [Key]
        public int RoomID { get; set; }

        public string RoomName { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
        [JsonProperty("Password")]
        private string AltPasswordSetter
        {
            set { Password = value; }
        }

        public string Bio { get; set; }

        public string ImageURL { get; set; }




    }

    public class RoomLogin
    {

        public int UserID { get; set; }

        public string RoomName { get; set; }

        public string Password { get; set; }
    }

    public class NewRoom
    {
        public List<int> UserIDs { get; set; }

        public string RoomName { get; set; }

        public string Password { get; set; }

        public string Bio { get; set; }

        public string ImageURL { get; set; }
    }


}
