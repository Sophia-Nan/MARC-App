using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MARC_App.Entities
{
    public class InstrumentCondition
    {
        public int Id { get; set; }
        public bool Condition { get; set; }

        [ForeignKey("InstrumentId")]
        public Instrument Instrument { get; set; }

    }
}
