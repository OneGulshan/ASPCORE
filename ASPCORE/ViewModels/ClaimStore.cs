using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ASPCORE.ViewModels
{
    public static class ClaimStore
    {
        public static List<Claim> All = new List<Claim>()
        {
            new Claim("Create Role","Create Role"),//Claim Type with its value here
            new Claim("Edit Role","Edit Role"),
            new Claim("Delete Role","Delete Role")
        };
    }
}