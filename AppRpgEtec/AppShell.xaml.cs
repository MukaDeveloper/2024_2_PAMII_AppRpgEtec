﻿using AppRpgEtec.ViewModels;
using AppRpgEtec.Views.Personagens;

namespace AppRpgEtec
{
    public partial class AppShell : Shell
    {

        AppShellViewModel viewModel;

        public AppShell()
        {
            InitializeComponent();

            viewModel = new AppShellViewModel();
            BindingContext = viewModel;

            string login = Preferences.Get("UsuarioUsername", string.Empty);
            lblLogin.Text = login;

            Routing.RegisterRoute("cadastroPersonagemView", typeof(CadastroPersonagemView));
        }
    }
}
