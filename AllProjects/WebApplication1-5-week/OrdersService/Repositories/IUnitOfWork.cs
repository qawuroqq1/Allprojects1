/// <summary>
/// </summary>
namespace OrdersService.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderRepository Orders { get; }

        Task<int> CompleteAsync();
    }
}