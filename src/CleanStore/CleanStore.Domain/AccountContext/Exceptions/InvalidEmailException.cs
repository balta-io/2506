using CleanStore.Domain.SharedContext.Exceptions;

namespace CleanStore.Domain.AccountContext.Exceptions;

public class InvalidEmailException(string message) : DomainException(message);