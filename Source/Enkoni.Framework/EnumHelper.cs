using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.Serialization;

namespace Enkoni.Framework {
  /// <summary>This class contains helper methods that perform common tasks for enum values.</summary>
  public static partial class EnumHelper {
    #region ToString methods

    /// <summary>Returns a string value representing the specified enum value. If the enum value has the <see cref="LocalizedDescriptionAttribute"/>
    /// applied, its parameters are used to generate the string value. If those parameters do not result in a valid string value or the attribute is
    /// not applied, the enum value is tested for the <see cref="DescriptionAttribute"/>. If that attribute is applied, the
    /// <see cref="DescriptionAttribute.Description"/> field is used as the string value. <br/>
    /// If the <see cref="DescriptionAttribute"/> is not applied or does not result in a valid string value, the enum value is tested for the
    /// <see cref="EnumMemberAttribute"/>. If that attribute is applied, the <see cref="EnumMemberAttribute.Value"/> field is used as the string
    /// value.<br/>
    /// If none of the attributes are applied or result in a valid string value, the default <see cref="Enum.ToString()"/> method is used.
    /// </summary>
    /// <param name="enumValue">The enum value that must be converted into a string value.</param>
    /// <returns>The string representation of the enum value.</returns>
    public static string ToString(Enum enumValue) {
      return EnumHelper.ToString(enumValue, CultureInfo.CurrentUICulture);
    }

    /// <summary>Returns a string value representing the specified enum value. If the enum value has the <see cref="LocalizedDescriptionAttribute"/>
    /// applied, its parameters are used to generate the string value. If those parameters do not result in a valid string value or the attribute is
    /// not applied, the enum value is tested for the <see cref="DescriptionAttribute"/>. If that attribute is applied, the
    /// <see cref="DescriptionAttribute.Description"/> field is used as the string value. <br/>
    /// If the <see cref="DescriptionAttribute"/> is not applied or does not result in a valid string value, the enum value is tested for the
    /// <see cref="EnumMemberAttribute"/>. If that attribute is applied, the <see cref="EnumMemberAttribute.Value"/> field is used as the string
    /// value.<br/>
    /// If none of the attributes are applied or result in a valid string value, the default <see cref="Enum.ToString()"/> method is used.</summary>
    /// <param name="enumValue">The enum value that must be converted into a string value.</param>
    /// <param name="culture">The <see cref="CultureInfo"/> object that represents the culture for which the resource is localized. Note that if the
    /// resource is not localized for this culture, the lookup will fall back using the culture's <see cref="CultureInfo.Parent"/> property, stopping
    /// after looking in the neutral culture.  If this value is <see langword="null"/>, the <see cref="CultureInfo"/> is obtained using the culture's
    /// <see cref="CultureInfo.CurrentUICulture"/> property.</param>
    /// <returns>The string representation of the enum value.</returns>
    public static string ToString(Enum enumValue, CultureInfo culture) {
      return EnumHelper.ToString(enumValue, null, culture);
    }

    /// <summary>Returns a string value representing the specified enum value. If the enum value has the <see cref="LocalizedDescriptionAttribute"/>
    /// applied, its parameters are used to generate the string value. If those parameters do not result in a valid string value or the attribute is
    /// not applied, the enum value is tested for the <see cref="DescriptionAttribute"/>. If that attribute is applied, the
    /// <see cref="DescriptionAttribute.Description"/> field is used as the string value. <br/>
    /// If the <see cref="DescriptionAttribute"/> is not applied or does not result in a valid string value, the enum value is tested for the
    /// <see cref="EnumMemberAttribute"/>. If that attribute is applied, the <see cref="EnumMemberAttribute.Value"/> field is used as the string
    /// value.<br/>
    /// If none of the attributes are applied or result in a valid string value, the default <see cref="Enum.ToString()"/> method is used.</summary>
    /// <param name="enumValue">The enum value that must be converted into a string value.</param>
    /// <param name="resources">The resource manager that must be used to retrieve the localized string.</param>
    /// <returns>The string representation of the enum value.</returns>
    public static string ToString(Enum enumValue, ResourceManager resources) {
      return EnumHelper.ToString(enumValue, resources, CultureInfo.CurrentUICulture);
    }

    #endregion
  }
}
