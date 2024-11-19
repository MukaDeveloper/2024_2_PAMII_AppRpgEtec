using AppRpgEtec.Models;
using AppRpgEtec.Services.Usuarios;
using System.Collections.ObjectModel;

namespace AppRpgEtec.ViewModels.Usuarios
{
    public class LocalizacaoViewModel : BaseViewModel
    {
        private UsuarioService uService;
        public LocalizacaoViewModel() 
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            uService = new UsuarioService(token);
        }

        public async void ExibirUsuariosNoMapa()
        {
            try
            {
                ObservableCollection<Usuario> ocUsuarios = await uService.GetUsuariosAsync();
                List<Usuario> listaUsuarios = new List<Usuario>(ocUsuarios);
                // Map map = new Map();


            }
            catch (Exception ex)
            {

            }
        }
    }
}
