using BookBuddy.Server.Enums;
using System.ComponentModel.DataAnnotations;

namespace BookBuddy.Server.Data;

public class Book
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = default!;

    [Required]
    [MaxLength(200)]
    public string Author { get; set; } = default!;

    [MaxLength(200)]
    public string? Publisher { get; set; }

    [MaxLength(20)]
    public string? ISBN { get; set; }

    [Range(0, 2100)]
    public int PublicationYear { get; set; }

    [MaxLength(100)]
    public string? Genre { get; set; }

    [Required] 
    public BookFormat Format { get; set; } = default!;

    public string? Notes { get; set; }

    public ProgrammingLanguage? ProgrammingLanguage { get; set; }
}