namespace TinyLibrary.Application.Dtos;

public record BookDto(Guid Id, string Title, string Author, string Isbn, int TotalCopies, int AvailableCopies);

