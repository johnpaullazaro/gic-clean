using GICApp.ApplicationCore.Application.Repositories;
using GICApp.ApplicationCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GICApp.Infrastructure.Persistence.Repositories
{
    public class BankAccountRepository : IBankAccountRepository 
    {
        private readonly AppDbContext _context;

        public BankAccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async void Add(BankAccount entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async void Delete(int id)
        {
            var item = GetById(id);
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BankAccount>> GetAll()
        {
             return await _context.BankAccounts.ToListAsync();
        }

        public async Task<BankAccount> GetById(int id)
        {
            var item = await _context.BankAccounts.Where(x => x.Id == id).FirstOrDefaultAsync();
            return item;
        }

        public async void Update(BankAccount entity)
        { 
            _context.BankAccounts.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
