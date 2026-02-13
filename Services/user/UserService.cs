using MyApi.Data;

public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public List<UserModel> GetAll()
    {
        return _context.Users.ToList();
    }

    public UserModel GetById(int id)
    {
        UserModel res = _context.Users.FirstOrDefault(u => u.Id == id);
        return res;
    }

    public UserModel Create(UserModel user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }
}