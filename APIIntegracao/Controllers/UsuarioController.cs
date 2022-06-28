using APIIntegracao.Exceptions;
using APIIntegracao.Models;
using APIIntegracao.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace APIIntegracao.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        /// <summary>
        /// Autentica o usuário
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(Token))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Login([FromBody][Required] UsuarioAutenticacao usuario)
        {
            try
            {
                var response = await _usuarioService.AutenticarUsuario(usuario);
                return Created("", response);
            }
            catch(Exception ex)
            {
                if (ex.Message.Contains("Login ou senha incorreta"))
                    return Unauthorized(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Cadastra novo usuário
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost("Cadastrar")]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(UsuarioVO))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CadastrarUsuario([FromBody] UsuarioVO usuario)
        {
            try
            {
                var response = await _usuarioService.CadastrarUsuario(usuario);
                return Created("", response);
            }
            catch(DuplicityException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
