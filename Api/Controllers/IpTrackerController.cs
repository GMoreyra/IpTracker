﻿using Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IpTrackerController : ControllerBase
    {
        private readonly IIpTrackerService _service;

        public IpTrackerController(IIpTrackerService service)
        {
            _service = service;
        }

        /// <summary>
        /// Returns the ip info based on the ip number
        /// </summary>
        /// <param name="ipNumber">Ip number</param>
        /// <returns></returns>
        [HttpGet("{ipNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetData(string ipNumber)
        {
            var response = await _service.GetIpInfo(ipNumber);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}
