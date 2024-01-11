using System.ComponentModel.DataAnnotations;

namespace ProcessorAPI.DataAccess;

public class ParsedTransfer
{
    public int Id { get; set; }
    
    [Required]
    public Guid Reference { get; set; }
    
    [Required]
    [MaxLength(34)]
    public string From { get; set; }
    
    [Required]
    [MaxLength(34)]
    public string To { get; set; }
    
    [Required]
    public decimal Amount { get; set; }
    
    [Required]
    [StringLength(3)]
    public string Currency { get; set; }
}
