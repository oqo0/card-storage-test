using FullSearchSamples.Entity;
using Microsoft.EntityFrameworkCore;

namespace FullSearchSamples;

public class DocDbContext : DbContext
{
    public virtual DbSet<Document> Documents { get; set; }

    public DocDbContext(DbContextOptions options) : base(options) { }
}