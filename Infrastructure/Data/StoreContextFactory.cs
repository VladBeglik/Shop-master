using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Data
{
	public class StoreContextFactory : IDesignTimeDbContextFactory<StoreContext>
	{
		public StoreContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<StoreContext>();
			optionsBuilder.UseSqlServer("(localdb)\\mssqllocaldb;Database=aspnetComShop;Trusted_Connection=False;MultipleActiveResultSets=true");

			return new StoreContext(optionsBuilder.Options);
		}
    }
}