using Inkukan.Domain.Entities;
using Inkukan.Domain.Exceptions;
using Inkukan.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Inkukan.Infrastructure.Repositories;

public class UserRepository(UserManager<User> userManager) : IUserRepository
{
    public async Task<User> CreateAsync(User user, string password, CancellationToken cancellationToken)
    {
        IdentityResult result = await userManager.CreateAsync(user, password);

        if (!result.Succeeded)
            throw new CustomException("An error occured while saving the user");

        await userManager.AddToRoleAsync((await userManager.FindByEmailAsync(user.Email ?? string.Empty))!, "User");

        return (await userManager.FindByEmailAsync(user.Email!))!;
    }

    public async Task<User?> FindByCredentials(string login, string password, CancellationToken cancellationToken)
    {
        User? foundUser = await GetByUsernameAsync(login, cancellationToken) ?? await GetByEmailAsync(login, cancellationToken);
        if (foundUser == null)
            return null;

        bool checkIdentityResult = await userManager.CheckPasswordAsync(foundUser, password);

        return checkIdentityResult ? foundUser : null;
    }


    public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken) 
        => await userManager.FindByNameAsync(username);

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken) 
        => await userManager.FindByEmailAsync(email);
}
