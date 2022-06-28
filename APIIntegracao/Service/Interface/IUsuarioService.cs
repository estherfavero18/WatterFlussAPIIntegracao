using APIIntegracao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIIntegracao.Service.Interface
{
    public interface IUsuarioService
    {
        Task<Token> AutenticarUsuario(UsuarioAutenticacao usuario);
        Task<UsuarioVO> CadastrarUsuario(UsuarioVO usuario);
    }
}
