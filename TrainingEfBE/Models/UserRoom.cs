using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingEfBE.Models
{
    public partial class UserRoom
    {
        public UserRoom()
        {
        }

        public int UserID { get; set; }

        public int RoomID { get; set; }

        [Key]
        public int UserRoomID { get; set; }
    }
}
