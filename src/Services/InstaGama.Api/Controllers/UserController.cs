using InstaGama.Application.AppPostage.Interfaces;
using InstaGama.Application.AppUser.Input;
using InstaGama.Application.AppUser.Interfaces;
using InstaGama.Domain.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaGama.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserAppService _userAppService;
        private readonly IRelationshipAppService _relationshipService;
        //  private readonly ILogged _logged;

        public UserController(IUserAppService userAppService, IRelationshipAppService relationshipService)
        {
            _userAppService = userAppService;
            _relationshipService = relationshipService;
        }
        
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserInput userInput)
        {
            try
            {
                var user = await _userAppService
                                    .InsertAsync(userInput)
                                    .ConfigureAwait(false);

                return Created("", user);
            }
            catch(ArgumentException arg)
            {
                return BadRequest(arg.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var user = await _userAppService
                                .GetByIdAsync(id)
                                .ConfigureAwait(false);

            if (user is null)
                return NotFound();

            return Ok(user);
        }

       /* [Authorize]
        [HttpGet]
        [Route("GetAllAcceptedRequests")]
        public async Task<IActionResult> GetAllAcceptedRequests()
        {
            var acceptedRequests = await _relationshipService.GetAllAcceptedRequests().ConfigureAwait(false);

            Console.WriteLine("'Accepted: " + acceptedRequests);

            if (acceptedRequests is null)
                return NoContent();

            return Ok(acceptedRequests);
        }*/

        /* [Authorize]
         [HttpGet]
         [Route("/Gallery")]
         public async Task<IActionResult> GetGallery()
         {

             var userId = _logged.GetUserLoggedId();
             try
             {
                 var gallery = await _postageAppService
                                         .GetGalleryByUserIdAsync(userId)
                                         .ConfigureAwait(false);

                 return Ok(gallery);
             }
             catch (ArgumentException arg)
             {
                 return BadRequest(arg.Message);
             }
         }*/
    }
}
