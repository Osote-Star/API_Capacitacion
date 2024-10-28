using WEBAPI.DTOs.User;
using WEBAPI.Models;

namespace WEBAPI.Data.Interfaces
{
    public interface IUserService
    {
        public Task<UserModel?> Create(CreateUserDto CreateUserDto);
        public Task<IEnumerable<UserModel>> FindAll();
        public Task<UserModel?> FindOne(int userId);
        public Task<UserModel?> Update(UpdateUserDto updateUserDto);
        public Task<UserModel?> Remove(int userId);
    }
}
