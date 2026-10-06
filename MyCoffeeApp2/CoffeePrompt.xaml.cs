namespace MyCoffeeApp2;

public partial class CoffeePrompt : ContentPage
{
	public CoffeePrompt()
	{
		InitializeComponent();
	}

	private async void OnSubmitClicked(object sender, EventArgs e)
	{
		string name = NameInput.Text;
		string roaster = RoasterInput.Text;
		string description = DescriptionInput.Text;
	}
}