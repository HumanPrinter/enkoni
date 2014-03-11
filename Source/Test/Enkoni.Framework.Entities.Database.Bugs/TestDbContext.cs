using System.Data.Entity;

namespace Enkoni.Framework.Entities.Database.Bugs {
  /// <summary>A helper class to support the testcases.</summary>
  public class TestDbContext : DbContext {
    /// <summary>Initializes a new instance of the <see cref="TestDbContext"/> class.</summary>
    public TestDbContext() {
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
