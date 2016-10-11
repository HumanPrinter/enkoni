using System;

namespace Enkoni.Framework {
  /// <summary>This class contains extension-methods for the <see cref="ICloneable"/> type.</summary>
  public static class Cloneable {
    /// <summary>Returns a strong-typed clone of the instance.</summary>
    /// <typeparam name="T">The actual type of the instance that will be cloned.</typeparam>
    /// <param name="instance">The instance on which the clone-method will be invoked.</param>
    /// <returns>A typed clone of the instance.</returns>
    public static T Clone<T>(this T instance) where T : ICloneable {
      if(instance == null) {
        return default(T);
      }
      else {
        return (T)instance.Clone();
      }
    }
  }
}
