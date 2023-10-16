using CardStorage.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CardStorage.Data;

public class CardStorageDbContext : DbContext
{
    public virtual DbSet<Card> Cards { get; set; }
    public virtual DbSet<User> Users { get; set; }
    
    public virtual DbSet<AccountSession> AccountSessions { get; set; }
    
    public virtual DbSet<Account> Accounts { get; set; }

    public CardStorageDbContext(DbContextOptions options) : base(options) { }
}