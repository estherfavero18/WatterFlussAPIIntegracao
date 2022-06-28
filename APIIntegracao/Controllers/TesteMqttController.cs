using APIIntegracao.Helpers.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIIntegracao.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TesteMqttController : ControllerBase
    {
        //private readonly IMQTTHelper _mqttHelper;
        //public TesteMqttController(IMQTTHelper mqttHelper)
        //{
        //    _mqttHelper = mqttHelper;
        //}
        //[HttpPost]
        //[Route("Subscribe")]
        //public async Task<IActionResult> Subscribe(string topico)
        //{
        //    try
        //    {
        //        var sub = await _mqttHelper.Subscribe(topico);
        //        return Ok(sub);
        //    }
        //    catch(Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        //[HttpPost]
        //[Route("Publish")]
        //public async Task<IActionResult> Publish(string topico)
        //{
        //    try
        //    {
        //        var sub = await _mqttHelper.Publish(topico);
        //        return Ok(sub);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
