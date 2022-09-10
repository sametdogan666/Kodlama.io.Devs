using Application.Features.Auth.Commands.AuthLogin;
using Application.Features.Auth.Commands.AuthRegister;
using Application.Features.Auth.Dtos;
using Core.Security.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            AuthRegisterCommand authRegisterCommand = new() { UserForRegisterDto = userForRegisterDto }; //ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() };
            AuthRegisterDto result = await Mediator.Send(authRegisterCommand);
            return Created("", result.AccessToken);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {
            AuthLoginCommand authLoginCommand = new() { UserForLoginDto = userForLoginDto }; //ipAddress = getIpAddress() };
            AuthLoginDto result = await Mediator.Send(authLoginCommand);
            return Ok(result.CreateResponseDto());
        }
    }
}
