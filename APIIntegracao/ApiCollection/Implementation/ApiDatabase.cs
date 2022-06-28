using APIIntegracao.ApiCollection.Interface;
using APIIntegracao.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace APIIntegracao.ApiCollection.Implementation
{
    public class ApiDatabase : IApiDatabase
    {
        private readonly string _url;
        private readonly int _reservatorioId; // Id do reservatório estático para o MVP

        public ApiDatabase(IConfiguration configuration)
        {
            _url = configuration["APIDatabase:Uri"];
            _reservatorioId = Convert.ToInt32(configuration["RepositorioId"]);
        }

        #region Usuario
        public async Task<UsuarioVO> GetUsuarioByLogin(UsuarioAutenticacao usuario)
        {
            try
            {
                var response = await _url
                                    .AppendPathSegment("Usuario")
                                    .AppendPathSegment("GetByLogin")
                                    .SetQueryParams(new
                                    {
                                        email = usuario.Email,
                                        senha = usuario.Senha
                                    })
                                    .GetJsonAsync<UsuarioVO>();
                return response;
            }
            catch
            {
                return null;
            }
        }
        public async Task<UsuarioVO> GetUsuarioByEmail(string email)
        {
            try
            {
                var response = await _url
                                    .AppendPathSegment("Usuario")
                                    .AppendPathSegment("GetByEmail")
                                    .SetQueryParam("email", email)
                                    .GetJsonAsync<UsuarioVO>();
                return response;
            }
            catch
            {
                return null;
            }
        }
        public async Task<UsuarioVO> GetUsuarioByCpf(string cpf)
        {
            try
            {
                var response = await _url
                                    .AppendPathSegment("Usuario")
                                    .AppendPathSegment("GetByCpf")
                                    .SetQueryParam("cpf", cpf)
                                    .GetJsonAsync<UsuarioVO>();
                return response;
            }
            catch
            {
                return null;
            }
        }
        public async Task<UsuarioVO> PostUsuario(UsuarioVO usuario)
        {
            try
            {
                var response = await _url
                                    .AppendPathSegment("Usuario")
                                    .PostJsonAsync(usuario)
                                    .ReceiveJson<UsuarioVO>();
                return response;
            }
            catch
            {
                throw new InvalidOperationException("Erro ao salvar usuário no banco.");
            }
        }
        #endregion
        #region Sensor de Nivel
        public async Task PostMedicaoSensorNivel(double alturaLida)
        {
            try
            {
                var medicao = new SensorNivelVO
                {
                    DataHora = DateTime.Now,
                    SensorNivel = alturaLida,
                    IdReservatorio = _reservatorioId
                };
                await _url
                        .AppendPathSegment("SensorNivel")
                        .PostJsonAsync(medicao);
            }
            catch
            {
                Console.WriteLine("erro");
            }
            
            
        }
        public async Task<SensorNivelVO> GetUltimaMedicaoSensorNivel(int idReservatorio)
        {
            return await _url
                    .AppendPathSegment("SensorNivel")
                    .AppendPathSegment("GetUltimaMedicao")
                    .SetQueryParam("idReservatorio", idReservatorio)
                    .GetJsonAsync<SensorNivelVO>();
        }
        #endregion

        #region SensorVazao
        public async Task PostMedicaoSensorVazaoEntrada(double valor)
        {
            try
            {
                var medicao = new SensorVazaoVO
                {
                    DataHora = DateTime.Now,
                    SensorVazaoEntrada = valor,
                    SensorVazaoSaida = 0, // enviando 0 pq não foi implementada a funcionalidade do sensor de saída
                    IdReservatorio = _reservatorioId
                };
                await _url
                        .AppendPathSegment("SensorVazao")
                        .PostJsonAsync(medicao);
            }
            catch
            {
                Console.WriteLine("erro");
            }
        }
        public async Task<SensorVazaoVO> GetUltimaMedicaoSensorVazaoEntrada(int idReservatorio)
        {
            return await _url
                    .AppendPathSegment("SensorVazao")
                    .AppendPathSegment("GetUltimaMedicao")
                    .SetQueryParam("idReservatorio", idReservatorio)
                    .GetJsonAsync<SensorVazaoVO>();
        }
        #endregion
        #region Reservatorio
        public async Task<ReservatorioVO> GetReservatorioByUsuario(string email)
        {
            return await _url
                    .AppendPathSegment("Reservatorio")
                    .AppendPathSegment("BuscaPorUsuario")
                    .SetQueryParam("email", email)
                    .GetJsonAsync<ReservatorioVO>();
        }
        #endregion
    }
}
