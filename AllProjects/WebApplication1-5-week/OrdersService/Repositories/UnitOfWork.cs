namespace OrdersService.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext context;
    public IOrderRepository Orders { get; private set; }

    public UnitOfWork(AppDbContext context)
    {
        this.context = context;
        Orders = new OrderRepository(this.context);
    }

    public async Task<int> CompleteAsync()
    {
        return await this.context.SaveChangesAsync().ConfigureAwait(false);
    }

    public void Dispose()
    {
        this.context.Dispose();
    }
}