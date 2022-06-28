using APIIntegracao.Helpers.Implementation;
using APIIntegracao.Models;
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
    public class SensorNivelController : ControllerBase
    {
        private readonly ISensorNivelService _sensorNivelService;
        public SensorNivelController(ISensorNivelService sensorNivelService)
        {
            _sensorNivelService = sensorNivelService;
        }
        [HttpGet("BuscarMedicao")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SensorNivelOutput))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> BuscarMedicao()
        {
            try
            {
                var email = HelperController.getEmailLogado(User);
                var response = await _sensorNivelService.BuscaMedicaoSensorNivel(email);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
