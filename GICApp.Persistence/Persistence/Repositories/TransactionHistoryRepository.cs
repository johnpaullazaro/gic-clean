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
    public class TransactionHistoryRepository : ITransactionHistoryRepository
    {

        private readonly AppDbContext _context;

        public TransactionHistoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async void Add(TransactionHistory entity)
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

        public async Task<IEnumerable<TransactionHistory>> GetAll()
        {
            return await _context.TransactionHistories.ToListAsync();
        }

        public async Task<TransactionHistory> GetById(int id)
        {
            var item = await _context.TransactionHistories.Where(x => x.Id == id).FirstOrDefaultAsync();
            return item;
        }

        public async void Update(TransactionHistory entity)
        {
            _context.TransactionHistories.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
