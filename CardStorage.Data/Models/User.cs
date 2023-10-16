using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardStorage.Data.Models;

[Table("Users")]
public class User
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public ulong Id { get; set; }
    
    [Column, StringLength(255)]
    public string FirstName { get; set; }
    
    [Column, StringLength(255)]
    public string SureName { get; set; }

    [InverseProperty(nameof(Card.User))]
    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();
}