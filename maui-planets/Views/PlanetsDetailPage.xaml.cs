using maui_planets.Models;

namespace maui_planets.Views;

public partial class PlanetsDetailPage : ContentPage
{

	public PlanetsDetailPage(Planet? planet)
	{
		InitializeComponent();
		this.BindingContext = planet;
	}

	async void BackButton_Clicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
}