using System.Text.RegularExpressions;
using CleanStore.Domain.AccountContext.Exceptions;
using CleanStore.Domain.SharedContext.Extensions;
using CleanStore.Domain.SharedContext.ValueObjects;

namespace CleanStore.Domain.AccountContext.ValueObjects;

public sealed partial record Email : ValueObject
{
    #region Constants

    public const int MinLength = 6;
    public const int MaxLength = 160;
    public const string Pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

    #endregion

    #region Constructors

    private Email()
    {
    }

    private Email(string address, string hash)
    {
        Address = address;
        Hash = hash;
    }

    #endregion

    #region Factories

    public static Email Create(string address)
    {
        if (string.IsNullOrEmpty(address) || string.IsNullOrWhiteSpace(address))
            throw new EmailNullOrEmptyException(ErrorMessages.Email.NullOrEmpty);
        
        address = address.Trim();
        address = address.ToLower();
        
        if (!EmailRegex().IsMatch(address))
            throw new InvalidEmailException(ErrorMessages.Email.Invalid);

        return new Email(address, address.ToBase64());
    }

    #endregion

    #region Properties

    public string Address { get; private set; } = string.Empty;
    public string Hash { get; private set; } = string.Empty;

    #endregion
    
    #region Operators

    public static implicit operator string(Email email) 
        => email.ToString();

    #endregion

    #region Overrides

    public override string ToString() 
        => Address;

    #endregion

    #region Other

    [GeneratedRegex(Pattern)]
    private static partial Regex EmailRegex();

    #endregion
}