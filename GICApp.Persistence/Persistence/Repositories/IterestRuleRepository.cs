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
    public class InterestRuleRepository : IInterestRuleRepository
    {

        private readonly AppDbContext _context;

        public InterestRuleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async void Add(InterestRule entity)
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

        public async Task<IEnumerable<InterestRule>> GetAll()
        {
            return await _context.InterestRules.ToListAsync();
        }

        public async Task<InterestRule> GetById(int id)
        {
            var item = await _context.InterestRules.Where(x => x.Id == id).FirstOrDefaultAsync();
            return item;
        }

        public async void Update(InterestRule entity)
        {
            _context.InterestRules.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
