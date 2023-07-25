﻿namespace Moneybase.MSALClient;

public class PlatformConfig
{
    /// <summary>
    /// Instance to store data
    /// </summary>
    public static PlatformConfig Instance { get; } = new PlatformConfig();

    /// <summary>
    /// Platform specific Redirect URI
    /// </summary>
    public string RedirectUri { get; set; } = $"msal{Constants.ClientId}://auth";

    /// <summary>
    /// Platform specific parent window
    /// </summary>
    public object ParentWindow { get; set; }

    // private constructor to ensure singleton
    private PlatformConfig()
    {
    }
}
