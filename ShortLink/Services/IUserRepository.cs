namespace ShortLink.Services
{
    public interface IUserRepository
    {
        Task<UserEntity> CreateUser();
    }
}