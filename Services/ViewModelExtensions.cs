using Moneybase.ViewModels;

namespace Moneybase.Services;

public static class ViewModelExtensions
{
    public static MauiAppBuilder ConfigureViewModels(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<HomePageViewModel>();
        builder.Services.AddSingleton<WalletViewModel>();
        builder.Services.AddTransient<ProfileViewModel>();

        builder.Services.AddSingleton<AppLandingViewModel>();
        builder.Services.AddSingleton<CashOutViewModel>();
        builder.Services.AddSingleton<DirectPayViewModel>();
        builder.Services.AddSingleton<GroupPayViewModel>();
        builder.Services.AddSingleton<ReceiveMoneyViewModel>();
        builder.Services.AddSingleton<RemotePayViewModel>();

        builder.Services.AddTransient<SendMoneyViewModel>();
        builder.Services.AddTransient<AllTransactionViewModel>();
        builder.Services.AddTransient<CreateSavingsViewModel>();
        builder.Services.AddTransient<SignUpViewModel>();
        builder.Services.AddTransient<SecurityPINViewModel>();
        builder.Services.AddTransient<SetPINViewModel>();
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<OTPPageViewModel>();

        return builder;
    }
}
