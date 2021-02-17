using InstaGama.Application.AppRelationships.Input;
using InstaGama.Application.AppRelationships.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace InstaGama.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelationshipsController : ControllerBase
    {
        private readonly IRelationshipsAppService _relationshipsAppService;

        public RelationshipsController(IRelationshipsAppService relationshipsAppService)
        {
            _relationshipsAppService = relationshipsAppService;
        }

        [Authorize]
        [HttpPost]
        [Route("Follow/{id}")]
        public async Task<IActionResult> Post([FromRoute] RelationshipsInput relationshipsInput)
        {
            try
            {
                var relationship = await _relationshipsAppService
                                    .InsertAsync(relationshipsInput)
                                    .ConfigureAwait(false);

                return Created("", relationship);
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("Following")]
        public async Task<IActionResult> GetFollowing()
        {
            try
            {
                var relationship = await _relationshipsAppService
                                        .GetRelationshipsByUserIdAsync()
                                        .ConfigureAwait(false);

                return Ok(relationship);
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg.Message);
            }
        }
    }
}
