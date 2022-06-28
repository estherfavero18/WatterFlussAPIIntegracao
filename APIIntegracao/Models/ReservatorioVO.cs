using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIIntegracao.Models
{
    public class ReservatorioVO
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }

        public decimal AlturaReservatorio { get; set; }

        public string Categoria { get; set; }
    }
}
