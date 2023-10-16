using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardStorage.Data.Models;

[Table("AccountSessions")]
public class AccountSession
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    [Required]
    [StringLength(512)]
    public string SessionToken { get; set; }
    
    [ForeignKey(nameof(Account))]
    public long AccountId { get; set; }
    
    [Column(TypeName = "datetime")]
    public DateTime TimeCreated { get; set; }
    
    [Column(TypeName = "datetime")]
    public DateTime TimeClosed { get; set; }
    
    public bool IsClosed { get; set; }
    
    public virtual Account Account { get; set; }
}