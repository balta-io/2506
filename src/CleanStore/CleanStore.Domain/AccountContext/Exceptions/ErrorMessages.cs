namespace CleanStore.Domain.AccountContext.Exceptions;

public class ErrorMessages
{
    #region Properties
    public static EmailErrorMessages Email { get; } = new();

    #endregion
    
    public class EmailErrorMessages
    {
        public string Invalid { get; } = "E-mail inválido";
        public string NullOrEmpty { get; } = "E-mail não pode ser nulo ou vazio.";
    }
}