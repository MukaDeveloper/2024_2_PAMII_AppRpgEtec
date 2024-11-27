using AppRpgEtec.ViewModels.Usuarios;

namespace AppRpgEtec.Views.Usuarios;

public partial class ImagemUsuarioView : ContentPage
{
	ImagemUsuarioViewlModel viewModel;

	public ImagemUsuarioView()
	{
		InitializeComponent();

		viewModel = new ImagemUsuarioViewlModel();
		Title = "Imagem do Usuário";
		BindingContext = viewModel;
	}
}