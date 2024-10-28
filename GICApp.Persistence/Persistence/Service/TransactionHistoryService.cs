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


    public class TransactionHistoryService : ITransactionHistoryService
    {
        private readonly ITransactionHistoryRepository _repository;


        public TransactionHistoryService(ITransactionHistoryRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

        }

        public async void Add(TransactionHistory entity)
        {
            _repository.Add(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public async Task<IEnumerable<TransactionHistory>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<TransactionHistory> GetById(int id)
        {
            return await _repository.GetById(id);
        }


        public async Task<TransactionHistory> GetByAccount(string account)
        {
            return   _repository.GetAll().Result.Where(x => x.Account == account).FirstOrDefault();
        }
        public async Task<IEnumerable<TransactionHistory>> GetAllByAccount(string account)
        {
            return   _repository.GetAll().Result.Where(x => x.Account == account);
        }
        public void Update(TransactionHistory entity)
        {
            _repository.Update(entity);
        }
    }
}