using System.ComponentModel.DataAnnotations;

namespace BookBuddy.Domain;

public class Book
{
    [Key]
    public int Id { get; }

    [Required]
    [MaxLength(200)]
    public string Title { get;} = default!;

    [Required]
    [MaxLength(200)]
    public string Author { get; } = default!;

    [MaxLength(200)]
    public string? Publisher { get; }

    [MaxLength(20)]
    public string? ISBN { get; }

    [Range(0, 2100)]
    public int PublicationYear { get; set; }

    [MaxLength(100)]
    public string? Genre { get; }

    [Required]
    public BookFormat Format { get; } = default!;

    public string? Notes { get; }

    public ProgrammingLanguage? ProgrammingLanguage { get; }

    public bool IsDeleted { get; }
}
