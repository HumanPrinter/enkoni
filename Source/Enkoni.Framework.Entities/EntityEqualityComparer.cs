using Enkoni.Framework.Collections;

namespace Enkoni.Framework.Entities {
  /// <summary>This class can be used to compare two instances of <see cref="IEntity{T}"/> for equality. It uses the
  /// <see cref="IEntity{T}.RecordId"/> property to compare the two instances.</summary>
  /// <typeparam name="TEntity">The type of entity that must be compared.</typeparam>
  public class EntityEqualityComparer<TEntity> : LambdaEqualityComparer<TEntity, int> where TEntity : IEntity<TEntity> {
    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="EntityEqualityComparer{TEntity}"/> class.</summary>
    public EntityEqualityComparer()
      : base(t => t.RecordId) {
    }
    #endregion
  }
}
