using System.ComponentModel.DataAnnotations;

namespace AvoTech.Models;

public class User
{
    [Key]
    public int Id { get; init; }
    
    [StringLength(39, ErrorMessage = "UserName length can't be more than 39 characters.")]
    [Required] public string UserName { get; set; } = string.Empty;
    
    [StringLength(64, ErrorMessage = "PassWordHash length can't be more than 64 characters.")]
    [Required] public string PassWordHash { get; set; } = string.Empty;
}