using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

using Enkoni.Framework.Properties;

namespace Enkoni.Framework.Serialization {
  /// <summary>Transforms an instance of type <typeparamref name="T"/> to and from CSV data.</summary>
  /// <typeparam name="T">Type of the object that has to be serialized.</typeparam>
  public class CsvTransformer<T> : Transformer<T> where T : new() {
    #region Private constants
    /// <summary>The regex that is used to determine if a format string matches the default format string.</summary>
    private static readonly Regex DefaultFormatRegex = new Regex(@"^.*(\{0\})?.*$", RegexOptions.Compiled);

    /// <summary>The regex that is used to determine if a format string matches a special true/false format string.</summary>
    private static readonly Regex TrueFalseFormatRegex = new Regex(@"^true:(?<trueString>.*)\|false:(?<falseString>.*)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    #endregion

    #region Private instance variables
    /// <summary>The actual separator.</summary>
    private string separator;

    /// <summary>The separator wrapped in an array for performance purposes.</summary>
    private string[] separatorArray;
    #endregion

    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="CsvTransformer{T}"/> class.</summary>
    /// <exception cref="InvalidTypeParameterException">The specified type-parameter cannot be serialized using this 
    /// serializer.</exception>
    /// <exception cref="InvalidOperationException">The specified type-parameter contains illegal metadata that prevents it 
    /// from being (de)serialized.</exception>
    public CsvTransformer() {
      this.ColumnNameMappings = new Dictionary<int, string>();
      this.PropertyDelegates = new Dictionary<int, Delegate>();
      this.FormatMappings = new Dictionary<int, string>();
      this.NullStringMappings = new Dictionary<int, string>();
      this.CultureMappings = new Dictionary<int, string>();

      MemberInfo info = typeof(T);

      /* Get the CsvRecord attribute */
      CsvRecordAttribute recordAttr = Attribute.GetCustomAttribute(info, typeof(CsvRecordAttribute), false) as CsvRecordAttribute;
      if(recordAttr == null) {
        string message = "The specified type does not contain the CsvRecord-attribute. " +
          "This attribute must be applied before it can be serialized to CSV data.";
        throw new InvalidTypeParameterException(message);
      }

      /* Get some properties of the attribute */
      this.Separator = recordAttr.Separator;
      this.IgnoreHeaderOnRead = recordAttr.IgnoreHeaderOnRead;
      this.EmitHeader = recordAttr.WriteHeader;

      string defaultCultureName = recordAttr.CultureName;

      /* Get the property attributes and map them */
      Type typeOfT = typeof(T);
      PropertyInfo[] properties = typeOfT.GetProperties();

      foreach(PropertyInfo property in properties) {
        CsvColumnAttribute colAttr = Attribute.GetCustomAttribute(property, typeof(CsvColumnAttribute), false) as CsvColumnAttribute;
        if(colAttr != null) {
          if(this.ColumnNameMappings.ContainsKey(colAttr.FieldIndex)) {
            /* The field-index is already in use */
            throw new InvalidOperationException("Cannot use the same field index for more than one property.");
          }
          else {
            this.ColumnNameMappings.Add(colAttr.FieldIndex, property.Name);
            if(colAttr.FormatString != null) {
              this.FormatMappings.Add(colAttr.FieldIndex, colAttr.FormatString);
            }

            if(colAttr.NullString != null) {
              this.NullStringMappings.Add(colAttr.FieldIndex, colAttr.NullString);
            }

            if(colAttr.CultureName != null) {
              this.CultureMappings.Add(colAttr.FieldIndex, colAttr.CultureName);
            }
            else if(defaultCultureName != null) {
              this.CultureMappings.Add(colAttr.FieldIndex, defaultCultureName);
            }

            ParameterExpression objExpression = Expression.Parameter(typeof(T), "obj");
            Expression propertyExpression = Expression.Property(objExpression, property.Name);
            LambdaExpression lambdaExpression = Expression.Lambda(propertyExpression, objExpression);
            Delegate functionDelegate = lambdaExpression.Compile();
            this.PropertyDelegates.Add(colAttr.FieldIndex, functionDelegate);
          }
        }
      }

      /* Make sure the field-indexes are correct */
      if(this.ColumnNameMappings.Keys.Min() < 0) {
        throw new InvalidOperationException("The specified field index values cannot be smaller than '0'");
      }
    }
    #endregion

    #region Private delegates
    /// <summary>Defines a delegate that can be used to reference a TryParse-method.</summary>
    /// <typeparam name="TPropertyType">The type of the out-parameter of the TryParse-method.</typeparam>
    /// <param name="input">The input string.</param>
    /// <param name="output">The output value.</param>
    /// <returns><see langword="true"/> if <paramref name="input"/> was successfully parsed; otherwise, <see langword="false"/>.</returns>
    private delegate bool TryParse<TPropertyType>(string input, out TPropertyType output);

    /// <summary>Defines a delegate that can be used to reference a culture-aware TryParse-method.</summary>
    /// <typeparam name="TPropertyType">The type of the out-parameter of the TryParse-method.</typeparam>
    /// <param name="input">The input string.</param>
    /// <param name="styles">The <see cref="NumberStyles"/> that must be passed to the TryParse method.</param>
    /// <param name="formatProvider">The format provider that must be used.</param>
    /// <param name="output">The output value.</param>
    /// <returns><see langword="true"/> if <paramref name="input"/> was successfully parsed; otherwise, <see langword="false"/>.</returns>
    private delegate bool TryParseFormatted<TPropertyType>(string input, NumberStyles styles, IFormatProvider formatProvider, out TPropertyType output);
    #endregion

    #region Properties
    /// <summary>Gets the mappings of the column names. The dictionary uses the column index as key and column name as value.</summary>
    protected internal Dictionary<int, string> ColumnNameMappings { get; private set; }

    /// <summary>Gets or sets a value indicating whether a header-line must be included when serializing the objects.</summary>
    protected internal bool EmitHeader { get; set; }

    /// <summary>Gets or sets a value indicating whether the header should be ignored when reading the file.</summary>
    protected internal bool IgnoreHeaderOnRead { get; set; }

    /// <summary>Gets or sets the separator-string.</summary>
    protected internal string Separator {
      get { 
        return this.separator; 
      }

      set {
        this.separator = value;
        this.separatorArray = new string[] { this.separator };
      }
    }

    /// <summary>Gets the delegates that give access to the properties of the instances that need to be serialized and deserialized.</summary>
    protected Dictionary<int, Delegate> PropertyDelegates { get; private set; }

    /// <summary>Gets the mappings of the format strings. The dictionary uses the column index as key and format string as value.</summary>
    protected Dictionary<int, string> FormatMappings { get; private set; }

    /// <summary>Gets the mappings of the null strings. The dictionary uses the column index as key and null string as value.</summary>
    protected Dictionary<int, string> NullStringMappings { get; private set; }

    /// <summary>Gets the mappings of the cultures. The dictionary uses the column index as key and culture name as value.</summary>
    protected Dictionary<int, string> CultureMappings { get; private set; }
    #endregion

    #region Protected methods
    /// <summary>Transforms <paramref name="instance"/> into a string.</summary>
    /// <param name="instance">The instance that must be transformed.</param>
    /// <returns>The string that contains the transformed instance.</returns>
    protected override string ToStringCore(T instance) {
      StringBuilder builder = new StringBuilder();

      bool firstField = true;
      for(int columnIndex = 0; columnIndex <= this.ColumnNameMappings.Max(kvp => kvp.Key); ++columnIndex) {
        Delegate retrievePropertyFunc = this.PropertyDelegates.ContainsKey(columnIndex) ? this.PropertyDelegates[columnIndex] : null;
        object propertyValue = null;
        if(retrievePropertyFunc != null) {
          propertyValue = retrievePropertyFunc.DynamicInvoke(instance);
        }

        CultureInfo culture = null;
        if(this.CultureMappings.ContainsKey(columnIndex)) {
          culture = new CultureInfo(this.CultureMappings[columnIndex]);
        }

        string formatString = "{0}";
        if(this.ColumnNameMappings.ContainsKey(columnIndex)) {
          PropertyInfo propertyInfo = typeof(T).GetProperty(this.ColumnNameMappings[columnIndex]);
          string propertyFormat = string.Empty;
          if(this.FormatMappings.ContainsKey(columnIndex)) {
            propertyFormat = this.FormatMappings[columnIndex];
          }

          if(propertyValue == null && this.NullStringMappings.ContainsKey(columnIndex)) {
            propertyValue = string.Empty;
            formatString = this.NullStringMappings[columnIndex];
          }
          else {
            if(propertyValue == null) {
              propertyValue = string.Empty;
            }

            if(propertyInfo.PropertyType.ActualType().IsEnum) {
              formatString = CreateEnumFormatString(propertyFormat, propertyValue, culture);
            }
            else if(propertyInfo.PropertyType.ActualType() == typeof(bool)) {
              formatString = CreateBooleanFormatString(propertyFormat, string.Empty.Equals(propertyValue) ? (bool?)null : (bool)propertyValue);
            }
            else if(propertyInfo.PropertyType.ActualType() == typeof(char)) {
              formatString = CreateCharFormatString(propertyFormat, string.Empty.Equals(propertyValue) ? (char?)null : (char)propertyValue, culture);
            }
            else if(propertyInfo.PropertyType == typeof(string)) {
              formatString = CreateStringFormatString(propertyFormat);
            }
            else {
              formatString = CreateFormatString(propertyFormat);
            }
          }
        }

        if(firstField) {
          if(culture != null) {
            builder.AppendFormat(culture, formatString, propertyValue);
          }
          else {
            builder.AppendFormat(formatString, propertyValue);
          }

          firstField = false;
        }
        else {
          formatString = "{1}" + formatString;

          if(culture != null) {
            builder.AppendFormat(culture, formatString, propertyValue, this.Separator);
          }
          else {
            builder.AppendFormat(formatString, propertyValue, this.Separator);
          }
        }
      }

      return builder.ToString();
    }

    /// <summary>Transforms <paramref name="instance"/> into a byte array.</summary>
    /// <param name="instance">The instance that must be transformed.</param>
    /// <param name="encoding">The encoding that must be used to transform the instance into bytes.</param>
    /// <returns>The byte array that contains the transformed instance.</returns>
    protected override byte[] ToBytesCore(T instance, Encoding encoding) {
      string transformedString = this.ToString(instance);
      byte[] transformedBytes = encoding.GetBytes(transformedString);
      return transformedBytes;
    }

    /// <summary>Transforms <paramref name="instance"/> into a byte array.</summary>
    /// <param name="instance">The instance that must be transformed.</param>
    /// <param name="encoding">The encoding that must be used to transform the instance into bytes.</param>
    /// <param name="bytes">The byte array into which the transformed instance must be stored.</param>
    /// <param name="offset">The offset in <paramref name="bytes"/>.</param>
    /// <returns>The number of bytes that were written into <paramref name="bytes"/>.</returns>
    protected override int ToBytesCore(T instance, Encoding encoding, byte[] bytes, int offset) {
      byte[] transformedInstance = this.ToBytesCore(instance, encoding);
      if(bytes.Length - offset < transformedInstance.Length) {
        throw new ArgumentException("The byte array does not have enough space to store the transformed instance.", "bytes");
      }

      Array.Copy(transformedInstance, 0, bytes, offset, transformedInstance.Length);
      return transformedInstance.Length;
    }

    /// <summary>Transforms a string value into an instance of type <typeparamref name="T"/>.</summary>
    /// <param name="input">The string that must be transformed.</param>
    /// <returns>The transformed instance.</returns>
    protected override T FromStringCore(string input) {
      T obj = new T();
      string[] columns = input.Split(this.separatorArray, StringSplitOptions.None);
      for(int colIndex = 0; colIndex < columns.Count(); colIndex++) {
        if(this.ColumnNameMappings.ContainsKey(colIndex)) {
          string cultureName;
          string formatString;
          string nullString;
          this.FormatMappings.TryGetValue(colIndex, out formatString);
          this.CultureMappings.TryGetValue(colIndex, out cultureName);
          this.NullStringMappings.TryGetValue(colIndex, out nullString);

          PropertyInfo propertyInfo = obj.GetType().GetProperty(this.ColumnNameMappings[colIndex]);
          if(nullString != null && columns[colIndex].Equals(nullString)) {
            if(!propertyInfo.PropertyType.IsValueType || propertyInfo.PropertyType.IsNullable()) {
              propertyInfo.SetValue(obj, null, null);
            }
            else {
              throw new FormatException(string.Format(CultureInfo.CurrentUICulture, Resources.FormatExceptionInvalidTypeValue, columns[colIndex], propertyInfo.PropertyType));
            }
          }
          else {
            SetPropertyValue(propertyInfo, obj, columns[colIndex], formatString, cultureName);
          }
        }
      }

      return obj;
    }

    /// <summary>Transforms the content of a byte array into an instance of type <typeparamref name="T"/>.</summary>
    /// <param name="bytes">The byte array whose content must be transformed.</param>
    /// <param name="offset">The offset from which to start reading bytes.</param>
    /// <param name="length">The number of bytes that must be read from the array.</param>
    /// <param name="encoding">The encoding that must be used to transform the bytes.</param>
    /// <returns>The transformed instance.</returns>
    protected override T FromBytesCore(byte[] bytes, int offset, int length, Encoding encoding) {
      char[] chars = encoding.GetChars(bytes, offset, length);
      string transformedString = new string(chars);
      return this.FromString(transformedString);
    }

    /// <summary>Transforms the content of a byte array into an instance of type <typeparamref name="T"/>.</summary>
    /// <param name="bytes">The byte array whose content must be transformed.</param>
    /// <param name="encoding">The encoding that must be used to transform the bytes.</param>
    /// <returns>The transformed instance.</returns>
    protected override T FromBytesCore(byte[] bytes, Encoding encoding) {
      return this.FromBytesCore(bytes, 0, bytes.Length, encoding);
    }
    #endregion

    #region Helper methods
    /// <summary>Creates a format string given the formatting that is applied to the property.</summary>
    /// <param name="propertyFormat">The formatting as it is applied to the property.</param>
    /// <returns>The created format string.</returns>
    private static string CreateFormatString(string propertyFormat) {
      if(string.IsNullOrEmpty(propertyFormat)) {
        return "{0}";
      }

      if(!propertyFormat.Contains('{')) {
        return "{0:" + propertyFormat + "}";
      }

      return propertyFormat;
    }

    /// <summary>Creates a format string given the formatting that is applied to the property.</summary>
    /// <param name="propertyFormat">The formatting as it is applied to the property.</param>
    /// <param name="propertyValue">De actual value of the property.</param>
    /// <returns>The created format string.</returns>
    private static string CreateBooleanFormatString(string propertyFormat, bool? propertyValue) {
      if(string.IsNullOrEmpty(propertyFormat)) {
        return "{0}";
      }

      Match trueFalseMatch = TrueFalseFormatRegex.Match(propertyFormat);
      if(trueFalseMatch.Success) {
        if(propertyValue == null) {
          return string.Empty;
        }
        else if(propertyValue.Value) {
          return trueFalseMatch.Groups["trueString"].Value;
        }
        else {
          return trueFalseMatch.Groups["falseString"].Value;
        }
      }

      return propertyFormat;
    }

    /// <summary>Creates a format string given the formatting that is applied to the property.</summary>
    /// <param name="propertyFormat">The formatting as it is applied to the property.</param>
    /// <param name="propertyValue">De actual value of the property.</param>
    /// <param name="propertyCulture">The culture that must be used.</param>
    /// <returns>The created format string.</returns>
    private static string CreateEnumFormatString(string propertyFormat, object propertyValue, CultureInfo propertyCulture) {
      if(string.IsNullOrEmpty(propertyFormat)) {
        return "{0}";
      }

      if(!propertyFormat.Contains('{')) {
        if(string.Empty.Equals(propertyValue)) {
          return string.Empty;
        }

        if(propertyFormat.Equals("g", StringComparison.OrdinalIgnoreCase) || propertyFormat.Equals("x", StringComparison.OrdinalIgnoreCase)
          || propertyFormat.Equals("f", StringComparison.OrdinalIgnoreCase) || propertyFormat.Equals("d", StringComparison.OrdinalIgnoreCase)) {
          return "{0:" + propertyFormat + "}";
        }
        else {
          int enumAsInt = (int)propertyValue;
          return string.Format(propertyCulture, "{0:" + propertyFormat + "}", enumAsInt);
        }
      }

      if(!propertyFormat.Contains(':')) {
        return propertyFormat;
      }

      int indexOfOpenBracket = propertyFormat.IndexOf('{');
      int indexOfCloseBracket = propertyFormat.IndexOf('}');
      int indexOfColon = propertyFormat.IndexOf(':');
      string prefix = indexOfOpenBracket == 0 ? string.Empty : propertyFormat.Substring(0, indexOfOpenBracket);
      string postfix = indexOfCloseBracket == propertyFormat.Length - 1 ? string.Empty : propertyFormat.Substring(indexOfCloseBracket + 1);
      string subformat = propertyFormat.Substring(indexOfColon + 1, indexOfCloseBracket - indexOfColon - 1);

      if(string.Empty.Equals(propertyValue)) {
        return prefix + postfix;
      }

      if(subformat.Equals("g", StringComparison.OrdinalIgnoreCase) || subformat.Equals("x", StringComparison.OrdinalIgnoreCase)
          || subformat.Equals("f", StringComparison.OrdinalIgnoreCase) || subformat.Equals("d", StringComparison.OrdinalIgnoreCase)) {
        return prefix + "{0:" + subformat + "}" + postfix;
      }
      else {
        int enumAsInt = (int)propertyValue;
        return prefix + string.Format(propertyCulture, "{0:" + subformat + "}", enumAsInt) + postfix;
      }
    }

    /// <summary>Creates a format string given the formatting that is applied to the property.</summary>
    /// <param name="propertyFormat">The formatting as it is applied to the property.</param>
    /// <param name="propertyValue">De actual value of the property.</param>
    /// <param name="propertyCulture">The culture that must be used.</param>
    /// <returns>The created format string.</returns>
    private static string CreateCharFormatString(string propertyFormat, char? propertyValue, CultureInfo propertyCulture) {
      if(string.IsNullOrEmpty(propertyFormat)) {
        return "{0}";
      }

      if(!propertyFormat.Contains('{')) {
        if(!propertyValue.HasValue) {
          return string.Empty;
        }

        return string.Format(propertyCulture, "{0:" + propertyFormat + "}", (int)propertyValue);
      }

      if(!propertyFormat.Contains(':')) {
        return propertyFormat;
      }

      int indexOfOpenBracket = propertyFormat.IndexOf('{');
      int indexOfCloseBracket = propertyFormat.IndexOf('}');
      int indexOfColon = propertyFormat.IndexOf(':');
      string prefix = indexOfOpenBracket == 0 ? string.Empty : propertyFormat.Substring(0, indexOfOpenBracket);
      string postfix = indexOfCloseBracket == propertyFormat.Length - 1 ? string.Empty : propertyFormat.Substring(indexOfCloseBracket + 1);
      string subformat = propertyFormat.Substring(indexOfColon + 1, indexOfCloseBracket - indexOfColon - 1);

      if(!propertyValue.HasValue) {
        return prefix + postfix;
      }

      return prefix + string.Format(propertyCulture, "{0:" + subformat + "}", (int)propertyValue) + postfix;
    }

    /// <summary>Creates a format string given the formatting that is applied to the property.</summary>
    /// <param name="propertyFormat">The formatting as it is applied to the property.</param>
    /// <returns>The created format string.</returns>
    private static string CreateStringFormatString(string propertyFormat) {
      if(string.IsNullOrEmpty(propertyFormat)) {
        return "{0}";
      }

      if(!propertyFormat.Contains('{')) {
        return "{0," + propertyFormat + "}";
      }

      return propertyFormat;
    }

    /// <summary>Set a property value of an object.</summary>
    /// <param name="propertyInfo">The property that must be assigned.</param>
    /// <param name="obj">Reference to the object whose property must be assigned.</param>
    /// <param name="value">The value that must be assigned.</param>
    /// <param name="formatString">An optional format string that can be used to properly parse the value.</param>
    /// <param name="cultureName">An optional culture name that can be used to properly parse the value.</param>
    /// <exception cref="NotSupportedException">The type of the property that must be assigned is not supported.</exception>
    private static void SetPropertyValue(PropertyInfo propertyInfo, T obj, string value, string formatString, string cultureName) {
      CultureInfo culture = CultureInfo.InvariantCulture;
      if(cultureName != null) {
        culture = new CultureInfo(cultureName);
      }

      if(string.IsNullOrEmpty(value)) {
        if(propertyInfo.PropertyType == typeof(string)) {
          propertyInfo.SetValue(obj, string.Empty, null);
          return;
        }
        else if(!propertyInfo.PropertyType.IsValueType || propertyInfo.PropertyType.IsNullable()) {
          propertyInfo.SetValue(obj, null, null);
          return;
        }
        else {
          throw new FormatException(string.Format(CultureInfo.CurrentUICulture, Resources.FormatExceptionInvalidTypeValue, value, propertyInfo.PropertyType));
        }
      }

      if(propertyInfo.PropertyType.ActualType().IsEnum) {
        CsvTransformer<T>.SetEnumValue(propertyInfo, obj, value, formatString, culture);
      }
      else if(propertyInfo.PropertyType.IsValueType) {
        CsvTransformer<T>.SetValueTypePropertyValue(propertyInfo, obj, value, formatString, culture);
      }
      else if(propertyInfo.PropertyType == typeof(string)) {
        CsvTransformer<T>.SetStringValue(propertyInfo, obj, value, formatString);
      }
      else {
        throw new NotSupportedException("Unable to parse data for data-type " + propertyInfo.PropertyType);
      }
    }

    /// <summary>Set a value type property value of an object.</summary>
    /// <param name="propertyInfo">The property that must be assigned.</param>
    /// <param name="obj">Reference to the object whose property must be assigned.</param>
    /// <param name="value">The value that must be assigned.</param>
    /// <param name="formatString">An optional format string that can be used to properly parse the value.</param>
    /// <param name="culture">An optional culture that can be used to properly parse the value.</param>
    /// <exception cref="NotSupportedException">The type of the property that must be assigned is not supported.</exception>
    private static void SetValueTypePropertyValue(PropertyInfo propertyInfo, T obj, string value, string formatString, CultureInfo culture) {
      if(string.IsNullOrEmpty(value)) {
        if(propertyInfo.PropertyType == typeof(string)) {
          propertyInfo.SetValue(obj, string.Empty, null);
          return;
        }
        else if(!propertyInfo.PropertyType.IsValueType || propertyInfo.PropertyType.IsNullable()) {
          propertyInfo.SetValue(obj, null, null);
          return;
        }
        else {
          throw new FormatException(string.Format(CultureInfo.CurrentUICulture, Resources.FormatExceptionInvalidTypeValue, value, propertyInfo.PropertyType));
        }
      }

      if(propertyInfo.PropertyType.ActualType() == typeof(bool)) {
        CsvTransformer<T>.SetBooleanValue(propertyInfo, obj, value, formatString, culture);
      }
      else if(propertyInfo.PropertyType.ActualType() == typeof(char)) {
        CsvTransformer<T>.SetCharValue(propertyInfo, obj, value, formatString, culture);
      }
      else if(propertyInfo.PropertyType.ActualType() == typeof(DateTime)) {
        CsvTransformer<T>.SetDateTimeValue(propertyInfo, obj, value, formatString, culture);
      }
      else if(propertyInfo.PropertyType.ActualType() == typeof(int)) {
        CsvTransformer<T>.SetPropertyValue<int>(propertyInfo, obj, value, formatString, culture, int.TryParse, int.TryParse);
      }
      else if(propertyInfo.PropertyType.ActualType() == typeof(byte)) {
        CsvTransformer<T>.SetPropertyValue<byte>(propertyInfo, obj, value, formatString, culture, byte.TryParse, byte.TryParse);
      }
      else if(propertyInfo.PropertyType.ActualType() == typeof(short)) {
        CsvTransformer<T>.SetPropertyValue<short>(propertyInfo, obj, value, formatString, culture, short.TryParse, short.TryParse);
      }
      else if(propertyInfo.PropertyType.ActualType() == typeof(long)) {
        CsvTransformer<T>.SetPropertyValue<long>(propertyInfo, obj, value, formatString, culture, long.TryParse, long.TryParse);
      }
      else if(propertyInfo.PropertyType.ActualType() == typeof(float)) {
        CsvTransformer<T>.SetPropertyValue<float>(propertyInfo, obj, value, formatString, culture, float.TryParse, float.TryParse);
      }
      else if(propertyInfo.PropertyType.ActualType() == typeof(double)) {
        CsvTransformer<T>.SetPropertyValue<double>(propertyInfo, obj, value, formatString, culture, double.TryParse, double.TryParse);
      }
      else if(propertyInfo.PropertyType.ActualType() == typeof(decimal)) {
        CsvTransformer<T>.SetPropertyValue<decimal>(propertyInfo, obj, value, formatString, culture, decimal.TryParse, decimal.TryParse);
      }
      else {
        throw new NotSupportedException("Unable to parse data for data-type " + propertyInfo.PropertyType);
      }
    }

    /// <summary>Set a property value of type <see cref="Boolean"/> of an object.</summary>
    /// <param name="propertyInfo">The property that must be assigned.</param>
    /// <param name="obj">Reference to the object whose property must be assigned.</param>
    /// <param name="value">The value that must be assigned.</param>
    /// <param name="formatString">An optional format string that can be used to properly parse the value.</param>
    /// <param name="culture">An optional culture name that can be used to properly parse the value.</param>
    /// <exception cref="FormatException">The value cannot be recognized as a valid <see cref="Boolean"/>.</exception>
    private static void SetBooleanValue(PropertyInfo propertyInfo, T obj, string value, string formatString, CultureInfo culture) {
      /* Check if the format string is defined. If not, use the default parse-functionality */
      if(string.IsNullOrEmpty(formatString)) {
        bool parsedValue;
        if(bool.TryParse(value, out parsedValue)) {
          propertyInfo.SetValue(obj, parsedValue, null);
          return;
        }
        else {
          throw new FormatException(string.Format(CultureInfo.CurrentUICulture, Resources.FormatExceptionInvalidBooleanValue, value));
        }
      }

      /* Check if the format string matches the special true/false format string */
      Match formatMatch = TrueFalseFormatRegex.Match(formatString);
      if(formatMatch.Success) {
        /* Extract the true-string and false-string from the format */
        string trueString = formatMatch.Groups["trueString"].Value;
        string falseString = formatMatch.Groups["falseString"].Value;
        if(trueString.Equals(value)) {
          propertyInfo.SetValue(obj, true, null);
        }
        else if(falseString.Equals(value)) {
          propertyInfo.SetValue(obj, false, null);
        }
        else {
          throw new FormatException(string.Format(CultureInfo.CurrentUICulture, Resources.FormatExceptionInvalidBooleanValue, value));
        }

        return;
      }

      /* Check if the format string matches the default format string */
      formatMatch = DefaultFormatRegex.Match(formatString);
      if(formatMatch.Success) {
        /* Divert the handling to a generic algoritm */
        CsvTransformer<T>.SetPropertyValue<bool>(propertyInfo, obj, value, formatString, culture, bool.TryParse, null);
        return;
      }

      /* If this point is reached, the format string is not recognized and therefore the value cannot be parsed */
      throw new FormatException(string.Format(CultureInfo.CurrentUICulture, Resources.FormatExceptionInvalidBooleanValue, value));
    }

    /// <summary>Set a property value of type <see cref="String"/> of an object.</summary>
    /// <param name="propertyInfo">The property that must be assigned.</param>
    /// <param name="obj">Reference to the object whose property must be assigned.</param>
    /// <param name="value">The value that must be assigned.</param>
    /// <param name="formatString">An optional format string that can be used to properly parse the value.</param>
    /// <exception cref="InvalidOperationException">The format string contains an invalid formatting definition.</exception>
    private static void SetStringValue(PropertyInfo propertyInfo, T obj, string value, string formatString) {
      /* Check if the format string is defined. If not, simply assign the value to the property */
      if(string.IsNullOrEmpty(formatString) || !formatString.Contains('{')) {
        propertyInfo.SetValue(obj, value.Trim(), null);
        return;
      }

      /* Check if the format string contains open and close brackets. */
      int indexOfOpenBracket = formatString.IndexOf('{');
      int indexOfCloseBracket = formatString.IndexOf('}');
      if((indexOfOpenBracket == -1 ^ indexOfCloseBracket == -1) || indexOfOpenBracket > indexOfCloseBracket) {
        throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, Resources.InvalidOperationExceptionInvalidFormatString, value));
      }

