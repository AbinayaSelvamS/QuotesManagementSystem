
using QuotesManagement.Common.DTO_s;
using QuotesManagement.Common.DTO_s.ResponseDTO;
using QuotesManagement.Common.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace QuotesManagement.Infrastructure.Mapper
{
    public class MappingHelper
    {
        // Entity -> Response DTO
        public static resqponseQuotesDto ToDto(Quotes entity)
        {
            return new resqponseQuotesDto
            {
                Id = entity.Id,
                Author = entity.Author,
                Quote = entity.QuoteText,
                Tags = string.IsNullOrWhiteSpace(entity.Tags)
                    ? new List<string>()
                    : entity.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                 .Select(t => t.Trim())
                                 .ToList()
            };
        }

        // Request DTO -> Entity
        public static Quotes ToEntity(searchrequestDto dto)
        {
            return new Quotes
            {
                Author = dto.Author,
                QuoteText = dto.Quote,
                Tags = dto.Tags != null && dto.Tags.Any()
                    ? string.Join(",", dto.Tags)
                    : string.Empty
            };
        }

        // List<Entity> -> List<Response DTO>
        public static List<resqponseQuotesDto> ToDtoList(IEnumerable<Quotes> entities)
        {
            return entities.Select(ToDto).ToList();
        }
    }
}
