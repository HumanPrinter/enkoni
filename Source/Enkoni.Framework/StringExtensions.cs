using System.Globalization;
using System.Linq;

namespace Enkoni.Framework {
  /// <summary>This class contains extension-methods for the <see langword="string"/> type.</summary>
  public static partial class StringExtensions {
    /// <summary>Capitalizes the first letter of each word assuming that words are separated by a single space.</summary>
    /// <param name="source">The string that must be capitalized.</param>
    /// <returns>The capitalized string. If <paramref name="source"/> is empty, <paramref name="source"/> is returned without
    /// modifications.</returns>
    public static string Capitalize(this string source) {
      return source.Capitalize(null);
    }

    /// <summary>Capitalizes the first letter of each word assuming that words are separated by a single space.</summary>
    /// <param name="source">The string that must be capitalized.</param>
    /// <param name="keepExistingCapitals">Indicates if any capitals that are already in the string must be preserved or must be lowered.</param>
    /// <returns>The capitalized string. If <paramref name="source"/> is empty, <paramref name="source"/> is returned without
    /// modifications.</returns>
    public static string Capitalize(this string source, bool keepExistingCapitals) {
      return source.Capitalize(keepExistingCapitals, null);
    }

    /// <summary>Capitalizes the first letter of each word assuming that words are separated by a single space.</summary>
    /// <param name="source">The string that must be capitalized.</param>
    /// <param name="culture">A <see cref="CultureInfo"/> object that supplies culture-specific casing rules.</param>
    /// <returns>The capitalized string. If <paramref name="source"/> is empty, <paramref name="source"/> is returned without
    /// modifications.</returns>
    public static string Capitalize(this string source, CultureInfo culture) {
      return source.Capitalize(false, culture);
    }

    /// <summary>Capitalizes the first letter of a sentence assuming that words are separated by a single space.</summary>
    /// <param name="source">The sentence that must be capitalized.</param>
    /// <returns>The capitalized sentence. If <paramref name="source"/> is empty, <paramref name="source"/> is returned without
    /// modifications.</returns>
    public static string CapitalizeSentence(this string source) {
      return source.CapitalizeSentence(null);
    }

    /// <summary>Capitalizes the first letter of a sentence assuming that words are separated by a single space.</summary>
    /// <param name="source">The sentence that must be capitalized.</param>
    /// <param name="keepExistingCapitals">Indicates if any capitals that are already in the string must be preserved or must be lowered.</param>
    /// <returns>The capitalized sentence. If <paramref name="source"/> is empty, <paramref name="source"/> is returned without
    /// modifications.</returns>
    public static string CapitalizeSentence(this string source, bool keepExistingCapitals) {
      return source.CapitalizeSentence(keepExistingCapitals, null);
    }

    /// <summary>Capitalizes the first letter of a sentence assuming that words are separated by a single space.</summary>
    /// <param name="source">The sentence that must be capitalized.</param>
    /// <param name="culture">A <see cref="CultureInfo"/> object that supplies culture-specific casing rules.</param>
    /// <returns>The capitalized sentence. If <paramref name="source"/> is empty, <paramref name="source"/> is returned without
    /// modifications.</returns>
    public static string CapitalizeSentence(this string source, CultureInfo culture) {
      return source.CapitalizeSentence(false, culture);
    }

    /// <summary>Truncates a string value to a maximum length. If the length of the string value is already less or equal to <paramref name="maxLength"/> the
    /// original string value is returned.</summary>
    /// <param name="source">The string value that must be truncated.</param>
    /// <param name="maxLength">The maximum length of the returned string.</param>
    /// <returns>The string value truncated to the specified length or the original string if the length is already less then or equal to the maximum length.</returns>
    public static string Truncate(this string source, int maxLength) {
      Guard.ArgumentIsGreaterOrEqualThan(0, maxLength, nameof(maxLength), "The maximum length cannot be negative");

      if(string.IsNullOrEmpty(source)) {
        return source;
      }
      else {
        return source.Length <= maxLength ? source : source.Substring(0, maxLength);
      }
    }
  }
}
