using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICApp.Presentation.NewFolder
{
    public class InterestRuleComputationDTO 
    {

        public int NumberOfDays { get; set; }

        public decimal EODBalance { get; set; }

        public string RuleId { get; set; }

        public decimal Rate { get; set; }

        public DateTime DateRate { get; set; }


        public decimal AnnualizedInterest { get; set; }

    }
}
