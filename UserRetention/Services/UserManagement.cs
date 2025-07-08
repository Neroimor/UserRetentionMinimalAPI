using UserRetention.DataBase.DTO;

namespace UserRetention.Services
{
    public class UserManagement : IUserManagement
    {
        public Task<bool> AddUserAsync(RequestUser userRequest)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUserAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserAsync(string email, RequestUser userRequest)
        {
            throw new NotImplementedException();
        }
    }
}
