using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MARC_App.Entities
{
    public class Instrument
    {
        public int Id { get; set; }
        public string InstrumentName { get; set; }
        public string InstrumentDescription { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
