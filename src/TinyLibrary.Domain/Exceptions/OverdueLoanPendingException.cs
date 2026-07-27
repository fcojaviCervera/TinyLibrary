namespace TinyLibrary.Domain.Exceptions;

public class OverdueLoanPendingException() : DomainException("El préstamo ha sobrepasado la fecha de devolución.");

