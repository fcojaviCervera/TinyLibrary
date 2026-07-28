namespace TinyLibrary.Application.Exceptions;

public class DuplicateEmailMemberException(string email) : Exception($"Ya existe un usuario con el email {email}");

