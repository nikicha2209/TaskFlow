using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskFlow.Data.Models.Common.Constants;

namespace TaskFlow.Data.Models;

public class Comment
{
    [Key]
    public int Id { get; set; }

    [Required] 
    [StringLength(ValidationConstants.Comment.ContentMaxLength)]
    public string Content { get; set; } = string.Empty;

    [ForeignKey(nameof(TaskItem))]
    public int TaskItemId { get; set; }

    public TaskItem TaskItem { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Author))]
    public string AuthorId { get; set; } = string.Empty;

    public ApplicationUser Author { get; set; } = null!;

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedOn { get; set; }
}
