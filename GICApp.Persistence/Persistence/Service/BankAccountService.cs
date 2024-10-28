using GICApp.ApplicationCore.Application.Repositories;
using GICApp.ApplicationCore.Application.Services;
using GICApp.ApplicationCore.Domain.Entities;
using GICApp.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICApp.Infrastructure.Persistence.Service
{
    public class BankAccountService : IBankAccountService
    {
        private readonly IBankAccountRepository _repository;


        public  BankAccountService(IBankAccountRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
             
        }

        public async void Add(BankAccount entity)
        {
            _repository.Add(entity); 
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public async Task<IEnumerable<BankAccount>> GetAll()
        {
           return  await _repository.GetAll();
        }

        public async Task<BankAccount> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public  void Update(BankAccount entity)
        {
              _repository.Update(entity);
         }
    }
}
