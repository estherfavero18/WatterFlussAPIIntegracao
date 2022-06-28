using APIIntegracao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIIntegracao.ApiCollection.Interface
{
    public interface IApiDatabase
    {
        Task<UsuarioVO> GetUsuarioByLogin(UsuarioAutenticacao usuario);
        Task<UsuarioVO> GetUsuarioByEmail(string email);
        Task<UsuarioVO> GetUsuarioByCpf(string cpf);
        Task<UsuarioVO> PostUsuario(UsuarioVO usuario);
        Task PostMedicaoSensorNivel(double alturaLida);
        Task<SensorNivelVO> GetUltimaMedicaoSensorNivel(int idReservatorio);
        Task PostMedicaoSensorVazaoEntrada(double valor);
        Task<SensorVazaoVO> GetUltimaMedicaoSensorVazaoEntrada(int idReservatorio);
        Task<ReservatorioVO> GetReservatorioByUsuario(string email);
    }
}
