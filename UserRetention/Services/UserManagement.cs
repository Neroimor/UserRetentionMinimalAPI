using Microsoft.EntityFrameworkCore;
using UserRetention.DataBase;
using UserRetention.DataBase.DTO;

namespace UserRetention.Services
{
    public class UserManagement : IUserManagement
    {
        private readonly ILogger<UserManagement> _logger;
        private readonly AppDBContext appDBContext;
        public UserManagement(ILogger<UserManagement> logger, AppDBContext appDBContext)
        {
            _logger = logger;
            this.appDBContext = appDBContext;
        }

        public async Task<ResponseUser<User>?> AddUserAsync(RequestUser userRequest)
        {
            if (userRequest == null)
            {
                _logger.LogError("User request is null.");
                return CreateResponseUser(null, 400, "User request cannot be null.", false);
            }
            var user = CreateUser(userRequest);
            _logger.LogInformation("Adding user: {Name}, Email: {Email}", user.Name, user.Email);

            var checkUser = await appDBContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (checkUser != null)
            {
                _logger.LogWarning("User with email {Email} already exists.", user.Email);
                return CreateResponseUser(null, 409, "User already exists.", false);
            }

            await appDBContext.Users.AddAsync(user);
            await appDBContext.SaveChangesAsync();
            _logger.LogInformation("User {Name} added successfully.", user.Name);

            return CreateResponseUser(user, 201, "User added successfully.", true);
        }

        private User CreateUser(RequestUser requestUser)
        {
            return new User
            {
                Name = requestUser.Name,
                Email = requestUser.Email,
                CreatedAt = DateTime.UtcNow
            };
        }

        private ResponseUser<User> CreateResponseUser(User? user, int _code, string message, bool Success)
        {
            return new ResponseUser<User>
            {

                Message = message,
                StatusCode = _code,
                Data = user,
                Success = Success

            };
        }

        public async Task<ResponseUser<User>?> DeleteUserAsync(string email)
        {
            var user = await appDBContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                _logger.LogWarning("User with email {Email} not found.", email);
                return CreateResponseUser(null, 404, "User not found.", false);
            }
            _logger.LogInformation("Deleting user: {Name}, Email: {Email}", user.Name, user.Email);
            appDBContext.Users.Remove(user);
            await appDBContext.SaveChangesAsync();

            return CreateResponseUser(user, 200, "User deleted successfully.", true);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var users = await appDBContext.Users.ToListAsync();
            _logger.LogInformation("Retrieved {Count} users from the database.", users.Count);
            return users;

            throw new NotImplementedException();
        }

        public async Task<ResponseUser<User>?> GetUserAsync(string email)
        {
            var user = await appDBContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                _logger.LogWarning("User with email {Email} not found.", email);
                return CreateResponseUser(null, 404, "User not found.", false);
            }
            _logger.LogInformation("Retrieved user: {Name}, Email: {Email}", user.Name, user.Email);
            return CreateResponseUser(user, 200, "User retrieved successfully.", true);
        }

        public async Task<ResponseUser<User>?> UpdateUserAsync(string email, RequestUser userRequest)
        {
            var user = await appDBContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                _logger.LogWarning("User with email {Email} not found.", email);
                return CreateResponseUser(null, 404, "User not found.", false);
            }
            _logger.LogInformation("Updating user: {Name}, Email: {Email}", user.Name, user.Email);
            user.Name = userRequest.Name;
            user.Email = userRequest.Email;

            appDBContext.Users.Update(user);
            await appDBContext.SaveChangesAsync();

            return CreateResponseUser(user, 200, "User updated successfully.", true);
        }
    }
}
