using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTOs;
using Api.Entities;

namespace Api.Repository.Abstracts
{
    public interface IUserRepository
    {
        void Update(AppUser user);

        Task<bool> SaveAllAsync();

        Task<IEnumerable<AppUser>> GetUsersAsync();

        Task<AppUser> GetUserAsync(int id);

        Task<AppUser> GetUserAsync(string username);

        Task<MemberDto> GetMemberAsync(string username);

        Task<IEnumerable<MemberDto>> GetMembersAsync();
    }
}