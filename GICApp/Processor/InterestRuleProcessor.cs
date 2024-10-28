using GICApp.ApplicationCore.Application.Services;
using GICApp.ApplicationCore.Domain.Entities;
using GICApp.Presentation.Constants;
using GICApp.Presentation.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICApp.Presentation.Processor
{
    public class InterestRuleProcessor
    {

        // 20241025 RULE03 2.30

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
 