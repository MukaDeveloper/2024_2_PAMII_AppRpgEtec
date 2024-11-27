using AppRpgEtec.ViewModels.Disputas;

namespace AppRpgEtec.Views.Personagens;

public partial class ListagemView : ContentPage
{
	DisputaViewModel viewModel;

	public ListagemView()
	{
		InitializeComponent();

		viewModel = new DisputaViewModel();
		BindingContext = viewModel;
	}
}