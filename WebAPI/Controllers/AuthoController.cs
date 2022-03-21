using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.Servers;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthoController : ControllerBase
    {
        private readonly IUserServer _userServer;
        private readonly JWTServer _jwtServer;

        public AuthoController(IUserServer userServer, JWTServer jwtServer)
        {
            _userServer = userServer;
            _jwtServer = jwtServer;
        }

        [HttpPost("Register")]
        public IActionResult Register(RegisterDto dto)
        {
            var user = new User
            {               
                UserName = dto.UserName,
                Email = dto.Email,  
                Address = dto.Address,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            };
            return Created("success", _userServer.Create(user));
        }

        [HttpPost("Login")]
        public IActionResult Login (AuthenticaDto dto)
        {
            var user = _userServer.GetByEmail(dto.Email);
            if (user == null) return BadRequest("Invalid Credentials");
            //check pass
            if(!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password)){
                return BadRequest("Invalid Credentials");
            }

            var jwt = _jwtServer.Generate(user.Id);
            Response.Cookies.Append(key:"jwt", value: jwt, new CookieOptions
            {
                HttpOnly = true,
            });
            return Ok(new { message = "success", jwt }); 
        }
        [HttpGet]
        public IActionResult GetUser()
        {
            return Ok(_userServer.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult GetId (int id)
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                var token = _jwtServer.Verify(jwt);
                var userId = int.Parse(token.Issuer);
                var user = _userServer.GetById(id);
                return Ok(user);
            }
            catch (Exception _)
            {
                return Unauthorized();
            }
        }
        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete(key:"jwt");
            return Ok(new { message = "success" });
        }
    }
}
