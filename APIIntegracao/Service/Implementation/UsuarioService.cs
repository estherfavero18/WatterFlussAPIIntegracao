using APIIntegracao.ApiCollection.Interface;
using APIIntegracao.Exceptions;
using APIIntegracao.Models;
using APIIntegracao.Service.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace APIIntegracao.Service.Implementation
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IConfiguration _configuration;
        private readonly IApiDatabase _database;

        public UsuarioService(IConfiguration configuration, IApiDatabase database)
        {
            _configuration = configuration;
            _database = database;
        }
        #region Autenticacao
        public async Task<Token> AutenticarUsuario(UsuarioAutenticacao usuario)
        {
            try
            {
                var usuarioVO = await _database.GetUsuarioByLogin(usuario);
                if (usuarioVO == null)
                    throw new InvalidOperationException("Login ou senha incorreta.");
                var token = new Token
                {
                    AccessToken = GenerateToken(usuario)
                };
                return token;
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException("Falha na autenticação: " + ex.Message);
            }
        }
        private string GenerateToken(UsuarioAutenticacao usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["TokenJWT:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, usuario.Email.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        #endregion
        #region Cadastro
        public async Task<UsuarioVO> CadastrarUsuario(UsuarioVO usuario)
        {
            await ValidaDuplicidade(usuario.Email, usuario.Cpf);

            return await _database.PostUsuario(usuario);
            
        }
        private async Task ValidaDuplicidade(string email, string cpf)
        {
            var usuario = await _database.GetUsuarioByEmail(email);

            if (usuario != null)
                throw new DuplicityException("Já existe um usuário cadastrado para o email " + email);

            usuario = await _database.GetUsuarioByCpf(cpf);

            if (usuario != null)
                throw new DuplicityException("Já existe um usuário cadastrado para o cpf " + cpf);
        }
        #endregion
    }
}
