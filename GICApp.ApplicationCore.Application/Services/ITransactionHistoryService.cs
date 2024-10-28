using GICApp.ApplicationCore.Application.Repositories;
using GICApp.ApplicationCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICApp.ApplicationCore.Application.Services
{
    public interface ITransactionHistoryService
    {


        Task<IEnumerable<TransactionHistory>> GetAll();

        Task<IEnumerable<TransactionHistory>> GetAllByAccount(string account);

        Task<TransactionHistory> GetById(int id);

        Task<TransactionHistory> GetByAccount(string account);

        void Add(TransactionHistory entity);

        void Update(TransactionHistory entity);

        void Delete(int id);
    }
}
