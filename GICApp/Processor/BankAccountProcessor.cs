using GICApp.ApplicationCore.Application.Services;
using GICApp.ApplicationCore.Domain.Common;
using GICApp.ApplicationCore.Domain.Entities;
using GICApp.Presentation.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICApp.Presentation.Processor
{
    public class BankAccountProcessor
    { 
 
        public BankAccountProcessor() 
        { 
        }

        public bool IsAccountExists(string account)
        {
            var gicService = GICServices.GetAll().GetService<IBankAccountService>();
            var result = gicService.GetAll().Result.Where(x=>x.Account == account).Any(); 
            return result;
        }


        public BankAccount GetAccountByAccountName(string accountName)
        {
            var gicService = GICServices.GetAll().GetService<IBankAccountService>();
            var bankAccount = gicService.GetAll().Result.Where(x => x.Account == accountName).First(); 
            return bankAccount;
        }
        public void Withdraw(string accountName,decimal amount)
        { 
            var account = GetAccountByAccountName(accountName);
           
            if(account != null)
            { 
                // update current balance - amount 
                var currentBalance = account.Balance;
                var newBalance = account.Balance - amount;
                if(isValidAmount(amount)) { 
                    if(amount <= currentBalance)
                    {
                        account.Balance = newBalance;

                        var gicService = GICServices.GetAll().GetService<IBankAccountService>();
                        gicService.Update(account);

                        var transaction = new TransActionProcessor();
                        transaction.CreateNewTransaction(amount, account.Account, TransactionType.W, newBalance);
                    }
                    else
                    {
                        Console.WriteLine("Insufficient Funds");
                    }
                } 
            }
            else
            {
                Console.WriteLine("Invalid Amount");
            }
        }
        public void Deposit(string accountName, decimal amount)
        {
            var account = GetAccountByAccountName(accountName);
            if (isValidAmount(amount))
            {
                if (account != null)
                {
                    // update current balance + amount 
                    var previousBalance = account.Balance;
                    var newBalance = account.Balance + amount;
                    account.Balance = newBalance;

                    var gicService = GICServices.GetAll().GetService<IBankAccountService>();
                    gicService.Update(account);

                    var transaction = new TransActionProcessor();
                    transaction.CreateNewTransaction(amount, account.Account, TransactionType.D, newBalance); 
                }
            }
            else
            {
                Console.WriteLine("Invalid Amount ");
            }
        }

        public void CreateNewAccount(string account, decimal amount)
        { 
            var newAccount = new BankAccount()
            {
                Account =  account, // transaction.GenerateTransactionID(input_account)
                Balance = amount,
                date = DateTime.Now
            };


            var gicService = GICServices.GetAll().GetService<IBankAccountService>();
            gicService.Add(newAccount); 
        }



        public bool isValidAmount(decimal amount) {
            if (amount < 0)
                return false;
            return true; 
        }

        public void UpdateAccount(BankAccount account)
        {
             
        }


    }
}
