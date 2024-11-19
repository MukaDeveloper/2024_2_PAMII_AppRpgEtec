using AppRpgEtec.Models;
using AppRpgEtec.Services.Usuarios;
using AppRpgEtec.Views.Personagens;
using AppRpgEtec.Views.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppRpgEtec.ViewModels.Usuarios
{
    public class UsuarioViewModel : BaseViewModel
    {
        private UsuarioService _uService;
        public ICommand AutenticarCommand { get; set; }
        public ICommand RegistrarCommand { get; set; }
        public ICommand DirecionarCadastroCommand { get; set; }

        public UsuarioViewModel()
        {
            _uService = new UsuarioService();
            InicializarCommands();
        }

        public void InicializarCommands()
        {
            AutenticarCommand = new Command(async () => await AutenticarUsuario());
            RegistrarCommand = new Command(async () => await RegistrarUsuario());
            DirecionarCadastroCommand = new Command(async () => await DirecionarParaCadastro());
        }

        #region AtributosPropriedades
        //CTRL + R + E → Cria a propriedade do atributo
        private string login = string.Empty;
        public string Login { 
            get { return login; } 
            set
            {
                login = value; 
                OnPropertyChanged();
            }
        }
        private string senha = string.Empty;
        public string Senha { 
            get { return senha; }
            set 
            { 
                senha = value; 
                OnPropertyChanged();
            } 
        }
        #endregion

        private CancellationTokenSource _cancelTokenSource;
        private bool _isCheckingLocation;

        #region Métodos
        public async Task AutenticarUsuario()
        {
            try
            {
                Usuario u = new Usuario();
                u.Username = Login;
                u.PasswordString = Senha;
                Usuario uAutenticado = await _uService.PostAutenticarUsuarioAsync(u);

                if (!string.IsNullOrEmpty(uAutenticado.Token))
                {
                    string mensagem = $"Bem vindo, {u.Username}!";
                    Preferences.Set("UsuarioToken", uAutenticado.Token);
                    Preferences.Set("UsuarioId", uAutenticado.Id);
                    Preferences.Set("UsuarioUsername", uAutenticado.Username);
                    Preferences.Set("UsuarioPerfil", uAutenticado.Perfil);

                    _isCheckingLocation = true;
                    _cancelTokenSource = new CancellationTokenSource();
                    GeolocationRequest request =
                        new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                    Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                    Usuario uLoc = new Usuario();
                    uLoc.Id = uAutenticado.Id;
                    uLoc.Latitude = uAutenticado.Latitude;
                    uLoc.Longitude = uAutenticado.Longitude;

                    UsuarioService uServiceLoc = new UsuarioService(uAutenticado.Token);

                    await uServiceLoc.PutAtualizarLocalizacaoAsync(uLoc);

                    await Application.Current.MainPage
                        .DisplayAlert("Informação", mensagem, "Ok");

                    // await DirecionarParaListagem();
                    Application.Current.MainPage = new AppShell();
                    // Application.Current.MainPage.Navigation.PushAsync(new ListagemView());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Informação", "Dados incorretos", "Ok");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async Task RegistrarUsuario()
        {
            try
            {
                Usuario u = new Usuario();
                u.Username = Login;
                u.PasswordString = Senha;
                Usuario uRegistrado = await _uService.PostRegistrarUsuarioAsync(u);

                if (uRegistrado.Id != 0)
                {
                    string mensagem = $"Usuário Id {uRegistrado.Id} registrado com sucesso!";

                    await Application.Current.MainPage
                        .DisplayAlert("Informação", mensagem, "Ok");

                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Informação", "Dados incorretos", "Ok");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async Task DirecionarParaCadastro()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new CadastroView());
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }

        public async Task DirecionarParaListagem()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new AppRpgEtec.Views.Personagens.ListagemView());
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Informação", ex.Message + " Detalhes: " + ex.InnerException, "Ok");
            }
        }
        #endregion
    }
}
