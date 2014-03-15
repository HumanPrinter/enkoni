using System.Data.Entity;

namespace Enkoni.Framework.Entities {
  /// <summary>This interface is used as a marker by the RepositoryFactory.</summary>
  public interface IDatabaseRepository {
    /// <summary>Replaces the current DbContext with the specified one. The current DbContext is first disposed.</summary>
    /// <param name="dbContext">The new DbContext that must be used.</param>
    void ReloadObjectContext(DbContext dbContext);
  }
}
