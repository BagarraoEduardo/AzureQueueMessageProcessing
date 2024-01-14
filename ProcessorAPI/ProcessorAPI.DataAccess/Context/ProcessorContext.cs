using Microsoft.EntityFrameworkCore;
using ProcessorAPI.DataAccess.Entities;

namespace ProcessorAPI.DataAccess.Context;

public class ProcessorContext : DbContext
{
    public ProcessorContext(DbContextOptions<ProcessorContext> options) : base(options)
    {
	        
    }

    public DbSet<ParsedTransfer> ParsedTransfers { get; set; }
}
