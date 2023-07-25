using Android.App;
using Android.Content;
using Microsoft.Identity.Client;

namespace Moneybase.Platforms.Android;

[Activity(Exported = true)]
[IntentFilter(new[] { Intent.ActionView },
    Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault },
    DataHost = "auth",
    DataScheme = "msald4cbda4b-571a-41df-ba5f-8515ab52b5d5")]
public class MsalActivity : BrowserTabActivity
{
}
