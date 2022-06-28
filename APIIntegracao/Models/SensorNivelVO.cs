using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIIntegracao.Models
{
    public class SensorNivelVO
    {
        public int IdReservatorio { get; set; }

        public DateTime DataHora { get; set; }

        public double SensorNivel { get; set; }

        public ReservatorioVO reservatorio { get; set; }
    }

    public class SensorNivelOutput
    {
        public int IdReservatorio { get; set; }
        public DateTime DataHora { get; set; }
        public double SensorNivel { get; set; }
        public decimal AlturaReservatorio { get; set; }

        public SensorNivelOutput(SensorNivelVO sensorNivel)
        {
            IdReservatorio = sensorNivel.IdReservatorio;
            DataHora = sensorNivel.DataHora;
            SensorNivel = Convert.ToDouble(sensorNivel.reservatorio.AlturaReservatorio) - sensorNivel.SensorNivel;
            AlturaReservatorio = sensorNivel.reservatorio.AlturaReservatorio;
        }
    }
}
