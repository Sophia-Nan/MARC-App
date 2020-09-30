using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MARC_App.Entities
{
    public class UsersBooking
    {
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("BookInstrumentId")]
        public BookInstrument BookInstrument { get; set; }
    }
}
