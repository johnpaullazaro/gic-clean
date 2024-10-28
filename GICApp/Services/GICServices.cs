using GICApp.ApplicationCore.Application.Repositories;
using GICApp.ApplicationCore.Application.Services;
using GICApp.Infrastructure.Persistence.Repositories;
using GICApp.Infrastructure.Persistence.Service;
using GICApp.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICApp.Presentation.Services
{
    public static class GICServices
    {


        public static ServiceProvider GetAll()
        {
            var serviceProvider = new ServiceCollection()
                .AddDbContext<AppDbContext>()
               .AddSingleton<IBankAccountService, BankAccountService>()
               .AddSingleton<IBankAccountRepository, BankAccountRepository>()
               .AddSingleton<IInterestRuleService, InterestRuleService>()
               .AddSingleton<IInterestRuleRepository    , InterestRuleRepository>()
               .AddSingleton<ITransactionHistoryService, TransactionHistoryService>()
               .AddSingleton<ITransactionHistoryRepository, TransactionHistoryRepository>()
               .BuildServiceProvider();

            return serviceProvider;

        }

       
        //Console.WriteLine("test");
        //Console.ReadLine();
    }
}
