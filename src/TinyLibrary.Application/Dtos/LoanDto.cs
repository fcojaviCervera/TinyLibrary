namespace TinyLibrary.Application.Dtos;

public record LoanDto(Guid Id, Guid BookId, Guid MemberId, DateTimeOffset LoanedAt, DateTimeOffset DueAt, DateTimeOffset? ReturnedAt);