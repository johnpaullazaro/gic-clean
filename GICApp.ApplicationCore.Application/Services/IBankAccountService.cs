using GICApp.ApplicationCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICApp.ApplicationCore.Application.Services
{
    public interface IBankAccountService 
    {
          Task<IEnumerable<BankAccount>> GetAll();

          Task<BankAccount> GetById(int id);

          void Add(BankAccount entity);

            void Update(BankAccount entity);

            void Delete(int id);


    }
}
