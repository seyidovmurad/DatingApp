using Api.Data;
using Api.DTOs;
using Api.Entities;
using Api.Repository.Abstracts;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository.Concretes
{
    public class UserReposiotry : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper __mapper;
        public UserReposiotry(DataContext context, IMapper _mapper)
        {
            __mapper = _mapper;
            _context = context;
            
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await _context.Users
                        .Where(u => u.UserName == username)
                        .ProjectTo<MemberDto>(__mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            return await _context.Users
                        .ProjectTo<MemberDto>(__mapper.ConfigurationProvider)
                        .ToListAsync();
        }

        public async Task<AppUser> GetUserAsync(string username)
        {
            return await _context.Users
                        .Include(u => u.Photos)
                        .FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<AppUser> GetUserAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
           return await _context.Users
                        .Include(u => u.Photos)
                        .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}