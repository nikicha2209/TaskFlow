using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskFlow.Data.Models.Common.Constants;

namespace TaskFlow.Data.Models;

public class Notification
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(ValidationConstants.Notification.MessageMaxLength)]
    public string Message { get; set; } = string.Empty;

    public bool IsRead { get; set; }

    [Required]
    [ForeignKey(nameof(User))]
    public string UserId { get; set; } = string.Empty;

    public ApplicationUser User { get; set; } = null!;

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
