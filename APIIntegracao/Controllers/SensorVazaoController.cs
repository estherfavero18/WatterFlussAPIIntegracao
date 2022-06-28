using APIIntegracao.Helpers.Implementation;
using APIIntegracao.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace APIIntegracao.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SensorVazaoController : ControllerBase
    {
        private readonly ISensorVazaoService _sensorVazaoService;
        public SensorVazaoController(ISensorVazaoService sensorVazaoService)
        {
            _sensorVazaoService = sensorVazaoService;
        }
        /// <summary>
        /// Verifica se está com abastecimento de água
        /// </summary>
        /// <returns></returns>
        [HttpGet("VerificaAbastecimento")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(bool))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> BuscarMedicao()
        {
            try
            {
                var email = HelperController.getEmailLogado(User);
                var response = await _sensorVazaoService.VerificaVazaoEntrada(email);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
