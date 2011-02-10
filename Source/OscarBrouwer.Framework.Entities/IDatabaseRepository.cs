//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="IDatabaseRepository.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2011. All rights reserved.
// </copyright>
// <summary>
//     Defines the basic IDatabaseRepository API.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System.Data.Entity;

namespace Enkoni.Framework.Entities {
  /// <summary>This interface is used as a marker by the RepositoryFactory.</summary>
  public interface IDatabaseRepository {
    /// <summary>Replaces the current DbContext with the specified one. The current DbContext is first disposed.</summary>
    /// <param name="dbContext">The new DbContext that must be used.</param>
    void ReloadObjectContext(DbContext dbContext);
  }
}
