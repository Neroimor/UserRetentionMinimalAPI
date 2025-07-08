using UserRetention.DataBase.DTO;

namespace UserRetention.Services
{
    public interface IUserManagement
    {

        public Task<bool> AddUserAsync(RequestUser userRequest);
        public Task<bool> UpdateUserAsync(string email, RequestUser userRequest);
        public Task<bool> DeleteUserAsync(string email);
        public Task<User?> GetUserAsync(string email);
        public Task<List<User>> GetAllUsersAsync();
    }
}
