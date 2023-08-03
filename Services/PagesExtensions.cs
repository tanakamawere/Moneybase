using Moneybase.Pages;
using Moneybase.Pages.SendMoneyPages;

namespace Moneybase.Services;

public static class PagesExtensions
{
    public static MauiAppBuilder ConfigurePages(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<HomePage>();
        builder.Services.AddSingleton<ProfilePage>();
        builder.Services.AddSingleton<WalletPage>();

        builder.Services.AddTransient<AppLandingPage>();
        builder.Services.AddTransient<CashOutPage>();
        builder.Services.AddTransient<DirectPayPage>();
        builder.Services.AddTransient<GroupPayPage>();
        builder.Services.AddTransient<RemotePayPage>();
        builder.Services.AddTransient<SendMoneyPage>();
        builder.Services.AddTransient<SignUpPage>();

        return builder;
    }
}
