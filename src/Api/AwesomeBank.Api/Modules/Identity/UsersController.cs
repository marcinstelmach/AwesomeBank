﻿namespace AwesomeBank.Api.Modules.Identity
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> TestAsync()
        {
            await Task.CompletedTask;
            return Ok("Hello world");
        }
    }
}