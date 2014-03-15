using System.Data.Entity;

namespace Enkoni.Framework.Entities.Tests {
  /// <summary>A helper class to support the testcases.</summary>
  public class DatabaseRepositoryTestContext : DbContext {
    /// <summary>Initializes a new instance of the <see cref="DatabaseRepositoryTestContext"/> class.</summary>
    public DatabaseRepositoryTestContext() {
    }

    /// <summary>Initializes a new instance of the <see cref="DatabaseRepositoryTestContext"/> class.</summary>
    /// <param name="nameOrConnectionString">Either the database name or a connection string.</param>
    public DatabaseRepositoryTestContext(string nameOrConnectionString)
      : base(nameOrConnectionString) {
    }

    /// <summary>Gets or sets the db-set for the TestDummy-table.</summary>
    public DbSet<TestDummy> TestDummies { get; set; }

    /// <summary>This method is called when the model for a derived context has been initialized, but before the model has been locked down and used to 
    /// initialize the context.</summary>
    /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
    protected override void OnModelCreating(DbModelBuilder modelBuilder) {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Configurations.Add(new TestDummyConfiguration());
    }
  }
}
