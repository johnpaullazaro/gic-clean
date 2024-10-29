using GICApp.ApplicationCore.Domain.Entities;
using GICApp.Presentation.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICApp.Presentation.Processor
{
    public class PrintStatementProcessor
    {



        public IEnumerable<TransactionHistory> GetTransactionHistories(string account)
        {
            var transactions = new TransActionProcessor();
            var items = transactions.GetTransactionHistoriesByAccount(account);
            return items;
        }

        public decimal GetRulePercentage(DateTime dateTime)
        {
            var interests = new InterestRuleProcessor();
            var items = interests.GetRuleItem(dateTime);
            if(items != null)
                return items.RulePercentage;
            return 0;
        }


        //var totalDaysInAMonth = DateTime.DaysInMonth(2024, 6);
        //var listRules = new List<DateTime>()
        //    {
        //        new DateTime(2024,6,14),
        //         new DateTime(2024,6,25),
        //    }; 
        //    for(var i= 1; i<totalDaysInAMonth +1;i++)
        //    { 
        //    }

      
        public void PrintTransactionHistories(IEnumerable<TransactionHistory> items)
        {
            //Date | Txn ID | Type | Amount | Balance7

            Console.WriteLine($" Date | Transaction ID | Type | Amount | Balance | Rate | ");

            foreach (var item in items)
            {
                Console.WriteLine($" {item.date.ToString(SystemConstants.DateFormat)} | {item.TransactionId} | {item.Type}  | {item.Amount}| {item.Balance}" +
                    $" | {GetRulePercentage(item.date)} | ");




            }
        }

    }
}
