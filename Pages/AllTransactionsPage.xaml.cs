using Moneybase.ViewModels;

namespace Moneybase.Pages;

public partial class AllTransactionsPage : ContentPage
{
	public AllTransactionsPage(AllTransactionViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}