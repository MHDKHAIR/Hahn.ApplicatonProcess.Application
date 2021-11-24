using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.July2021.Domain.Common;
using Hahn.ApplicatonProcess.July2021.Domain.Dtos;
using Hahn.ApplicatonProcess.July2021.WebServices.Services;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Hahn.ApplicatonProcess.July2021.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        ///  Authenticate User to the system
        /// </summary>
        /// <param name="TokenDto"></param>
        /// <returns></returns>
        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(TokenResponseDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(TokenResponseDto), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AuthenticateAsync(TokenRequestDto tokenDto)
        {
            var response = await _userService.AuthenticateAsync(tokenDto);

            if (response.Message.HasValue())
                return BadRequest(response);

            return Ok(response);
        }
        /// <summary>
        ///  Authenticate User to the system
        /// </summary>
        /// <param name="TokenDto"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [ProducesResponseType(typeof(CreateUserResposeDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(CreateUserResposeDto), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAsync(CreateUserDto dto)
        {
            var response = await _userService.Create(dto);

            if (response.Message.HasValue())
                return BadRequest(response);

            Response.StatusCode = (int)HttpStatusCode.Created;
            return Ok(response);
        }
        /// <summary>
        ///  Authenticate User to the system
        /// </summary>
        /// <param name="TokenDto"></param>
        /// <returns></returns>
        [HttpPut("update/{id}")]
        [ProducesResponseType(typeof(CreateUserResposeDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(CreateUserResposeDto), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.Unauthorized)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateAsync(int id, CreateUserDto dto)
        {
            var response = await _userService.Update(id, dto);

            if (response.Message.HasValue())
                return BadRequest(response);

            Response.StatusCode = (int)HttpStatusCode.Created;
            return Ok(response);
        }
    }
}
