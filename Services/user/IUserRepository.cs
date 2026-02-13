using System.Threading.Tasks;

namespace MyApi.Repositories;

public interface IUserRepository
{
    Task<UserModel?> GetByUsernameAsync(string username);
}
