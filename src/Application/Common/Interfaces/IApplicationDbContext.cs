using ca.Domain.Entities;

namespace ca.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<People> Peoples { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
