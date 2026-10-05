namespace MyCoffeeApp2;

public partial class CoffeeDetailsPage : ContentPage
{
	public CoffeeDetailsPage(CoffeeDetailsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}