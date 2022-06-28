using APIIntegracao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIIntegracao.Service.Interface
{
    public interface ISensorNivelService
    {
        Task<SensorNivelOutput> BuscaMedicaoSensorNivel(string emailUsuario);
    }
}
