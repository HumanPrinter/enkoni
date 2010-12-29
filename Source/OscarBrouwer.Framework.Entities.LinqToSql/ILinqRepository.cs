//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="ILinqRepository.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Defines the basic Linq repository API.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System.Data.Linq;

namespace OscarBrouwer.Framework.Entities.LinqToSql {
  /// <summary>This interface is used as a marker by the RepositoryFactory.</summary>
  public interface ILinqRepository {
    /// <summary>Replaces the current DataContext with the specified one. The current DataContext is first disposed.
    /// </summary>
    /// <param name="dataContext">The new DataContext that must be used.</param>
    void ReloadDataContext(DataContext dataContext);
  }
}
