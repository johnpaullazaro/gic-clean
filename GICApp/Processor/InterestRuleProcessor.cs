using GICApp.ApplicationCore.Application.Services;
using GICApp.ApplicationCore.Domain.Common;
using GICApp.ApplicationCore.Domain.Entities;
using GICApp.Presentation.Constants;
using GICApp.Presentation.NewFolder;
using GICApp.Presentation.Services;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICApp.Presentation.Processor
{
    public class InterestRuleProcessor
    {

        public InterestRule GetRuleItem(DateTime date)
        {
            //25 
            var gicService = GICServices.GetAll().GetService<IInterestRuleService>();
            var getTransactionDay = date.Day;
            var rules = gicService.GetAll().Result;
            var item = new InterestRule();
             
            var LastRule = rules.LastOrDefault(); 
            var prev = new InterestRule();
            foreach ( var rule in rules )
            {  
                if (getTransactionDay <= rule.date.Day)
                {

                    prev = rule;
                    break;
                } 
            }
             
            //14
            //25 
            //25



            item = prev; 
            int n = getTransactionDay; 
            if (LastRule.date.Day < n)
            {
                item = LastRule;
            }
              
            return item; 
        }


        public IEnumerable<InterestRule> GetInterestRules()
        {
            var gicService = GICServices.GetAll().GetService<IInterestRuleService>();
            var result = gicService.GetAll().Result;
            return result;
        }
        public void DisplayInterestRules(IEnumerable<InterestRule> items, string account )
        {
            var ctr = 1;
            var totalCount = items.Count();
            var prevDate = new DateTime();
            var interestList = new List<InterestRuleComputationDTO>();
             

            foreach (var item in items)
            {
                var ruleDay=  item.date.Day;
                var numDays = 0;
                var totalDays = 0;
                 
                //first
                if (ctr == 1)
                {
                    totalDays = ruleDay;
                    prevDate = item.date; 
                }
                 
                if (ctr > 1  )
                {  
                    TimeSpan dateDifference = item.date - prevDate;
                    totalDays = (int)dateDifference.TotalDays;
                    prevDate = item.date; 
                }
                 
                var newItem = CreateInterestComputation(item, totalDays, account);
                interestList.Add(newItem); 
                totalDays = 0; 
                ctr++;
            }


            //when there is beyond last rule
            // 25+ 26 27 28 29 30
            var lastItem = items.LastOrDefault();
            var lastitemRuleDay = lastItem.date.Day;
            var calendarDays = DateTime.DaysInMonth(lastItem.date.Year, lastItem.date.Month);

            if (calendarDays > lastitemRuleDay)
            {
                //30 -25 
                var totalDays = calendarDays - lastitemRuleDay;
                var newItem = CreateBeyondInterestComputation(lastItem, totalDays, account);
                interestList.Add(newItem);
            }

 
            ComputeAnnualizedInterest(interestList,  account);
        }


        public InterestRuleComputationDTO CreateInterestComputation(InterestRule item, int totalDays,string account)
        {

            var transactionHistory = new TransActionProcessor(); 
            var transactionItems = transactionHistory.GetTransactionHistoriesByAccount(account);
            var itemTransactionHistory = transactionItems
                                            .Where(x => x.date <= item.date)
                                            .OrderByDescending(x => x.TransactionId)
                                            .FirstOrDefault();

            var balance = itemTransactionHistory.Balance;

            var annualizedInterestRate = balance * item.RulePercentage   * totalDays  /100 ;
        
            var newInterestItem = new InterestRuleComputationDTO()
            {
                RuleId = item.RuleId,
                Rate = item.RulePercentage,
                NumberOfDays = totalDays,
                AnnualizedInterest = annualizedInterestRate,
                EODBalance = balance,
                DateRate =item.date
            };


            //DisplayComputation(newInterestItem);
            return newInterestItem;
        }


        public InterestRuleComputationDTO CreateBeyondInterestComputation(InterestRule item, int totalDays, string account)
        {

            var transactionHistory = new TransActionProcessor();
            var transactionItems = transactionHistory.GetTransactionHistoriesByAccount(account);
            var itemTransactionHistory = transactionItems
                                            .Where(x => x.date >= item.date)
                                            .OrderByDescending(x => x.TransactionId)
                                            .FirstOrDefault();
            var balance = itemTransactionHistory.Balance;

            var annualizedInterestRate = balance * item.RulePercentage * totalDays  / 100;

            var newInterestItem = new InterestRuleComputationDTO()
            {
                RuleId = item.RuleId,
                Rate = item.RulePercentage,
                NumberOfDays = totalDays,
                AnnualizedInterest = annualizedInterestRate,
                EODBalance = itemTransactionHistory.Balance,
                DateRate = item.date
            };

             //DisplayComputation(newInterestItem);
            return newInterestItem;
        }

         
       


        private void DisplayComputation(InterestRuleComputationDTO item)
        {


            Console.WriteLine($" {item.NumberOfDays} | {item.EODBalance} | {item.RuleId} | {item.Rate} | {Math.Round(item.AnnualizedInterest,2)}");
             
        }


        private void ComputeAnnualizedInterest(List<InterestRuleComputationDTO> items, string account)
        {

            var interest = items.Sum(p => p.AnnualizedInterest) / 365;
        
            decimal resultInterest = Math.Round(interest, 2);

            var bankProcessor = new BankAccountProcessor();
            var acc = bankProcessor.GetAccountByAccountName(account);
            var newBalance = acc.Balance + resultInterest;


            var firstItem = items.FirstOrDefault();
            
           
            Console.WriteLine($" {GetLastDayofTheMonth(firstItem)} |          | I |  {resultInterest} | {newBalance}"); 
        }

        private string GetLastDayofTheMonth(InterestRuleComputationDTO item)
        {
            var year = item.DateRate.Year;
            var month = item.DateRate.Month;
            var totalDays = DateTime.DaysInMonth(year, month);
            var itemDateYearMonth = $"{year}{item.DateRate.Month}{totalDays}";
            return itemDateYearMonth;
        }

        public bool IsRateExists(string rule)
        {
            var gicService = GICServices.GetAll().GetService<IInterestRuleService>();
            var result = gicService.GetAll().Result.Where(x => x.RuleId == rule).Any();
            if(result)
            {
                Console.WriteLine("Rate exists");
            }

            return result;
        }

        public bool hasExistingRuleDaily(string dateString)
        {
            if(IsValidDate(dateString))
            {
                DateTime dateTime = DateTime.ParseExact(dateString, SystemConstants.DateFormat, null);

                var gicService = GICServices.GetAll().GetService<IInterestRuleService>();
                var result = gicService.GetAll().Result.Where(x => x.date.Equals(dateTime)).Any();
                if (result)
                {
                    Console.WriteLine("Not allowed to enter rule within the same day");
                }
                return result;
            }
            return false;
        }

        public void CreateNewRule(string rule, decimal percentage, string dateString)
        {

            if (!IsRateExists(rule))
            {
                if (isValidRate(percentage))
                { 
                  
                    var newInterestRule = new InterestRule()
                    {
                        date = DateTime.ParseExact(dateString, SystemConstants.DateFormat, null),
                        RuleId = rule,
                        RulePercentage = percentage,
                    };
                    var gicService = GICServices.GetAll().GetService<IInterestRuleService>();
                    gicService.Add(newInterestRule);
                } 
            } 
        }

        public bool isValidRate(decimal rate)
        {
            if (rate < 0 || rate > 100)
            {
                Console.WriteLine("Invalid rate value . Rate should be > 0 and < 100"); 
                return false;
            } 
            return true;
        }


        public bool IsValidDate(string dateString)
        {
            try
            {
                DateTime dateTime = DateTime.ParseExact(dateString, SystemConstants.DateFormat, null);
                return true;
            }
            catch (FormatException e)
            {
                Console.WriteLine("Invalid date Format");
                return false;
            }
        }
    }
}
 