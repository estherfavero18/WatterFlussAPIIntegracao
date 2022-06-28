using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace APIIntegracao.Helpers.Implementation
{
    public static class HelperController
    {
        /// <summary>
        /// Método para busca email de usuário logado
        /// </summary>
        /// <param name="User">Usuario logado</param>
        /// <returns>Retorna o email encontrado</returns>
        public static string getEmailLogado(ClaimsPrincipal User)
        {
            var Claim = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress");

            return Claim.Value;
        }
    }
}
