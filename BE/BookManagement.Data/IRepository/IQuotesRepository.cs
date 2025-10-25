using QuotesManagement.Common.DTO_s.SearchDTO;
using QuotesManagement.Common.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotesManagement.Data.IRepository
{
    public interface IQuotesRepository
    {

        Task<List<Quotes>> GetAllAsync();
        Task<Quotes?> GetByIdAsync(int id);
        Task<Quotes> AddAsync(Quotes entity);
        Task<Quotes> UpdateAsync(Quotes entity);
        Task<bool> DeleteAsync(int id);
        Task<List<Quotes>> SearchAsync(string? authorName, List<string>? tags, List<string>? quotes);
        Task<IEnumerable<Quotes>> AddMultipleAsync(IEnumerable<Quotes> quotes);
    }
}
