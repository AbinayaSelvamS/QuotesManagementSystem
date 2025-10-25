using QuotesManagement.Common.DTO_s;
using QuotesManagement.Common.DTO_s.reqSearchDTO;
using QuotesManagement.Common.DTO_s.ResponseDTO;
using QuotesManagement.Common.DTO_s.SearchDTO;
using QuotesManagement.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotesManagement.Application.Interfaces
{
    public interface IQuotesService
    {
        
            Task<List<resqponseQuotesDto>> GetAllAsync();
            Task<resqponseQuotesDto?> GetByIdAsync(int id);
            Task<resqponseQuotesDto> AddAsync(searchrequestDto dto);
            Task<resqponseQuotesDto?> UpdateAsync(int id, searchrequestDto dto);
            Task<bool> DeleteAsync(int id);
            Task<List<Quotes>> SearchAsync(string? authorName, List<string>? tags, List<string>? quotes);
            Task<IEnumerable<QuoteDto>> AddMultipleQuotesAsync(IEnumerable<QuoteDto> quoteDtos);

    }
}
