using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardStorage.Data.Models;

[Table("Cards")]
public class Card
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    [Column, StringLength(16)]
    public string Number { get; set; }
    
    [Column, StringLength(255)]
    public string OwnerName { get; set; }
    
    [Column, StringLength(16)]
    public string CVV2 { get; set; }
    
    [Column]
    public DateTime ExpDateTime { get; set; }
    
    public ulong OwnerId { get; set; }
    
    [ForeignKey(nameof(OwnerId))]
    public virtual User User { get; set; }
}