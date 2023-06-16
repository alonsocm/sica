using Application.DTOs.Users;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IActiveDirectoryService
    {
        Task<bool> IsUserValid(string userName, string password);
        Task<List<UserDto>> GetUsers(string userName);
    }
}
