using InstaGama.Domain.Core.Interfaces;
using InstaGama.Application.AppPostage.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaGama.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelationshipController : ControllerBase
    {
        private readonly IRelationshipAppService _relationshipService;
        //private readonly ILogged _logged;

        public RelationshipController(IRelationshipAppService relationshipAppService)
        {
            _relationshipService = relationshipAppService;
        }

        [Authorize]
        [HttpPost]
        [Route("RequestConnection/{idSolicitado}")]
        public async Task<IActionResult> RequestConnection([FromRoute] int idSolicitado)
        {
            try
            {
                var relationship = await _relationshipService.RequestConnection(idSolicitado).ConfigureAwait(false);
                return Created("", relationship);
            }
            catch(ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPatch]
        [Route("AcceptConnection/{idSolicitante}")]
        public async Task<IActionResult> AcceptConnection([FromRoute] int idSolicitante)
        {
            try
            {
                var relationship = await _relationshipService.AcceptConnection(idSolicitante).ConfigureAwait(false);
                return Ok(relationship);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPatch]
        [Route("DeclineConnection/{idSolicitante}")]
        public async Task<IActionResult> DeclineConnection([FromRoute] int idSolicitante)
        {
            try
            {
                var relationship = await _relationshipService.DeclineConnection(idSolicitante).ConfigureAwait(false);
                return Ok(relationship);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllRelationshipRequests")]
        public async Task<IActionResult> GetAllRelationshipRequests()
        {
            var allRequests = await _relationshipService.GetAllRelationshipRequests().ConfigureAwait(false);

            if (allRequests is null)
                return NoContent();

            return Ok(allRequests);
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllAcceptedRequests")]
        public async Task<IActionResult> GetAllAcceptedRequests()
        {
            var acceptedRequests = await _relationshipService.GetAllAcceptedRequests().ConfigureAwait(false);

            Console.WriteLine("'Accepted: " + acceptedRequests);

            if (acceptedRequests is null)
                return NoContent();

            return Ok(acceptedRequests);
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllDeclinedRequests")]
        public async Task<IActionResult> GetAllDeclinedRequests()
        {
            var declinedRequests = await _relationshipService.GetAllDeclinedRequests().ConfigureAwait(false);

            if (declinedRequests is null)
                return NoContent();

            return Ok(declinedRequests);  
        }
    }
}