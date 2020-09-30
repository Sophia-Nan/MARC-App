using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MARC_App.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string ProjectName { get; set; }
        public string ActivePi { get; set; }
        public string DosageForm { get; set; }
        public string Concentration { get; set; }
        public int Priority { get; set; }
    }
}
