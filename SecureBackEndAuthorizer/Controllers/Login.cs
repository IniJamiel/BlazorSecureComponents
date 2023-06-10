﻿using System.Net.Http.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SecureBackEndAuthorizer.Controllers
{
    [Route("api/[Controller]")]
    public class Login : ControllerBase
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<string> SetAuthorization()
        {
            Console.WriteLine("masuk ke API dari Class");
            return "Sukses";
        }
    }
}