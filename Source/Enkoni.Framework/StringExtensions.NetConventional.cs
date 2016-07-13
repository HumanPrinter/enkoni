using System.Globalization;
using System.Linq;

namespace Enkoni.Framework {
  /// <summary>This class contains extension-methods for the <see langword="string"/> type.</summary>
  public static partial class StringExtensions {
    /// <summary>Capitalizes the first letter of each word assuming that words are separated by a single space.</summary>
    /// <param name="source">The string that must be capitalized.</param>
    /// <param name="keepExistingCapitals">Indicates if any capitals that are already in the string must be preserved or must be lowered.</param>
    /// <param name="culture">A <see cref="CultureInfo"/> object that supplies culture-specific casing rules.</param>
    /// <returns>The capitalized string. If <paramref name="source"/> is empty, <paramref name="source"/> is returned without
    /// modifications.</returns>
    public static string Capitalize(this string source, bool keepExistingCapitals, CultureInfo culture) {
      Guard.ArgumentIsNotNull(source, nameof(source));

      if(string.IsNullOrEmpty(source.Trim())) {
        return source;
      }

      if(culture == null) {
        culture = CultureInfo.CurrentCulture;
      }

      string[] words = source.Split(' ');
      for(int wordIndex = 0; wordIndex < words.Length; ++wordIndex) {
        if(string.IsNullOrEmpty(words[wordIndex])) {
          continue;
        }

        words[wordIndex] = new string(new char[] { words[wordIndex][0] }).ToUpper(culture) /* Capitalize the first character */
            + (keepExistingCapitals
              /* Keep the remaining characters as they are */
              ? words[wordIndex].Substring(1)
              /* Lower the remaining characters */
              : words[wordIndex].Substring(1).ToLower(culture));
      }

      return string.Join(" ", words);
    }

    /// <summary>Capitalizes the first letter of a sentence assuming that words are separated by a single space.</summary>
    /// <param name="source">The sentence that must be capitalized.</param>
    /// <param name="keepExistingCapitals">Indicates if any capitals that are already in the string must be preserved or must be lowered.</param>
    /// <param name="culture">A <see cref="CultureInfo"/> object that supplies culture-specific casing rules.</param>
    /// <returns>The capitalized sentence. If <paramref name="source"/> is empty, <paramref name="source"/> is returned without
    /// modifications.</returns>
    public static string CapitalizeSentence(this string source, bool keepExistingCapitals, CultureInfo culture) {
      Guard.ArgumentIsNotNull(source, nameof(source));

      if (string.IsNullOrEmpty(source.Trim())) {
        return source;
      }

      if(culture == null) {
        culture = CultureInfo.CurrentCulture;
      }

      string[] words = source.Split(' ');
      bool firstWordFound = false;
      for(int wordIndex = 0; wordIndex < words.Length; ++wordIndex) {
        if(string.IsNullOrEmpty(words[wordIndex])) {
          continue;
        }

        if(string.IsNullOrEmpty(words[wordIndex].Trim())) {
          continue;
        }

        words[wordIndex] =
          firstWordFound
          ? (keepExistingCapitals
            /* Keep the remaining words as they are */
            ? words[wordIndex]
            /* Lower the remaining characters of the remaining words*/
            : words[wordIndex].ToLower(culture))
          /* Capitalize the first character of the first word */
          : new string(new char[] { words[wordIndex][0] }).ToUpper(culture)
            + (keepExistingCapitals
              /* Keep the remaining characters of the first word as they are */
              ? words[wordIndex].Substring(1)
              /* Lower the remaining characters of the first word*/
              : words[wordIndex].Substring(1).ToLower(culture));

        firstWordFound = true;
      }

      return string.Join(" ", words);
    }
  }
}
