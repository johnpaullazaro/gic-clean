using GICApp.ApplicationCore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICApp.ApplicationCore.Domain.Entities
{
    public class BankAccount : BaseEntity
    {


        public string Account { get; set; } //account number of bank

        public decimal Balance { get; set; }

         
    }
}
