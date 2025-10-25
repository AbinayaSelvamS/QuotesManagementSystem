using Microsoft.EntityFrameworkCore;
using QuotesManagement.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotesManagement.Data.DB
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        // DbSet for Quotes table
        public DbSet<Quotes> quotes { get; set; }


    }
}
