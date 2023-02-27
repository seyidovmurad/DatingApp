using Api.DTOs;
using Api.Entities;
using Api.Repository.Abstracts;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepo, IMapper mapper)
        {
            _mapper = mapper;
            _userRepo = userRepo;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var user = await _userRepo.GetUsersAsync();
            var userDto = _mapper.Map<IEnumerable<MemberDto>>(user);
            return Ok(userDto);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            var user = await _userRepo.GetUserAsync(username);

            if (user is null)
                return NotFound();
            var userDto = _mapper.Map<MemberDto>(user);
            return userDto;
        }
    }
}