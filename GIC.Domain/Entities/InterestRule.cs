using GICApp.ApplicationCore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICApp.ApplicationCore.Domain.Entities
{
    public class InterestRule : BaseEntity
    {
        public string RuleId { get; set; }

        public decimal RulePercentage { get; set; }
    }
}
