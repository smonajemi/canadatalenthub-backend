using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public interface IUserService
{
    Task<User> GetUserByEmailAsync(string email);
    Task<FreelancerUser> CreateFreelancerUserAsync(FreelancerUser freelancer);
}

public class UserService : IUserService
{
    private readonly List<FreelancerUser> _users = new();
    public async Task<User> GetUserByEmailAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email cannot be null or empty.", nameof(email));
        }

        var user = await Task.Run(() => _users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)));

        if (user == null)
        {
            throw new UserNotFoundException($"User with email '{email}' not found.");
        }

        return user;
    }

    // User creation
    public async Task<FreelancerUser> CreateFreelancerUserAsync(FreelancerUser freelancer)
    {
        if (freelancer == null)
        {
            throw new ArgumentNullException(nameof(freelancer), "Freelancer data cannot be null.");
        }

        if (string.IsNullOrWhiteSpace(freelancer.Email))
        {
            throw new ArgumentException("Freelancer email cannot be empty.", nameof(freelancer.Email));
        }

        // Check if user exists
        if (_users.Any(u => u.Email.Equals(freelancer.Email, StringComparison.OrdinalIgnoreCase)))
        {
            throw new ArgumentException($"User with email '{freelancer.Email}' already exists.");
        }

     
        freelancer.Id = Guid.NewGuid();  
        await Task.Run(() => _users.Add(freelancer));

        return freelancer;
    }
}

// Custom exception
public class UserNotFoundException : Exception
{    public UserNotFoundException(string message) : base(message) { }
}