      string realValue;
      if(indexOfOpenBracket == -1 && indexOfCloseBracket == -1) {
        /* If the format string does not contain any brackets, no further formatting is required */
        realValue = value;
      }
      else {
        /* If the format string contains brackets, apply some extra formatting logic */
        string prefix = indexOfOpenBracket == 0 ? string.Empty : formatString.Substring(0, indexOfOpenBracket);
        string postfix = indexOfCloseBracket == formatString.Length - 1 ? string.Empty : formatString.Substring(indexOfCloseBracket + 1);
        string pattern = string.Format(CultureInfo.InvariantCulture, "^{0}(?<subFormat>.*){1}$", prefix, postfix);
        Match match = Regex.Match(value, pattern);
        if(!match.Success) {
          throw new FormatException(string.Format(CultureInfo.CurrentUICulture, Resources.FormatExceptionInvalidFormatting, value));
        }

        realValue = match.Groups["subFormat"].Value;
      }

      /* Finally, assign the actual value to the property */
      propertyInfo.SetValue(obj, realValue.Trim(), null);
    }

    /// <summary>Set a property value of type <see cref="DateTime"/> of an object.</summary>
    /// <param name="propertyInfo">The property that must be assigned.</param>
    /// <param name="obj">Reference to the object whose property must be assigned.</param>
    /// <param name="value">The value that must be assigned.</param>
    /// <param name="formatString">An optional format string that can be used to properly parse the value.</param>
    /// <param name="culture">An optional culture name that can be used to properly parse the value.</param>
    /// <exception cref="FormatException">The value cannot be recognized as a valid <see cref="DateTime"/>.</exception>
    /// <exception cref="InvalidOperationException">The format string contains an invalid formatting definition.</exception>
    private static void SetDateTimeValue(PropertyInfo propertyInfo, T obj, string value, string formatString, CultureInfo culture) {
      DateTime parsedValue;
      bool success;
      /* Check if the format string is defined. If not, use the default parse-functionality */
      if(string.IsNullOrEmpty(formatString)) {
        success = DateTime.TryParse(value, culture, DateTimeStyles.None, out parsedValue);
        if(success) {
          propertyInfo.SetValue(obj, parsedValue, null);
          return;
        }
        else {
          throw new FormatException(string.Format(CultureInfo.CurrentUICulture, Resources.FormatExceptionInvalidDateTimeValue, value));
        }
      }
      else if(!formatString.Contains('{')) {
        success = DateTime.TryParseExact(value, formatString, culture, DateTimeStyles.None, out parsedValue);
        if(success) {
          propertyInfo.SetValue(obj, parsedValue, null);
          return;
        }
        else {
          throw new FormatException(string.Format(CultureInfo.CurrentUICulture, Resources.FormatExceptionInvalidDateTimeValue, value));
        }
      }

      /* Check if the format string contains open and close brackets. */
      int indexOfOpenBracket = formatString.IndexOf('{');
      int indexOfCloseBracket = formatString.IndexOf('}');
      if((indexOfOpenBracket == -1 ^ indexOfCloseBracket == -1) || indexOfOpenBracket > indexOfCloseBracket) {
        throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, Resources.InvalidOperationExceptionInvalidFormatString, value));
      }

      string realValue;
      string realFormat;
      if(indexOfOpenBracket == -1 && indexOfCloseBracket == -1) {
        /* If the format string does not contain any brackets, no further formatting is required */
        realValue = value;
        realFormat = formatString;
      }
      else {
        /* If the format string contains brackets, apply some extra formatting logic */
        string prefix = indexOfOpenBracket == 0 ? string.Empty : formatString.Substring(0, indexOfOpenBracket);
        string postfix = indexOfCloseBracket == formatString.Length - 1 ? string.Empty : formatString.Substring(indexOfCloseBracket + 1);

        realFormat = formatString.Substring(indexOfOpenBracket, indexOfCloseBracket - indexOfOpenBracket + 1);
        int indexOfColon = realFormat.IndexOf(':');
        if(indexOfColon == -1) {
          realFormat = string.Empty;
        }
        else {
          realFormat = realFormat.Substring(indexOfColon + 1, realFormat.Length - indexOfColon - 2);
        }

        string pattern = string.Format(CultureInfo.InvariantCulture, "^{0}(?<subFormat>.*){1}$", prefix, postfix);
        Match match = Regex.Match(value, pattern);
        if(!match.Success) {
          throw new FormatException(string.Format(CultureInfo.CurrentUICulture, Resources.FormatExceptionInvalidFormatting, value));
        }

        realValue = match.Groups["subFormat"].Value;
      }

      /* Finally, try to parse the value into the requested type */
      if(string.IsNullOrEmpty(realValue) && propertyInfo.PropertyType == typeof(DateTime?)) {
        propertyInfo.SetValue(obj, null, null);
        return;
      }

      success = string.IsNullOrEmpty(realFormat)
        ? DateTime.TryParse(realValue, culture, DateTimeStyles.None, out parsedValue)
        : DateTime.TryParseExact(realValue, realFormat, culture, DateTimeStyles.None, out parsedValue);
      if(success) {
        propertyInfo.SetValue(obj, parsedValue, null);
        return;
      }
      else {
        throw new FormatException(string.Format(CultureInfo.CurrentUICulture, Resources.FormatExceptionInvalidDateTimeValue, value));
      }
    }

    /// <summary>Set a property value of an enum type of an object.</summary>
    /// <param name="propertyInfo">The property that must be assigned.</param>
    /// <param name="obj">Reference to the object whose property must be assigned.</param>
    /// <param name="value">The value that must be assigned.</param>
    /// <param name="formatString">An optional format string that can be used to properly parse the value.</param>
    /// <param name="culture">An optional culture name that can be used to properly parse the value.</param>
    /// <exception cref="FormatException">The value cannot be recognized as a valid enum value.</exception>
    /// <exception cref="InvalidOperationException">The format string contains an invalid formatting definition.</exception>
    private static void SetEnumValue(PropertyInfo propertyInfo, T obj, string value, string formatString, CultureInfo culture) {
      bool success;

      string[] enumNames = Enum.GetNames(propertyInfo.PropertyType.ActualType());

      /* Check if the format string is defined. If not, simply assign the value to the property */
      if(string.IsNullOrEmpty(formatString) || !formatString.Contains('{')) {
        /* First, check if the value equals one of the enum names */
        if(enumNames.Contains(value)) {
          object enumValue = Enum.Parse(propertyInfo.PropertyType.ActualType(), value);
          propertyInfo.SetValue(obj, enumValue, null);
          return;
        }
        else {
          /* Then, try to parse the value as an int */
          int enumAsInt;
          success = int.TryParse(value, NumberStyles.Number, culture, out enumAsInt);
          if(success && Enum.IsDefined(propertyInfo.PropertyType.ActualType(), enumAsInt)) {
            propertyInfo.SetValue(obj, Enum.ToObject(propertyInfo.PropertyType.ActualType(), enumAsInt), null);
            return;
          }
          else {
            throw new FormatException(string.Format(CultureInfo.CurrentUICulture, Resources.FormatExceptionInvalidTypeValue, value, propertyInfo.PropertyType));
          }
        }
      }

      /* Check if the format string contains open and close brackets. */
      int indexOfOpenBracket = formatString.IndexOf('{');
      int indexOfCloseBracket = formatString.IndexOf('}');
      if((indexOfOpenBracket == -1 ^ indexOfCloseBracket == -1) || indexOfOpenBracket > indexOfCloseBracket) {
        throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, Resources.InvalidOperationExceptionInvalidFormatString, value));
      }

      string realValue;
      if(indexOfOpenBracket == -1 && indexOfCloseBracket == -1) {
        /* If the format string does not contain any brackets, no further formatting is required */
        realValue = value;
      }
      else {
        /* If the format string contains brackets, apply some extra formatting logic */
        string prefix = indexOfOpenBracket == 0 ? string.Empty : formatString.Substring(0, indexOfOpenBracket);
        string postfix = indexOfCloseBracket == formatString.Length - 1 ? string.Empty : formatString.Substring(indexOfCloseBracket + 1);
        string pattern = string.Format(CultureInfo.InvariantCulture, "^{0}(?<subFormat>.*){1}$", prefix, postfix);
        Match match = Regex.Match(value, pattern);
        if(!match.Success) {
          throw new FormatException(string.Format(CultureInfo.CurrentUICulture, Resources.FormatExceptionInvalidFormatting, value));
        }

        realValue = match.Groups["subFormat"].Value;
      }

      if(string.IsNullOrEmpty(realValue) && propertyInfo.PropertyType.IsNullable()) {
        propertyInfo.SetValue(obj, null, null);
        return;
      }

      /* Finally, assign the actual value to the property */
      /* First, check if the value equals one of the enum names */
      if(enumNames.Contains(realValue)) {
        object enumValue = Enum.Parse(propertyInfo.PropertyType.ActualType(), realValue);
        propertyInfo.SetValue(obj, enumValue, null);
        return;
      }
      else {
        /* Then, try to parse the value as an int */
        int enumAsInt;
        success = int.TryParse(realValue, NumberStyles.Number, culture, out enumAsInt);
        if(success && Enum.IsDefined(propertyInfo.PropertyType.ActualType(), enumAsInt)) {
          propertyInfo.SetValue(obj, Enum.ToObject(propertyInfo.PropertyType.ActualType(), enumAsInt), null);
          return;
        }
        else {
          throw new FormatException(string.Format(CultureInfo.CurrentUICulture, Resources.FormatExceptionInvalidTypeValue, value, propertyInfo.PropertyType));
        }
      }
    }

    /// <summary>Set a property value of a <see cref="Char"/> type of an object.</summary>
    /// <param name="propertyInfo">The property that must be assigned.</param>
    /// <param name="obj">Reference to the object whose property must be assigned.</param>
    /// <param name="value">The value that must be assigned.</param>
    /// <param name="formatString">An optional format string that can be used to properly parse the value.</param>
    /// <param name="culture">An optional culture name that can be used to properly parse the value.</param>
    /// <exception cref="FormatException">The value cannot be recognized as a valid <see cref="Char"/>.</exception>
    /// <exception cref="InvalidOperationException">The format string contains an invalid formatting definition.</exception>
    private static void SetCharValue(PropertyInfo propertyInfo, T obj, string value, string formatString, CultureInfo culture) {
      bool success;
      char parsedValue;

      /* Check if the format string is defined. If not, simply assign the value to the property */
      if(string.IsNullOrEmpty(formatString) || !formatString.Contains('{')) {
        /* First, check if the value can be parsed directly */
        success = char.TryParse(value, out parsedValue);
        if(success) {
          propertyInfo.SetValue(obj, parsedValue, null);
          return;
        }
        else {
          /* Then, try to parse the value as an int */
          int charAsInt;
          success = int.TryParse(value, NumberStyles.Number, culture, out charAsInt);
          if(success && charAsInt >= char.MinValue && charAsInt <= char.MaxValue) {
            propertyInfo.SetValue(obj, (char)charAsInt, null);
            return;
          }
          else {
            throw new FormatException(string.Format(CultureInfo.CurrentUICulture, Resources.FormatExceptionInvalidCharValue, value));
          }
        }
      }

      /* Check if the format string contains open and close brackets. */
      int indexOfOpenBracket = formatString.IndexOf('{');
      int indexOfCloseBracket = formatString.IndexOf('}');
      if((indexOfOpenBracket == -1 ^ indexOfCloseBracket == -1) || indexOfOpenBracket > indexOfCloseBracket) {
        throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, Resources.InvalidOperationExceptionInvalidFormatString, value));
      }

      string realValue;
      if(indexOfOpenBracket == -1 && indexOfCloseBracket == -1) {
        /* If the format string does not contain any brackets, no further formatting is required */
        realValue = value;
      }
      else {
        /* If the format string contains brackets, apply some extra formatting logic */
        string prefix = indexOfOpenBracket == 0 ? string.Empty : formatString.Substring(0, indexOfOpenBracket);
        string postfix = indexOfCloseBracket == formatString.Length - 1 ? string.Empty : formatString.Substring(indexOfCloseBracket + 1);
        string pattern = string.Format(CultureInfo.InvariantCulture, "^{0}(?<subFormat>.*){1}$", prefix, postfix);
        Match match = Regex.Match(value, pattern);
        if(!match.Success) {
          throw new FormatException(string.Format(CultureInfo.CurrentUICulture, Resources.FormatExceptionInvalidFormatting, value));
        }

        realValue = match.Groups["subFormat"].Value;
      }

      if(string.IsNullOrEmpty(realValue) && propertyInfo.PropertyType == typeof(char?)) {
        propertyInfo.SetValue(obj, null, null);
        return;
      }

      /* Finally, assign the actual value to the property */
      /* First, check if the value can be parsed directly */
      success = char.TryParse(realValue, out parsedValue);
      if(success) {
        propertyInfo.SetValue(obj, parsedValue, null);
        return;
      }
      else {
        /* Then, try to parse the value as an int */
        int charAsInt;
        success = int.TryParse(realValue, NumberStyles.Number, culture, out charAsInt);
        if(success && charAsInt >= char.MinValue && charAsInt <= char.MaxValue) {
          propertyInfo.SetValue(obj, (char)charAsInt, null);
          return;
        }
        else {
          throw new FormatException(string.Format(CultureInfo.CurrentUICulture, Resources.FormatExceptionInvalidCharValue, value));
        }
      }
    }

    /// <summary>Set a property value of type <typeparamref name="TPropertyType"/> of an object.</summary>
    /// <typeparam name="TPropertyType">The type of the property that must be set.</typeparam>
    /// <param name="propertyInfo">The property that must be assigned.</param>
    /// <param name="obj">Reference to the object whose property must be assigned.</param>
    /// <param name="value">The value that must be assigned.</param>
    /// <param name="formatString">An optional format string that can be used to properly parse the value.</param>
    /// <param name="culture">An optional culture name that can be used to properly parse the value.</param>
    /// <param name="parseFunc">The delegate that is used to perform the actual parsing.</param>
    /// <param name="parseFormattedFunc">The delegate that is used to perform the actual culture-aware parsing.</param>
    /// <exception cref="FormatException">The value cannot be recognized as a valid <see cref="Boolean"/>.</exception>
    /// <exception cref="InvalidOperationException">The format string contains an invalid formatting definition.</exception>
    private static void SetPropertyValue<TPropertyType>(PropertyInfo propertyInfo, T obj, string value, string formatString, CultureInfo culture, TryParse<TPropertyType> parseFunc, TryParseFormatted<TPropertyType> parseFormattedFunc) {
      TPropertyType parsedValue;
      bool success;
      /* Check if the format string is defined. If not, use the default parse-functionality */
      if(string.IsNullOrEmpty(formatString) || !formatString.Contains('{')) {
        success = parseFormattedFunc == null ? parseFunc(value, out parsedValue) : parseFormattedFunc(value, NumberStyles.Any, culture, out parsedValue);
        if(success) {
          propertyInfo.SetValue(obj, parsedValue, null);
          return;
        }
        else {
          throw new FormatException(string.Format(CultureInfo.CurrentUICulture, Resources.FormatExceptionInvalidTypeValue, value, typeof(TPropertyType)));
        }
      }

      /* Check if the format string contains open and close brackets. */
      int indexOfOpenBracket = formatString.IndexOf('{');
      int indexOfCloseBracket = formatString.IndexOf('}');
      if((indexOfOpenBracket == -1 ^ indexOfCloseBracket == -1) || indexOfOpenBracket > indexOfCloseBracket) {
        throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, Resources.InvalidOperationExceptionInvalidFormatString, value));
      }

      string realValue;
      if(indexOfOpenBracket == -1 && indexOfCloseBracket == -1) {
        /* If the format string does not contain any brackets, no further formatting is required */
        realValue = value;
      }
      else {
        /* If the format string contains brackets, apply some extra formatting logic */
        string prefix = indexOfOpenBracket == 0 ? string.Empty : formatString.Substring(0, indexOfOpenBracket);
        string postfix = indexOfCloseBracket == formatString.Length - 1 ? string.Empty : formatString.Substring(indexOfCloseBracket + 1);
        string pattern = string.Format(CultureInfo.InvariantCulture, "^{0}(?<subFormat>.*){1}$", prefix, postfix);
        Match match = Regex.Match(value, pattern);
        if(!match.Success) {
          throw new FormatException(string.Format(CultureInfo.CurrentUICulture, Resources.FormatExceptionInvalidFormatting, value));
        }

        realValue = match.Groups["subFormat"].Value;
      }

      /* Finally, try to parse the value into the requested type */
      if(string.IsNullOrEmpty(realValue) && (!propertyInfo.PropertyType.IsValueType || propertyInfo.PropertyType.IsNullable())) {
        propertyInfo.SetValue(obj, null, null);
        return;
      }

      success = parseFormattedFunc == null ? parseFunc(realValue, out parsedValue) : parseFormattedFunc(realValue, NumberStyles.Any, culture, out parsedValue);
      if(success) {
        propertyInfo.SetValue(obj, parsedValue, null);
        return;
      }
      else {
        throw new FormatException(string.Format(CultureInfo.CurrentUICulture, Resources.FormatExceptionInvalidTypeValue, value, typeof(TPropertyType)));
      }
    }
    #endregion
  }
}
