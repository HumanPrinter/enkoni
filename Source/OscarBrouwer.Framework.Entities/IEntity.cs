//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="IEntity.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Defines the basic entity API.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

namespace OscarBrouwer.Framework.Entities {
  /// <summary>Defines the basic signature of an entity-type.</summary>
  /// <typeparam name="T">The actual entity type.</typeparam>
  public interface IEntity<T> {
    /// <summary>Gets or sets the record-ID of the entity.</summary>
    int RecordId { get; set; }

    /// <summary>Copies the values from <paramref name="source"/> into this instance.</summary>
    /// <param name="source">The entity that contains the desired values.</param>
    void CopyFrom(T source);
  }
}
