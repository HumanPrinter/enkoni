using System.Data.Entity;

namespace Enkoni.Framework.Entities {
  /// <summary>This interface is used as a marker by the RepositoryFactory.</summary>
  public interface IDatabaseRepository {
    /// <summary>Replaces the current <see cref="DbContext"/> with the specified one. The current <see cref="DbContext"/> is first disposed.</summary>
    /// <param name="dbContext">The new <see cref="DbContext"/> that must be used.</param>
    void ReloadObjectContext(DbContext dbContext);
  }
}
