using System;

namespace Enkoni.Framework {
  /// <summary>Defines a marker attribute that is used to avoid incorrect CA1062 warnings.</summary>
  [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
  internal sealed class ValidatedNotNullAttribute : Attribute {
  }
}
