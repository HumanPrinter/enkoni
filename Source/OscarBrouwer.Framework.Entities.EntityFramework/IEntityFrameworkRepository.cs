//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="IEntityFrameworkRepository.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Defines the basic IEntityFrameworkRepository API.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System.Data.Entity;

namespace OscarBrouwer.Framework.Entities.EntityFramework {
  /// <summary>This interface is used as a marker by the RepositoryFactory.</summary>
  public interface IEntityFrameworkRepository {
    /// <summary>Replaces the current DbContext with the specified one. The current DbContext is first disposed.
    /// </summary>
    /// <param name="dbContext">The new DbContext that must be used.</param>
    void ReloadObjectContext(DbContext dbContext);
  }
}
