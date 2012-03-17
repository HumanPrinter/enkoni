// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumHelper.cs" company="Oscar Brouwer">
//   Copyright (c) Oscar Brouwer 2011. All rights reserved.
// </copyright>
// <summary>
//   Contains numerous helper methods for enum values.
// </summary>
// -------------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Enkoni.Framework {
  /// <summary>This class contains helper methods that perform common tasks for enum values.</summary>
  public static class EnumHelper {
    #region ToString methods
    /// <summary>Returns a string value representing the specified enum value. If the enum value has the 
    /// <see cref="LocalizedDescriptionAttribute"/> applied, its parameters are used to generate the string value. If those
    /// parameters do not result in a valid string value or the attribute is not applied, the enum value is tested for the 
    /// <see cref="DescriptionAttribute"/>. If that attribute is applied, the <see cref="DescriptionAttribute.Description"/>
    /// field is used as the string value. <br/>
    /// If none of the attributes are applied or result in a valid string value, the default <see cref="Enum.ToString()"/>
    /// method is used.</summary>
    /// <param name="enumValue">The enum value that must be converted into a string value.</param>
    /// <returns>The string representation of the enum value.</returns>
    public static string ToString(Enum enumValue) {
      return EnumHelper.ToString(enumValue, CultureInfo.CurrentUICulture);
    }

    /// <summary>Returns a string value representing the specified enum value. If the enum value has the 
    /// <see cref="LocalizedDescriptionAttribute"/> applied, its parameters are used to generate the string value. If those
    /// parameters do not result in a valid string value or the attribute is not applied, the enum value is tested for the 
    /// <see cref="DescriptionAttribute"/>. If that attribute is applied, the <see cref="DescriptionAttribute.Description"/>
    /// field is used as the string value. <br/>
    /// If none of the attributes are applied or result in a valid string value, the default <see cref="Enum.ToString()"/>
    /// method is used.</summary>
    /// <param name="enumValue">The enum value that must be converted into a string value.</param>
    /// <param name="culture">The <see cref="CultureInfo"/> object that represents the culture for which the resource is 
    /// localized. Note that if the resource is not localized for this culture, the lookup will fall back using the culture's 
    /// <see cref="CultureInfo.Parent"/> property, stopping after looking in the neutral culture.  If this value is 
    /// <see langword="null"/>, the <see cref="CultureInfo"/> is obtained using the culture's 
    /// <see cref="CultureInfo.CurrentUICulture"/> property.</param>
    /// <returns>The string representation of the enum value.</returns>
    public static string ToString(Enum enumValue, CultureInfo culture) {
      return EnumHelper.ToString(enumValue, null, culture);
    }

    /// <summary>Returns a string value representing the specified enum value. If the enum value has the 
    /// <see cref="LocalizedDescriptionAttribute"/> applied, its parameters are used to generate the string value. If those
    /// parameters do not result in a valid string value or the attribute is not applied, the enum value is tested for the 
    /// <see cref="DescriptionAttribute"/>. If that attribute is applied, the <see cref="DescriptionAttribute.Description"/>
    /// field is used as the string value. <br/>
    /// If none of the attributes are applied or result in a valid string value, the default <see cref="Enum.ToString()"/>
    /// method is used.</summary>
    /// <param name="enumValue">The enum value that must be converted into a string value.</param>
    /// <param name="resources">The resource manager that must be used to retrieve the localized string.</param>
    /// <returns>The string representation of the enum value.</returns>
    public static string ToString(Enum enumValue, ResourceManager resources) {
      return EnumHelper.ToString(enumValue, resources, CultureInfo.CurrentUICulture);
    }

    /// <summary>Returns a string value representing the specified enum value. If the enum value has the 
    /// <see cref="LocalizedDescriptionAttribute"/> applied, its parameters are used to generate the string value. If those
    /// parameters do not result in a valid string value or the attribute is not applied, the enum value is tested for the 
    /// <see cref="DescriptionAttribute"/>. If that attribute is applied, the <see cref="DescriptionAttribute.Description"/>
    /// field is used as the string value. <br/>
    /// If none of the attributes are applied or result in a valid string value, the default <see cref="Enum.ToString()"/>
    /// method is used.</summary>
    /// <param name="enumValue">The enum value that must be converted into a string value.</param>
    /// <param name="resources">The resource manager that must be used to retrieve the localized string.</param>
    /// <param name="culture">The <see cref="CultureInfo"/> object that represents the culture for which the resource is 
    /// localized. Note that if the resource is not localized for this culture, the lookup will fall back using the culture's 
    /// <see cref="CultureInfo.Parent"/> property, stopping after looking in the neutral culture.  If this value is 
    /// <see langword="null"/>, the <see cref="CultureInfo"/> is obtained using the culture's 
    /// <see cref="CultureInfo.CurrentUICulture"/> property.</param>
    /// <returns>The string representation of the enum value.</returns>
    public static string ToString(Enum enumValue, ResourceManager resources, CultureInfo culture) {
      if(enumValue == null) {
        throw new ArgumentNullException("enumValue");
      }

      Type enumType = enumValue.GetType();
      if(!enumType.IsEnum) {
        throw new ArgumentException("The parameter must be an enum.", "enumValue");
      }

      string enumAsString = enumValue.ToString();

      FieldInfo enumValueAsFieldInfo = enumType.GetField(enumValue.ToString());
      LocalizedDescriptionAttribute localizedDescription = (LocalizedDescriptionAttribute)Attribute.GetCustomAttribute(enumValueAsFieldInfo, typeof(LocalizedDescriptionAttribute));
      if(localizedDescription != null) {
        string key = localizedDescription.ResourceKey;
        if(resources == null) {
          Type resourceType = localizedDescription.ResourceType;
          resources = new ResourceManager(resourceType);
        }

        string value = resources.GetString(key, culture);
        if(value == null) {
          value = localizedDescription.DefaultDescription;
        }

        if(value != null) {
          enumAsString = value;
        }
      }
      else {
        DescriptionAttribute description = (DescriptionAttribute)Attribute.GetCustomAttribute(enumValueAsFieldInfo, typeof(DescriptionAttribute));
        if(description != null && description.Description != null) {
          enumAsString = description.Description;
        }
      }

      return enumAsString;
    }
    #endregion

    #region Flag methods
    /// <summary>Sets the specified flag in the enum value.</summary>
    /// <typeparam name="T">The type of enum that is manipulated.</typeparam>
    /// <param name="enumValue">The value that must have the flag bit set.</param>
    /// <param name="flag">The flag bit that must be set.</param>
    /// <returns>The enum value with the flag bit set.</returns>
    public static T SetFlag<T>(T enumValue, T flag) where T : struct {
      if(!typeof(T).IsEnum) {
        throw new ArgumentException("The parameter must be an enum.", "enumValue");
      }

      long newEnum =
        Convert.ToInt64(enumValue, CultureInfo.InvariantCulture) | Convert.ToInt64(flag, CultureInfo.InvariantCulture);
      return (T)Enum.ToObject(typeof(T), newEnum);
    }

    /// <summary>Unsets the specified flag in the enum value.</summary>
    /// <typeparam name="T">The type of enum that is manipulated.</typeparam>
    /// <param name="enumValue">The value that must have the flag bit removed.</param>
    /// <param name="flag">The flag bit that must be removed.</param>
    /// <returns>The enum value with the flag bit removed.</returns>
    public static T UnsetFlag<T>(T enumValue, T flag) where T : struct {
      if(!typeof(T).IsEnum) {
        throw new ArgumentException("The parameter must be an enum.", "enumValue");
      }

      long newEnum =
        Convert.ToInt64(enumValue, CultureInfo.InvariantCulture) & ~Convert.ToInt64(flag, CultureInfo.InvariantCulture);
      return (T)Enum.ToObject(typeof(T), newEnum);
    }

    /// <summary>Toggles the specified flag in the enum value.</summary>
    /// <typeparam name="T">The type of enum that is manipulated.</typeparam>
    /// <param name="enumValue">The value that must have the flag bit toggled.</param>
    /// <param name="flag">The flag bit that must be toggled.</param>
    /// <returns>The enum value with the flag bit toggled.</returns>
    public static T ToggleFlag<T>(T enumValue, T flag) where T : struct {
      if(!typeof(T).IsEnum) {
        throw new ArgumentException("The parameter must be an enum.", "enumValue");
      }

      long newEnum =
        Convert.ToInt64(enumValue, CultureInfo.InvariantCulture) ^ Convert.ToInt64(flag, CultureInfo.InvariantCulture);
      return (T)Enum.ToObject(typeof(T), newEnum);
    }
    #endregion
  }
}
