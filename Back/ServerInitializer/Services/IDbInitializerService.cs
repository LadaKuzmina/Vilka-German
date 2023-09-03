namespace ServerInitializer.Services;

public interface IDbInitializerService
{
    Task CreateDb();
    Task AddHeadingsOne();
    Task AddHeadingsTwo();
}