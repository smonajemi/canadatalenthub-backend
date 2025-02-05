public interface IUserService
{
    User GetUserByEmail(string email);
}
public class UserService : IUserService
    {
    private readonly List<User> _users = new()
    {
        new User { Id = "1", Email = "sina.monajemi@me.com", FirstName = "Sina", LastName = "Monajemi" },
    };

    public User GetUserByEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email cannot be null or empty", nameof(email));
        }
        var user = _users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

        if (user == null)
        {
            throw new UserNotFoundException($"User with email '{email}' not found.");
        }

        return user;
    }
}

// Custom exception for user not found
public class UserNotFoundException : Exception
{
    public UserNotFoundException(string message) : base(message) { }
}
