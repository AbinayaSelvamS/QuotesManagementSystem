using AutoMapper;
using QuotesManagement.Application.Interfaces;
using QuotesManagement.Common.DTO_s;
using QuotesManagement.Common.DTO_s.reqSearchDTO;
using QuotesManagement.Common.DTO_s.ResponseDTO;
using QuotesManagement.Common.DTO_s.SearchDTO;
using QuotesManagement.Common.Model;
using QuotesManagement.Data.IRepository;
using QuotesManagement.Infrastructure.Mapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QuotesManagement.Application.Service
{
    public class QuotesService: IQuotesService
    {
        private readonly IQuotesRepository _repository;
     
        public QuotesService( IQuotesRepository repository)
        {
          
            _repository = repository;
        }
        public async Task<List<resqponseQuotesDto>> GetAllAsync()
        {
            var quotes = await _repository.GetAllAsync();
            return MappingHelper.ToDtoList(quotes);
        }

        public async Task<resqponseQuotesDto?> GetByIdAsync(int id)
        {
            var quote = await _repository.GetByIdAsync(id);
            return quote == null ? null : MappingHelper.ToDto(quote);
        }

        public async Task<resqponseQuotesDto> AddAsync(searchrequestDto dto)
        {
            var entity = MappingHelper.ToEntity(dto);
            var created = await _repository.AddAsync(entity);
            return MappingHelper.ToDto(created);
        }

        public async Task<resqponseQuotesDto?> UpdateAsync(int id, searchrequestDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return null;

            existing.Author = dto.Author;
            existing.QuoteText = dto.Quote;
            existing.Tags = dto.Tags != null && dto.Tags.Any()
                ? string.Join(",", dto.Tags)
                : string.Empty;

            var updated = await _repository.UpdateAsync(existing);
            return MappingHelper.ToDto(updated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
        public async Task<List<Quotes>> SearchAsync(string? authorName, List<string>? tags, List<string>? quotes)
        {
            return await _repository.SearchAsync(authorName,tags,quotes);
        }
        public async Task<IEnumerable<QuoteDto>> AddMultipleQuotesAsync(IEnumerable<QuoteDto> dtos)
        {
            var entities = dtos.Select(d => new Quotes
            {
                Author = d.Author,
                QuoteText = d.QuoteText,
                Tags = string.Join(",", d.Tags)
            }).ToList();

            var added = await _repository.AddMultipleAsync(entities);

            return added.Select(a => new QuoteDto
            {
                Author = a.Author,
               QuoteText = a.QuoteText,
                Tags = a.Tags.Split(',').ToList()
            });
        }


    }
}
