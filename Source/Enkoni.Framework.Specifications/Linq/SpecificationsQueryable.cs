using System.Linq;

namespace Enkoni.Framework.Linq {
  /// <summary>This class contains some all-purpose extension-methods.</summary>
  public static class SpecificationsQueryable {
    #region IQueryable<T> extension methods

    /// <summary>Sorts the sequence according to the sort specifications.</summary>
    /// <typeparam name="T">The type of object that must be sorted.</typeparam>
    /// <param name="source">The sequence that must be sorted.</param>
    /// <param name="sortSpecifications">The specifications for the sorting.</param>
    /// <returns>The sorted sequence.</returns>
    public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, SortSpecifications<T> sortSpecifications) {
      Guard.ArgumentIsNotNull(source, nameof(source), "The IQueryable instance is mandatory.");

      if(sortSpecifications == null) {
        return source;
      }
      else {
        return sortSpecifications.Sort(source);
      }
    }

    #endregion
  }
}
