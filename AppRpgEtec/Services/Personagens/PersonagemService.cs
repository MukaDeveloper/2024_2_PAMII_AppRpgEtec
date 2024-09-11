﻿using AppRpgEtec.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRpgEtec.Services.Personagens
{
    public class PersonagemService : Request
    {
        private readonly Request _request;
        private const string apiUrlBase = "https://rpgapi20242pam.azurewebsites.net/Personagens";
        private string _token;

        public PersonagemService(string token)
        {
            _request = new Request();
            _token = token;
        }

        public async Task<Personagem> PostPersonagemAsync(Personagem p)
        {
            return await _request.PostAsync(apiUrlBase, p, _token);
        }

        public async Task<ObservableCollection<Personagem>> GetPersonagensAsync()
        {
            string urlComplementar = string.Format("/{0}", "/GetAll");
            ObservableCollection<Personagem> listaPersonagens = 
                await _request.GetAsync<ObservableCollection<Personagem>>(apiUrlBase + urlComplementar, _token);

            return listaPersonagens;
        }

        public async Task<Personagem> GetPersonagemAsync(int personagemId)
        {
            string urlComplementar = string.Format("/{0}", personagemId);
            var peronsagem = await _request.GetAsync<Personagem>(apiUrlBase + urlComplementar, _token);

            return peronsagem;
        }

        public async Task<int> PutPersonagemAsync(Personagem p)
        {
            var result = await _request.PutAsync(apiUrlBase, p, _token);

            return result;
        }

        public async Task<int> DeletePersonagemAsync(int personagemId)
        {
            string urlComplementar = string.Format("/{0}", personagemId);
            var result = await _request.DeleteAsync(apiUrlBase + urlComplementar, _token);

            return result;
        }
    }
}