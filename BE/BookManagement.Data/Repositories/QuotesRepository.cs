using Microsoft.EntityFrameworkCore;
using QuotesManagement.Common.DTO_s.SearchDTO;
using QuotesManagement.Common.Model;
using QuotesManagement.Data.DB;
using QuotesManagement.Data.IRepository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotesManagement.Data.Repositories
{
    public class QuotesRepository: IQuotesRepository
    {
        private readonly ApplicationDbContext _context;
        public QuotesRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Quotes>> GetAllAsync()
        {
            var a = _context.quotes.Count();                                                                                                                       
            return await _context.quotes.ToListAsync();
        }

        public async Task<Quotes?> GetByIdAsync(int id)
        {
            return await _context.quotes.FindAsync(id);
        }

        public async Task<Quotes> AddAsync(Quotes entity)
        {
            _context.quotes.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Quotes> UpdateAsync(Quotes entity)
        {
            _context.quotes.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.quotes.FindAsync(id);
            if (existing == null)
                return false;

            _context.quotes.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Quotes>> SearchAsync(string? authorName, List<string>? tags, List<string>? quotes)
        {
            var query = _context.quotes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(authorName))
                query = query.Where(q => q.Author.Contains(authorName));

            if (tags != null && tags.Any())
            {
                foreach (var tag in tags)
                {
                    query = query.Where(q => q.Tags.Contains(tag));
                }
            }

            if (quotes != null && quotes.Any())
            {
                foreach (var term in quotes)
                {
                    query = query.Where(q => q.QuoteText.Contains(term));
                }
            }

            return await query.ToListAsync();
        }
        private static readonly object _dbLock = new object();

        public async Task<IEnumerable<Quotes>> AddMultipleAsync(IEnumerable<Quotes> entities)
        {
            lock (_dbLock) // Only one thread can write at a time
            {
                _context.quotes.AddRange(entities);
                _context.SaveChanges(); // Use synchronous inside lock
            }

            return entities;
        }



    }


}

