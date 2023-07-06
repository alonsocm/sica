using Application.DTOs.Users;

namespace Application.Interfaces
{
    public interface IActiveDirectoryService
    {
        Task<bool> IsUserValid(string userName, string password);
        Task<List<UserDto>> GetUsers(string userName);
        Task<string> GetUrlServiceCna();
        Task<bool> ValidarPorActiveDirectoryAsync();
    }
}
