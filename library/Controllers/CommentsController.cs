using library.Data;
using library.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace library.Controllers;

public class CommentsController : Controller
{
    private readonly CommentRepository _comments;

    // ASP.NET Core provides the registered repository.
    public CommentsController(CommentRepository comments)
    {
        _comments = comments;
    }

    // GET /Comments : show the form and the saved comments.
    public async Task<IActionResult> Index()
    {
        var page = new CommentsPage
        {
            Comments = await _comments.GetAllAsync()
        };
        return View(page);
    }

    // POST /Comments/Create : save the comment the visitor wrote.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind(Prefix = "NewComment")] Comment comment)
    {
        if (!ModelState.IsValid)
        {
            // show the page again with the error and keep what was typed
            var page = new CommentsPage
            {
                NewComment = comment,
                Comments = await _comments.GetAllAsync()
            };
            return View("Index", page);
        }

        await _comments.AddAsync(comment);
        TempData["Thanks"] = "Thank you! Your comment was sent.";
        return RedirectToAction(nameof(Index));
    }
}
