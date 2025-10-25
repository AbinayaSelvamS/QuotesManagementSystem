using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotesManagement.Common.Model
{
    public class Quotes
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Author { get; set; } = string.Empty;

        // Stored in DB as comma-separated values ("tag1,tag2,tag3")
        public string Tags { get; set; } = string.Empty;

        [Required]
        public string QuoteText { get; set; } = string.Empty;
    }
}
