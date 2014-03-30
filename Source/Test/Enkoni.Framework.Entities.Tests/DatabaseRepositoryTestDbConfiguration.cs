using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Enkoni.Framework.Entities.Tests {
  /// <summary>A helper class to support the testcases.</summary>
  public class DatabaseRepositoryTestDbConfiguration : DbConfiguration {
    /// <summary>Initializes a new instance of the <see cref="DatabaseRepositoryTestDbConfiguration"/> class.</summary>
    public DatabaseRepositoryTestDbConfiguration() {
      this.SetDefaultConnectionFactory(new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0"));
    }
  }
}
