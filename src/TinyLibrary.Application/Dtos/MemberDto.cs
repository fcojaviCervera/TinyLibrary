namespace TinyLibrary.Application.Dtos;
public record MemberDto(Guid Id, string Name, string Email, DateTimeOffset JoinedAt, DateTimeOffset? PenalizedUntil);

