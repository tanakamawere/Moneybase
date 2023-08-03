namespace Moneybase.Templates;

public partial class OptionCardTemplate : ContentView
{
    public static readonly BindableProperty TitleProperty = BindableProperty.
		Create(nameof(Title), typeof(string), typeof(OptionCardTemplate), propertyChanged: (bindable, oldValue, newValue) => 
		{
			var template = (OptionCardTemplate)bindable;
			template.TitleLabel.Text = (string)newValue;
		});
    public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(OptionCardTemplate), propertyChanged:(bindable, oldValue, newValue) => 
	{
		var template = (OptionCardTemplate)bindable;
		template.IconImage.Glyph = (string)newValue;
	});

    public string Title
	{
		get => (string)GetValue(TitleProperty);
		set => SetValue(TitleProperty, value);
	}

	public string Icon
    {
        get => (string)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

	public OptionCardTemplate()
	{
		InitializeComponent();
	}
}