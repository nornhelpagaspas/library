namespace library.Models;

// holds everything the comments page needs: the form and the list
public class CommentsPage
{
    // the form the visitor fills in
    public Comment NewComment { get; set; } = new Comment();

    // the comments already saved, newest first
    public List<Comment> Comments { get; set; } = new List<Comment>();
}
