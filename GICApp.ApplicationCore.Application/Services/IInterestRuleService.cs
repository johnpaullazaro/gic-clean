using GICApp.ApplicationCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICApp.ApplicationCore.Application.Services
{
    public interface IInterestRuleService  
    {
        Task<IEnumerable<InterestRule>> GetAll();

        Task<InterestRule> GetById(int id);

        void Add(InterestRule entity);

        void Update(InterestRule entity);

        void Delete(int id);

    }
}
