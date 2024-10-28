using GICApp.ApplicationCore.Application.Repositories;
using GICApp.ApplicationCore.Application.Services;
using GICApp.ApplicationCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICApp.Infrastructure.Persistence.Service
{



    public class InterestRuleService : IInterestRuleService
    {
        private readonly IInterestRuleRepository _repository;


        public InterestRuleService(IInterestRuleRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

        }

        public async void Add(InterestRule entity)
        {
            _repository.Add(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public async Task<IEnumerable<InterestRule>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<InterestRule> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public void Update(InterestRule entity)
        {
            _repository.Update(entity);
        }
    }
}