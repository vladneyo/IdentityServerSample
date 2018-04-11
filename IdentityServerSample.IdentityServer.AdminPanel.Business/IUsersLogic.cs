using System.Collections.Generic;
using IdentityServerSample.IdentityServer.AdminPanel.Data.Dtos;

namespace IdentityServerSample.IdentityServer.AdminPanel.Business
{
    public interface IUsersLogic
    {
        List<UserDto> GetAll();

        UserDto Get(string id);

        UserDto Update(UserDto user);
    }
}
