using System.ComponentModel.DataAnnotations;

namespace library.Models;

// One Note object holds one row of lending.note.
public class Note
{
    public long NoteId { get; set; }                 // note_id     BIGINT, the primary key

    [StringLength(80, ErrorMessage = "The title can be at most 80 characters.")]
    public string Title { get; set; } = "";          // title       VARCHAR(80) NOT NULL

    [StringLength(500, ErrorMessage = "The note can be at most 500 characters.")]
    public string? Body { get; set; }                // body        VARCHAR(500), may be NULL

    public DateOnly CreatedOn { get; set; }          // created_on  DATE, filled by the database
}
