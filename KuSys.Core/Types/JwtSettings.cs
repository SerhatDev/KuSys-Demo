namespace KuSys.Core.Types;

/// <summary>
/// JWT settings holder class. It will be used to bind appSettings.
/// </summary>
public sealed class JwtSettings
{
    /// <summary>
    /// Issuer.
    /// </summary>
    public string Issuer { get; set; }
    
    /// <summary>
    /// Audience
    /// </summary>
    public string Audience { get; set; }
    
    /// <summary>
    /// Secret Key.
    /// </summary>
    public string Secret { get; set; }
    
    /// <summary>
    /// Token lifetime in minutes.
    /// </summary>
    public int ExpiresIn { get; set; }
}