using library.Models;
using Npgsql;
using System.Xml.Linq;

namespace library.Data;

// Every SQL statement about comments lives in this class.
public class CommentRepository
{
    private readonly NpgsqlDataSource _dataSource;

    // ASP.NET Core provides the registered data source.
    public CommentRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    // READ: get all comments, newest first.
    public async Task<List<Comment>> GetAllAsync()
    {
        const string sql =
            "SELECT comment_id, name, message, created_on " +
            "FROM lending.comment ORDER BY created_on DESC;";

        var comments = new List<Comment>();

        await using var command = _dataSource.CreateCommand(sql);
        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            comments.Add(new Comment
            {
                CommentId = reader.GetInt64(0),
                Name = reader.IsDBNull(1) ? null : reader.GetString(1),
                Message = reader.GetString(2),
                CreatedOn = reader.GetFieldValue<DateTimeOffset>(3)
            });
        }

        return comments;
    }

    // CREATE: save a new comment. The database sets the id and the time.
    public async Task AddAsync(Comment comment)
    {
        const string sql =
            "INSERT INTO lending.comment (name, message) " +
            "VALUES (@name, @message);";

        await using var command = _dataSource.CreateCommand(sql);

        // an empty name is saved as NULL
        var name = string.IsNullOrWhiteSpace(comment.Name) ? null : comment.Name.Trim();
        command.Parameters.AddWithValue("name", (object?)name ?? DBNull.Value);
        command.Parameters.AddWithValue("message", comment.Message.Trim());

        await command.ExecuteNonQueryAsync();
    }
}
