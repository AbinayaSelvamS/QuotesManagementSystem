using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotesManagement.Common.DTO_s.SearchDTO
{
    public class searchDto
    {
        public string? authorName { get; set; }
        public List<string>? tags { get; set; }    // automatically bound
        public List<string>? quotes { get; set; }  // automatically bound
    }
}
