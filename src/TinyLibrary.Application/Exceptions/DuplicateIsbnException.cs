namespace TinyLibrary.Application.Exceptions;

public class DuplicateIsbnException(string isbn) : Exception($"Ya existe un libro con el ISBN {isbn}.");
