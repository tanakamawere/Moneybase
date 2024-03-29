﻿using Moneybase.Pages;
using Moneybase.Pages.SendMoneyPages;
using Moneybase.Pages.WalletPages;

namespace Moneybase;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(WalletPage), typeof(WalletPage));
        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));


        Routing.RegisterRoute(nameof(SendMoneyPage), typeof(SendMoneyPage));
        Routing.RegisterRoute(nameof(GroupPayPage), typeof(GroupPayPage));
        Routing.RegisterRoute(nameof(RemotePayPage), typeof(RemotePayPage));    
        Routing.RegisterRoute(nameof(DirectPayPage), typeof(DirectPayPage));
        Routing.RegisterRoute(nameof(AllTransactionsPage), typeof(AllTransactionsPage));
        Routing.RegisterRoute(nameof(CashOutPage), typeof(CashOutPage));
        Routing.RegisterRoute(nameof(CreateSavingsAccountPage), typeof(CreateSavingsAccountPage));
    }
}
