using System.ComponentModel.DataAnnotations;

namespace library.Models;

// one Comment object is one row of lending.comment
public class Comment
{
    // comment_id, the primary key
    public long CommentId { get; set; }

    // name, optional so it can be null
    [StringLength(60)]
    public string? Name { get; set; }

    // message, required and up to 500 characters
    [Required(ErrorMessage = "Please write a comment.")]
    [StringLength(500, ErrorMessage = "Please keep it under 500 characters.")]
    public string Message { get; set; } = "";

    // created_on, set by the database
    public DateTimeOffset CreatedOn { get; set; }
}
