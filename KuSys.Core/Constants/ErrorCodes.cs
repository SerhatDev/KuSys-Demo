namespace KuSys.Core.Constants;

/// <summary>
/// Error codes constants object. Made abstract to black unnecessary usages.
/// </summary>
public abstract class ErrorCodes
{
    public const string DatabaseExceptionErrorCode = "KU-504";
    public const string DatabaseExceptionDefaultMsg = "There was an error with database!";

    public const string ValidationExceptionErrorCode = "KU-411";
    public const string ValidationExceptionDefaultMsg = "There was an error with validation!";
    
    public const string DataNotFoundExceptionErrorCode = "KU-404";
    public const string DataNotFoundExceptionDefaultMsg = "Data not found!";
    
    public const string AuthenticationExceptionErrorCode = "KU-401";
    public const string AuthenticationExceptionDefaultMsg = "Authentication failed!";

}