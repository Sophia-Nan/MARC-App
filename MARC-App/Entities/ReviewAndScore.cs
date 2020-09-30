using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MARC_App.Entities
{
    public class ReviewAndScore
    {
        public int Id { get; set; }
        public string  Review { get; set; }
        public float Score { get; set; }

        [ForeignKey("InstrumentId")]
        public Instrument Instrument { get; set; }
    }
}
