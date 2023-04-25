using BookBuddy.Server.Enums;
using System.ComponentModel.DataAnnotations;

namespace BookBuddy.Server.Data;

public class Book
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = default!;

    [Required]
    public string Author { get; set; } = default!;

    public string? Publisher { get; set; }

    public string? ISBN { get; set; }

    [Range(0, 2100)]
    public int PublicationYear { get; set; }

    public string? Genre { get; set; }

    [Required] 
    public BookFormat Format { get; set; } = default!;

    public string? Notes { get; set; }

    public string? ProgrammingLanguage { get; set; }
}