using System.Security.Claims;
using Api.DTOs;
using Api.Entities;
using Api.Extensions;
using Api.Interfaces;
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
        private readonly IPhotoServices _photo;
        public UsersController(IUserRepository userRepo, IMapper mapper, IPhotoServices photo)
        {
            _photo = photo;
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

        [HttpPost()]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto dto) {

            var username = User.GetUsername();

            var user = await _userRepo.GetUserAsync(username);

            if(user is null) 
                return NotFound();

            _mapper.Map(dto, user);

            if(await _userRepo.SaveAllAsync()) 
                return NoContent();
            
            return BadRequest("Failed to update user");
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file) {
            var username = User.GetUsername();

            var user = await _userRepo.GetUserAsync(username);

            if(user is null) 
                return NotFound();

            var result = await _photo.AddPhotoAsync(file);

            if(result.Error is {})
                return BadRequest(result.Error.Message);
            
            var photo = new Photo {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
                IsMain = user.Photos.Count == 0
            };

            user.Photos.Add(photo);

            if(await _userRepo.SaveAllAsync()) {
                return CreatedAtAction(
                    nameof(GetUser),
                    new { username = user.UserName },
                    _mapper.Map<PhotoDto>(photo)
                );
            }

            return BadRequest("Problem adding photo");
            
        }

        [HttpPost("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId) {
            var user = await _userRepo.GetUserAsync(User.GetUsername());

            if(user is null)
                return NotFound();
            
            var photo = user.Photos.FirstOrDefault(p => p.Id == photoId);

            if(photo is null)
                return NotFound();

            if(photo.IsMain)
                return BadRequest("This photo is already main photo");

            var currentMain = user.Photos.FirstOrDefault(p => p.IsMain);

            if(currentMain is {}) {
                currentMain.IsMain = false;
            }

            photo.IsMain = true;

            if(await _userRepo.SaveAllAsync())
                return NoContent();

            return BadRequest("Main photo could not updated.");
        }

        [HttpGet("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId) {

            var user = await _userRepo.GetUserAsync(User.GetUsername());

            if(user is null)
                return NotFound();

            var photo = user.Photos.FirstOrDefault(p => p.Id == photoId);

            if(photo is null)
                return NotFound();
                
            if(photo.IsMain)
                return BadRequest("Can't delete main photo");

            if(photo.PublicId != null) {
                var deleteResult = await _photo.DeletePhotoAsync(photo.PublicId);

                if(deleteResult.Error is {})
                    return BadRequest(deleteResult.Error.Message);
            }
            

            user.Photos.Remove(photo);

            if(await _userRepo.SaveAllAsync())
                return NoContent();

            return BadRequest("Could not  delete photo");
        }
    }
}