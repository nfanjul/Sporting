﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace Sporting.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SportingController : ControllerBase
    {
        /// <summary>
        /// Return Sporting de Gijon Players.
        /// </summary>
        [HttpGet()]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetPlayers()
        {
            var players = new List<string>
            {
                "Diego Mariño",
                "Jean-Sylvain Babin",
                "Mathieu Peybernes"
            };
            if (players == null) { return NotFound(); }
            return Ok(players);
        }
    }
}