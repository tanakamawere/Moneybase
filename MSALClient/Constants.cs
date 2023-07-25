namespace Moneybase.MSALClient;

public static class Constants
{
    public static readonly string Instance = "moneybasezw.b2clogin.com";
    public static readonly string[] Scopes = { "openid", "offline_access" };
    public static readonly string Domain = "moneybasezw.onmicrosoft.com";
    public static readonly string ClientId = "d4cbda4b-571a-41df-ba5f-8515ab52b5d5";
    public static readonly string SignUpSignInPolicyId = "B2C_1_susi";
    public static readonly string ResetPasswordPolicyId = "B2C_1_reset";
    public static readonly string EditProfilePolicyId = "B2C_1_edit_profile";
    public static readonly string CacheFileName = "";
    public static readonly string CacheDir = "";
    public static readonly string iOSKeyChainGroup = "com.companyname.moneybase";

    public static readonly string RedirectUri = $"msal{ClientId}://auth";
    public static readonly string AuthorityBase = $"https://{Instance}/tfp/{Domain}/";
    public static readonly string AuthoritySignInSignUp = $"{AuthorityBase}{SignUpSignInPolicyId}";
}
