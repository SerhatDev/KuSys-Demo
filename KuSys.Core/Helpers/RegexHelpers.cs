using System.Text.RegularExpressions;

namespace KuSys.Core.Helpers;

/// <summary>
/// Regex helpers class.
/// </summary>
public static class RegexHelpers
{
    /// <summary>
    /// Checks if provided string value is in correct email format.
    /// </summary>
    /// <param name="emailAddress">Email address to validate</param>
    /// <returns>True if format is correct otherwise returns false</returns>
    public static bool IsCorrectMailFormat(this string emailAddress)
    {
        Regex emailRegex = new Regex(
            @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);
        return emailRegex.IsMatch(emailAddress);
    }

    /// <summary>
    /// Checks if provided string value is in required username format.
    /// </summary>
    /// <param name="userName">User name to validate</param>
    /// <returns>True if format is correct otherwise returns false</returns>
    public static bool IsValidUserName(this string userName)
    {
        Regex usernameRegex = new Regex(@"^[a-zA-Z0-9]+$", RegexOptions.Compiled);
        return usernameRegex.IsMatch(userName);
    }
}