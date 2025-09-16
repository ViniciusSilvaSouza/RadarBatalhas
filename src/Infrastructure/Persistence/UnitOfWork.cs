using Domain.Compartilhado.Contracts;

namespace Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly RadarDbContext _db;
    public UnitOfWork(RadarDbContext db) => _db = db;
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => _db.SaveChangesAsync(cancellationToken);
}
