using APIIntegracao.ApiCollection.Interface;
using APIIntegracao.Models;
using APIIntegracao.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIIntegracao.Service.Implementation
{
    public class SensorNivelService : ISensorNivelService
    {
        private readonly IApiDatabase _database;
        public SensorNivelService(IApiDatabase apiDatabase)
        {
            _database = apiDatabase;
        }

        public async Task<SensorNivelOutput> BuscaMedicaoSensorNivel(string emailUsuario)
        {
            try
            {
                var reservatorio = await _database.GetReservatorioByUsuario(emailUsuario);
                var medicaoSensorNivel = await _database.GetUltimaMedicaoSensorNivel(reservatorio.Id);
                return new SensorNivelOutput(medicaoSensorNivel);
            }
            catch
            {
                throw new InvalidOperationException("Erro ao buscar medição.");
            }
        }
    }
}
