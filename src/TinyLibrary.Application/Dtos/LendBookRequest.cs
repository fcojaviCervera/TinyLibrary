namespace TinyLibrary.Application.Dtos;

public record LendBookRequest(Guid MemberId, Guid BookId);