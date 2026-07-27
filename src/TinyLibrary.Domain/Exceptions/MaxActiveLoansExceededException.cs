namespace TinyLibrary.Domain.Exceptions;

public class MaxActiveLoansExceededException() : DomainException("Se ha excedido el número máximo de prestamos permitido");

