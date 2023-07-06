using Application.DTOs.Users;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace Shared.ActiveDirectory.Services
{
    public class ActiveDirectoryService : IActiveDirectoryService
    {
        private readonly IConfiguration _configuration;

        public ActiveDirectoryService(IConfiguration configuration)
        {
            _configuration=configuration;
        }

        public Task<List<UserDto>> GetUsers(string userName)
        {
            List<UserDto> users = new();
            DirectoryEntry de = new();
            DirectorySearcher directorySearcher = new();

            de.Path = _configuration["ActiveDirectorySettings:LDAP"];
            de.Username = _configuration["ActiveDirectorySettings:LDAPUser"];
            de.Password = _configuration["ActiveDirectorySettings:LDAPPassword"];
            de.AuthenticationType = AuthenticationTypes.Secure;

            string filter = "(&(objectCategory=person)(objectClass=user)(sAMAccountName=" + userName + "*))";
            directorySearcher.SearchRoot = de;
            directorySearcher.Filter = filter;

            foreach (SearchResult result in directorySearcher.FindAll())
            {
                UserDto user = new()
                {
                    UserName = result.Properties["samaccountname"][0].ToString() ?? string.Empty,
                    Nombre = result.Properties["givenname"][0].ToString()?? string.Empty,
                    ApellidoPaterno = result.Properties["sn"][0].ToString()!.Split(' ')[0],
                    ApellidoMaterno = result.Properties["sn"][0].ToString()!.Split(' ')[1]??string.Empty,
                    Email = result.Properties["mail"].Count == 0 ? string.Empty : result.Properties["mail"][0].ToString()
                };

                users.Add(user);
            }

            return Task.FromResult(users);
        }

        public Task<bool> IsUserValid(string userName, string password)
        {
            var nameServer = _configuration["ActiveDirectorySettings:ServerDomain"];

            using PrincipalContext pc = new(ContextType.Domain, nameServer);
            bool isValid = pc.ValidateCredentials(userName, password);
            return Task.FromResult(isValid);
        }

        public Task<string> GetUrlServiceCna()
        {
            return Task.FromResult(_configuration["ActiveDirectorySettings:WebServiceUrl"]);
        }

        public Task<bool> ValidarPorActiveDirectoryAsync()
        {
            return Task.FromResult(Convert.ToBoolean(_configuration["ActiveDirectorySettings:Activo"]));
        }
    }
}
