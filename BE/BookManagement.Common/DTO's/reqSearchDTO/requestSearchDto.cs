using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotesManagement.Common.DTO_s.reqSearchDTO
{
    public class QuoteDto
    {
        public string Author { get; set; }
        public string QuoteText { get; set; }
        public List<string> Tags { get; set; }
    }

    public class RequestSearchDto
    {
        public List<QuoteDto> Quote { get; set; }
    }

}
