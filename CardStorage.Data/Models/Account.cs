using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardStorage.Data.Models;

[Table("Accounts")]
public class Account
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    [StringLength(255)]
    public string Email { get; set; }
    
    [StringLength(255)]
    public string FirstName { get; set; }
    
    [StringLength(255)]
    public string LastName { get; set; }
    
    [StringLength(127)]
    public string PasswordSalt { get; set; }
    
    [StringLength(127)]
    public string PasswordHash { get; set; }

    [InverseProperty(nameof(AccountSession.Account))]
    public virtual ICollection<AccountSession> Sessions { get; set; } = new HashSet<AccountSession>();
}