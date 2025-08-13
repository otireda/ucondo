using Ardalis.SharedKernel;
using Ardalis.Specification.EntityFrameworkCore;

namespace Ucondo.Infrastructure.Data;

public class EfRepository<T>(UcondoDbContext dbContext)
	: RepositoryBase<T>(dbContext), IReadRepository<T>, IRepository<T>
	where T : class, IAggregateRoot;
	