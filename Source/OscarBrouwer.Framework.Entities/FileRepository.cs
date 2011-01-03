//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="FileRepository.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Holds the default implementation of a repository that uses a file as datasource.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System;
using System.Text.RegularExpressions;

namespace OscarBrouwer.Framework.Entities {
  /// <summary>This abstract class extends the abstract <see cref="Repository{T}"/> class and implements some of the 
  /// functionality using basic file I/O. This implementation can be used a base for any fileformat-specific 
  /// filerepositories.</summary>
  /// <typeparam name="TEntity">The type of the entity that is handled by this repository.</typeparam>
  public abstract class FileRepository<TEntity> : Repository<TEntity>
    where TEntity : class, new() {
    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="FileRepository{TEntity}"/> class.</summary>
    protected FileRepository()
      : base() {
    }
    #endregion

    #region Repository<T> overrides
    /// <summary>Creates a new entity of type <typeparamref name="TEntity"/>. This is done by calling the default
    /// constructor of <typeparamref name="TEntity"/>.</summary>
    /// <returns>The created entity.</returns>
    protected override TEntity CreateEntityCore() {
      TEntity entity = new TEntity();
      return entity;
    }

    /// <summary>Creates an expression that can be used to perform a 'Like' operation.</summary>
    /// <returns>The created expression.</returns>
    protected override Func<string, string, bool> CreateLikeExpressionCore() {
      Func<string, string, bool> expression = (field, pattern) => Regex.IsMatch(field, pattern, RegexOptions.None);
      return expression;
    }
    #endregion
  }
}
