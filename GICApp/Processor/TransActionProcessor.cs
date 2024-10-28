using GICApp.ApplicationCore.Application.Services;
using GICApp.ApplicationCore.Domain.Common;
using GICApp.ApplicationCore.Domain.Entities;
using GICApp.Presentation.Constants;
using GICApp.Presentation.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICApp.Presentation.Processor
{
    public class TransActionProcessor
    { 
        public TransActionProcessor() { 
         
        }   
          
        public void CreateNewTransaction(decimal amount, string account,TransactionType transactionType,decimal balance)
        { 
            TransactionHistory transaction = new TransactionHistory();
            transaction.date = DateTime.ParseExact(DateTime.Now.ToString(SystemConstants.DateFormat), SystemConstants.DateFormat, null);
            transaction.TransactionId = GenerateTransactionID(account);
            transaction.Amount = amount;
            transaction.Account = account;
            transaction.Type = transactionType;
            transaction.Balance = balance;
             
            var gicService = GICServices.GetAll().GetService<ITransactionHistoryService>();
            gicService.Add(transaction);
             
        }
         

        public bool ValidateAmount(decimal amount)
        {
            if (amount < 0)
                Console.WriteLine("Amount should be greater than 0");
                return false; 
        }
         
        public bool hasTransactionHistory(string account) 
        {

            var gicService = GICServices.GetAll().GetService<ITransactionHistoryService>();
            var acc = gicService.GetByAccount(account).GetAwaiter().GetResult();  

            if(acc is not null)
            {
               return true;
              
            }   
            return false;
             
        }


        public bool IsAllowedWithdrawalInFirstTransaction(string account, string input_transaction_type)
        {

            var hasHistory = hasTransactionHistory(account);

            // if there is no transaction history 
            // do not allow withdrawal
            if (!hasHistory && input_transaction_type == TransactionType.W.ToString())
            {
                Console.WriteLine("Not allowed withdrawal in first transaction");
                return false;
            }
            else
            {
                Console.WriteLine("allow " + input_transaction_type);
                return true;
            }
        }



        public string GenerateTransactionID(string account)
        {
            var tranasactionid = "";

            if(!hasTransactionHistory(account))
            {
                tranasactionid = DateTime.Now.ToString(SystemConstants.DateFormat) + "1";
            }
            else
            {
                var gicService = GICServices.GetAll().GetService<ITransactionHistoryService>();
                //get count of transactions
                var count = gicService.GetAllByAccount(account).GetAwaiter().GetResult().Count() +1;
                tranasactionid = DateTime.Now.ToString(SystemConstants.DateFormat) + "-" + count.ToString();

            }
            Console.WriteLine(tranasactionid);
            return tranasactionid;
           
        }




        public IEnumerable<TransactionHistory> GetTransactionHistoriesByAccount(string account)
        {
            var gicService = GICServices.GetAll().GetService<ITransactionHistoryService>(); 
            return gicService.GetAllByAccount(account).GetAwaiter().GetResult();
        }

    }
}
