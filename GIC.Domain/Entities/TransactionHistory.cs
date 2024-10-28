using GICApp.ApplicationCore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICApp.ApplicationCore.Domain.Entities
{
    public class TransactionHistory : BaseEntity
    {
        public string Account { get; set; }

        public string TransactionId { get; set; }

        public TransactionType Type { get; set; }

        public decimal Amount { get; set; }

        public decimal Balance { get; set; }

    }
}
