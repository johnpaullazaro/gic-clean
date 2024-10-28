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

        public void PrintTransactionHistories(IEnumerable<TransactionHistory> items)
        {
            //Date | Txn ID | Type | Amount | Balance7

            Console.WriteLine($" Date | Transaction ID | Type | Amount | Balance");

            foreach (var item in items)
            {
                Console.WriteLine($" {item.date.ToString(SystemConstants.DateFormat)} | {item.TransactionId} | {item.Type}  | {item.Amount}| {item.Balance} ");

            }
        }

    }
}
