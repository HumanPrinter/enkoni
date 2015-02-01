using System;

namespace Enkoni.Framework.Entities {
  /// <summary>This class contains some all-purpose extension-methods.</summary>
  internal static class Extensions {
    #region IEntity extensions
    /// <summary>Returns a copy or clone of the instance.</summary>
    /// <typeparam name="TEntity">The actual type of the instance that will be copied.</typeparam>
    /// <param name="instance">The instance that will be copied or cloned.</param>
    /// <returns>A copy of the instance.</returns>
    internal static TEntity CreateCopyOrClone<TEntity>(this TEntity instance) where TEntity : class, IEntity<TEntity>, new() {
      if(instance == null) {
        return null;
      }
      else {
        ICloneable entityAsCloneable = instance as ICloneable;
        TEntity copyOfEntity;
        if(entityAsCloneable != null) {
          copyOfEntity = entityAsCloneable.Clone() as TEntity;
        }
        else {
          copyOfEntity = new TEntity();
          copyOfEntity.CopyFrom(instance);
        }

        return copyOfEntity;
      }
    }
    #endregion
  }
}
