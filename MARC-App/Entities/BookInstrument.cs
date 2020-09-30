using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MARC_App.Entities
{
    public class BookInstrument
    {
        public int Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int ActualWorkingHours { get; set; }
        public string Approval { get; set; }
        public string Notes { get; set; }
        public string AdditionalReq { get; set; }

        [ForeignKey("InstrumentId")]
        public Instrument Instrument { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; }
        
    }
}
