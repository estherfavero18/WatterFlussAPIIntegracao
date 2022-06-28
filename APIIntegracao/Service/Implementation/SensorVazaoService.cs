using APIIntegracao.ApiCollection.Interface;
using APIIntegracao.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIIntegracao.Service.Implementation
{
    public class SensorVazaoService : ISensorVazaoService
    {
        private readonly IApiDatabase _database;
        public SensorVazaoService(IApiDatabase apiDatabase)
        {
            _database = apiDatabase;
        }

        public async Task<bool> VerificaVazaoEntrada(string emailUsuario)
        {
            var reservatorio = await _database.GetReservatorioByUsuario(emailUsuario);
            var medicao = await _database.GetUltimaMedicaoSensorVazaoEntrada(reservatorio.Id);
            return medicao.SensorVazaoEntrada > 0;
        }
    }
}
