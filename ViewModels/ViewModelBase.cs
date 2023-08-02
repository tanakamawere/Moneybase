using CommunityToolkit.Mvvm.ComponentModel;
namespace Moneybase.ViewModels;

public partial class ViewModelBase : ObservableObject
{
    [ObservableProperty]
    public bool isBusy;
    [ObservableProperty]
    public string title;
}
