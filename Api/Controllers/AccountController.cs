using System.Security.Cryptography;
using System.Text;
using Api.Data;
using Api.DTOs;
using Api.Entities;
using Api.Interfaces;
using Api.Repository.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    public class AccountController: BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepo;

        public AccountController(DataContext context, ITokenService tokenService, IUserRepository userRepo)
        {
            _userRepo = userRepo;
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto dto) {
            
            var exist = await _userRepo.GetUserAsync(dto.Username);
            if(exist is {})
                return BadRequest("This user already exsist");
            using var hmac = new HMACSHA512();

            var user = new AppUser {
                UserName = dto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)),
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
            };
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto dto) {

            var user = await _userRepo.GetUserAsync(dto.Username);

            if(user is null)
                return Unauthorized("Wrong username or password");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));

            for(int i = 0; i < computedHash.Length; i++) {
                if(computedHash[i] != user.PasswordHash[i])
                    return Unauthorized("Wrong username or password");
            }

            var u = new UserDto {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                PhotoUrl = user.Photos.FirstOrDefault(p => p.IsMain)?.Url
            };
            return u;
        }

        private async Task<bool> UserExsist(string username) {
            return await _context.Users.AnyAsync(u => u.UserName == username.ToLower());
        }
    }
}