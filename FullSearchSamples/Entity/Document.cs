using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FullSearchSamples.Entity;

[Table("Documents")]
public class Document
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    [Column]
    [MaxLength(65534)]
    public string Content { get; set; }
}