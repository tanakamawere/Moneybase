using Moneybase.Pages;
using Moneybase.Pages.SendMoneyPages;
using Moneybase.Pages.WalletPages;

namespace Moneybase.Services;

public static class PagesExtensions
{
    public static MauiAppBuilder ConfigurePages(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<HomePage>();
        builder.Services.AddTransient<ProfilePage>();
        builder.Services.AddSingleton<WalletPage>();

        builder.Services.AddTransient<AppLandingPage>();
        builder.Services.AddTransient<CashOutPage>();
        builder.Services.AddTransient<DirectPayPage>();
        builder.Services.AddTransient<GroupPayPage>();
        builder.Services.AddTransient<RemotePayPage>();
        builder.Services.AddTransient<SendMoneyPage>();
        builder.Services.AddTransient<SignUpPage>();
        builder.Services.AddTransient<CreateSavingsAccountPage>();
        builder.Services.AddTransient<AllTransactionsPage>();

        return builder;
    }
}
