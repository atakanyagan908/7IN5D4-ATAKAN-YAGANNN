using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Business.Abstract;
using System.Text;

namespace _7IN5D4_ATAKAN_YAGAN.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //private static User user = new User();

        private readonly IConfiguration _configuration;
        private IUserService _userService;
        public AuthController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register (UserDto request)
        {
            List<User> users = _userService.GetAll();
            
            foreach (var _user in users)
            {
                if (request.Username == _user.Username)
                {
                    return BadRequest();
                }
            }
            
            
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = new User();
            user.Username = request.Username;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _userService.Add(user);

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(UserDto request)
        {
            
            List<User> users = _userService.GetAll();
            
            foreach (var _user in users)
            {
                if (request.Username == _user.Username)
                {
                    
                    if (!VerifyPasswordHash(request.Password, _user.PasswordHash, _user.PasswordSalt))
                    {
                        return BadRequest("Wrong password.");
                    }

                    string token = CreateToken(_user);

                    return Ok(token);
                }
            }

            return BadRequest("User not found");



            

            
        }

        private string CreateToken(User user)
        {
            

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], null,expires: DateTime.Now.AddMinutes(1),signingCredentials:creds);
                
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }


        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }

        private bool VerifyPasswordHash(string password,  byte[] passwordHash,  byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                var result = computedHash.SequenceEqual(passwordHash);
                return result;


            }



        }

        



    }
}
