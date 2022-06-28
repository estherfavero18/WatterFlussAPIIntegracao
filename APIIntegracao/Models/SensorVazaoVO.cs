using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIIntegracao.Models
{
    public class SensorVazaoVO
    {
        public int IdReservatorio { get; set; }

        public DateTime DataHora { get; set; }

        public double SensorVazaoEntrada { get; set; }
        public double SensorVazaoSaida { get; set; }

        public ReservatorioVO reservatorio { get; set; }
    }
}
