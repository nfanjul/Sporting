using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace Sporting.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion("1")]
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SportingController : ControllerBase
    {
        /// <summary>
        /// Return Sporting de Gijon Players.
        /// </summary>
        [HttpGet()]
        [MapToApiVersion("1")]
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

        /// <summary>
        /// Return Sporting de Gijon Players v2.
        /// </summary>
        [HttpGet()]
        [MapToApiVersion("2")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetPlayers_V2()
        {
            var players = new List<string>
            {
                "Diego Mariño",
                "Jean-Sylvain Babin",
                "Mathieu Peybernes",
                "André Geraldes",
                "Uros Djurdjevic"
            };
            if (players == null) { return NotFound(); }
            return Ok(players);
        }
    }
}