using Databasing.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Databasing;

public class DatabaseContext: DbContext
{
	public DbSet<Product> products { get; set; }
	public DbSet<Client> clients { get; set; }

	public DatabaseContext(DbContextOptions<DatabaseContext> opts)
	: base(opts)
	{}
}