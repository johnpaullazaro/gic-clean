using GICApp.ApplicationCore.Domain.Common;
using GICApp.ApplicationCore.Domain.Entities;
using GICApp.Presentation.Processor;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GICApp.Presentation
{
    public static class MenuManager
    {
      

        public static void ShowMenu()
        {
            Console.WriteLine("=== Menu ===");
            Console.WriteLine("[T] Input transactions");
            Console.WriteLine("[I] Define Interest rules");
            Console.WriteLine("[P] Print statement");
            Console.WriteLine("[Q] Quit");
        }


        public static bool HandleInput(string option)
        {
            option = option.ToUpper();
            switch (option)
            {
                case "T":
                    Console.WriteLine("=== Transaction ===");

                    BankAccountProcess();

                    break;
                case "I":


                    InterestRuleProcess(); 
                    break;
                case "P":
                    Console.WriteLine("=== Print Statements ===");
                    PrintStatementProcess();

                    break;
                case " ":
                     break;
                case "Q":
                    Console.WriteLine("Exiting...");
                    return true; // Exit the loop
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
            return false; // Continue the loop
        }
 
    
    
    
    
    
    
    
        public static void BankAccountProcess()
        {
            // account, transaction, amount
            Console.WriteLine("Account Name"); 
            var input_account = Console.ReadLine();

            Console.WriteLine("Transaction Type [W/D]"); 
            var input_transaction_type = Console.ReadLine();

            Console.WriteLine("Amount"); 
            var input_amount = Console.ReadLine();


            var transaction = new TransActionProcessor();
            var bankAccountProcessor = new BankAccountProcessor();


            if (input_transaction_type.ToUpper() == TransactionType.W.ToString())
            {
                // has bankaccount
                if (bankAccountProcessor.IsAccountExists(input_account))
                {
                    bankAccountProcessor.Withdraw(input_account, Convert.ToDecimal(input_amount));
                }
                else
                {
                    // has no bank account 
                    Console.WriteLine("Account does not exist and not allowed to do withdrawal");
                }
            }
            else if (input_transaction_type.ToUpper() == TransactionType.D.ToString())
            {
                // has bankaccount
                if (bankAccountProcessor.IsAccountExists(input_account))
                {
                    bankAccountProcessor.Deposit(input_account, Convert.ToDecimal(input_amount));
                }
                else
                {
                    // has no bank account  
                    // create bank account 
                    bankAccountProcessor.CreateNewAccount(input_account, Convert.ToDecimal(input_amount));

                    // add new initial transaction in history
                    var transactionType = (TransactionType)Enum.Parse(typeof(TransactionType), input_transaction_type);
                    transaction.CreateNewTransaction(Convert.ToDecimal(input_amount), input_account, transactionType
                        , Convert.ToDecimal(input_amount));
                }
            }
            else
            {
                Console.WriteLine("Invalid Transaction Type");
            }


        }


        public static void InterestRuleProcess()
        {
            Console.WriteLine("=== Define Interest Rules ===");

            Console.WriteLine("Please enter interest rule details in <Date> <RULEID> <RATE IN %> format  ");
            Console.WriteLine("Please enter blank to go back to main menu ");

            Console.WriteLine("Date :  YYYYMMdd");
            var input_date = Console.ReadLine();

            Console.WriteLine("Rule Id :");
            var input_rule_id = Console.ReadLine();

            Console.WriteLine("Rate % - Note: Rate > 0 and < 100");
            var input_rate = Console.ReadLine();


            var interestRuleProcessor = new InterestRuleProcessor();

            if(!interestRuleProcessor.hasExistingRuleDaily(input_date))
            {
                if (interestRuleProcessor.IsValidDate(input_date))
                {
                    interestRuleProcessor.CreateNewRule(input_rule_id, Convert.ToDecimal(input_rate), input_date);
                }
            }
            

        }



        public static void PrintStatementProcess()
        {
            Console.WriteLine("=== Print Statement ==="); 
            Console.WriteLine("Please enter account and month to generate the statement < Account>  <Year><Month> ");
 
            Console.WriteLine("Account");
            var input_account = Console.ReadLine();

            Console.WriteLine("Year");
            var input_year = Console.ReadLine();
 
            Console.WriteLine("Month");
            var input_month = Console.ReadLine();

            var printStatementProcessor = new PrintStatementProcessor();
            var items = printStatementProcessor
                         .GetTransactionHistories(input_account)
                         .Where(x => x.date.Year.ToString().Equals(input_year)  
                                     && x.date.Month.ToString().Equals(input_month))
                         .OrderBy(x=> x.date); 

            if(items.Any())
            {
                printStatementProcessor.PrintTransactionHistories(items); 
            }
            Console.WriteLine("Not found");


        }
    }
}
