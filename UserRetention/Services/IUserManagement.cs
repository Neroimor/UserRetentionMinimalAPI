using UserRetention.DataBase.DTO;

namespace UserRetention.Services
{
    public interface IUserManagement
    {

        public Task<ResponseUser<User>?> AddUserAsync(RequestUser userRequest);
        public Task<ResponseUser<User>?> UpdateUserAsync(string email, RequestUser userRequest);
        public Task<ResponseUser<User>?> DeleteUserAsync(string email);
        public Task<ResponseUser<User>?> GetUserAsync(string email);
        public Task<List<User>> GetAllUsersAsync();
    }
}
