namespace TinyLibrary.Application.Dtos;

public record RegisterBookRequest(string Title, string Author, string Isbn, int TotalCopies);    

