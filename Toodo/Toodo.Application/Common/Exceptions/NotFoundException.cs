namespace Toodo.Application.Common.Exceptions;

public class NotFoundException(string resource, int identifier)
    : Exception($"{resource} with identifier {identifier} not found");