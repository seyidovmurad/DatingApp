using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Extensions
{
    public static class ClaimPrincipalExtension
    {
        public static string GetUsername(this ClaimsPrincipal user) {
            return  user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}