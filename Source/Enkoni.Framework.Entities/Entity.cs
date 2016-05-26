using System;

using AutoMapper;

namespace Enkoni.Framework.Entities {
  /// <summary>Provides a basic implementation of an entity-type.</summary>
  /// <typeparam name="T">The actual entity type.</typeparam>
  public abstract class Entity<T> : IEntity<T> {
    /// <summary>Initializes a new instance of the <see cref="Entity{T}"/> class.</summary>
    protected Entity() {
    }

    /// <summary>Gets or sets the record-ID of the entity.</summary>
    public virtual int RecordId { get; set; }

    /// <summary>Copies the values from <paramref name="source"/> into this instance.</summary>
    /// <param name="source">The entity that contains the desired values.</param>
    public virtual void CopyFrom(T source) {
      Guard.ArgumentIsNotNull(source, nameof(source));

      Type entityType = typeof(T);
      TypeMap existingMap = Mapper.FindTypeMapFor(entityType, entityType);
      if(existingMap != null) {
        Mapper.Map(source, this, entityType, entityType);
      }
      else {
        Mapper.DynamicMap(source, this, typeof(T), typeof(T));
      }
    }
  }
}
