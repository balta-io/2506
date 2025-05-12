namespace CleanStore.Application.SharedContext.Results;

public record Error(string Code, string Message)
{
    public static Error None = new(string.Empty, string.Empty);
    public static Error NullValue = new("Error.NullValue", "Um valor nulo foi fornecido.");
}