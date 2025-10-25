using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotesManagement.Common.DTO_s
{
    public class searchrequestDto
    {
        public string Author { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = new();
        public string Quote { get; set; } = string.Empty;

    }
}
