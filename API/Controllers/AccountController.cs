using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {

        private readonly DataContext _Context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext Context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _Context = Context;

        }


        // [HttpPost("registers")]
        // public async Task<ActionResult<AppUser>> Registers(string username, string password)
        // {
        //     using var hmac = new HMACSHA512();

        //     var user = new AppUser
        //     {
        //         UserName = username,
        //         PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),

        //         PasswordSalt = hmac.Key
        //     };

        //     _Context.User.Add(user);
        //     await _Context.SaveChangesAsync();

        //     return user;
        // }


        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDto)
        {
            if (await UserExists(registerDto.Username)) return BadRequest("Uesrname is taken");

            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            _Context.User.Add(user);
            await _Context.SaveChangesAsync();

          

            return new UserDTO
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDto)
        {
            var user = await _Context.User
                .SingleOrDefaultAsync(x => x.UserName == loginDto.Username);
            
            if (user == null) return Unauthorized("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computerHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computerHash.Length; i++)
            {
                if(computerHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }

          
            return new UserDTO
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UserExists(string username)
        {
            return await _Context.User.AnyAsync(x => x.UserName == username.ToLower());
            
        }
    }
}