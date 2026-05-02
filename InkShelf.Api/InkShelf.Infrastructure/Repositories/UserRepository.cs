using InkShelf.Domain.Entities;
using InkShelf.Domain.Exceptions;
using InkShelf.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace InkShelf.Infrastructure.Repositories
{
    public class UserRepository(UserManager<User> userManager, RoleManager<Role> roleManager) : IUserRepository
    {
        public async Task<User> CreateAsync(User user, string password, CancellationToken cancellationToken = default)
        {
            IdentityResult result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                throw new CustomException("An error occured while saving the user");

            await userManager.AddToRoleAsync((await userManager.FindByEmailAsync(user.Email ?? string.Empty))!, "User");

            return (await userManager.FindByEmailAsync(user.Email!))!;
        }

        public async Task<User?> FindByCredentials(string login, string password, CancellationToken token = default)
        {
            User? foundUser = await GetByUsernameAsync(login) ?? await GetByEmailAsync(login);
            if (foundUser == null)
                return null;

            bool checkIdentityResult = await userManager.CheckPasswordAsync(foundUser, password);

            return checkIdentityResult ? foundUser : null;
        }


        public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            return await userManager.FindByNameAsync(username);
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await userManager.FindByEmailAsync(email);
        }
    }
}
