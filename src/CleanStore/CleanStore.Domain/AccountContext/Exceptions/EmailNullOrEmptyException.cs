using CleanStore.Domain.SharedContext.Exceptions;

namespace CleanStore.Domain.AccountContext.Exceptions;

public class EmailNullOrEmptyException(string message) : DomainException(message);