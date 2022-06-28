using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace APIIntegracao
{
    public static class Health
    {
        public static HealthCheckOptions HealthCheckOptions(this IWebHostEnvironment env)
        {
            var options = new HealthCheckOptions
            {
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "application/json";
                    await System.Text.Json.JsonSerializer.SerializeAsync(context.Response.Body, new { Status = report.Status.ToString(), API = "Database", Ambiente = env.EnvironmentName },
                        new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                }
            };
            return options;
        }
    }
}
