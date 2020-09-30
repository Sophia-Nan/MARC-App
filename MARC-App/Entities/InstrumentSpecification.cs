using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MARC_App.Entities
{
    public class InstrumentSpecification
    {
        public int Id { get; set; }

        [ForeignKey("SpecificationId")]
        public Specification Specification { get; set; }

        [ForeignKey("InstrumentId")]
        public Instrument Instrument { get; set; }
    }
}
