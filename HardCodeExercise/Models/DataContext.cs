using System;
using Microsoft.EntityFrameworkCore;

namespace HardCodeExercise.Models
{
	public class DataContext : DbContext
	{
		public static readonly IConfiguration Config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
		public static readonly DbContextOptionsBuilder OptionsBuilder = new DbContextOptionsBuilder<DataContext>()
																.UseNpgsql(Config.GetConnectionString("Default"))
																.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
		public DbSet<Category> Categories { get; set; }
		public DbSet<Field> Fields { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Type> Types { get; set; }

		public DataContext()
        {

        }
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{
			Database.EnsureCreated();
		}
		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			if (!options.IsConfigured)
			{
				options.UseNpgsql(Config.GetConnectionString("Default"));
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}

