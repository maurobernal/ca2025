using ca.Domain.Entities;

namespace ca.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    DbSet<People> Peoples { get; }

    DbSet<Country> Countrys { get; }

    DbSet<Hobbie> Hobbies { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
