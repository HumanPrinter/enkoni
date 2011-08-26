//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvTransformer.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2011. All rights reserved.
// </copyright>
// <summary>
//     This class is capable of transforming objects into CSV data and vice versa.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Enkoni.Framework.Serialization {
  /// <summary>Transforms an instance of type <typeparamref name="T"/> to and from CSV data.</summary>
  /// <typeparam name="T">Type of the object that has to be serialized.</typeparam>
  public class CsvTransformer<T> : Transformer<T> where T : new() {
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

    #region Properties
		/// <summary>Gets the mappings of the columnnames. The dictionary uses the columnindex as key and columnname as value.</summary>
		protected internal Dictionary<int, string> ColumnNameMappings { get; private set; }

    /// <summary>Gets or sets a value indicating whether a header-line must be included when serializing the objects.</summary>
    protected internal bool EmitHeader { get; set; }

    /// <summary>Gets or sets a value indicating whether the header should be ignored when reading the file.</summary>
    protected internal bool IgnoreHeaderOnRead { get; set; }

    /// <summary>Gets or sets the separator-character.</summary>
    protected internal char Separator { get; set; }

		/// <summary>Gets the delegates that give access to the properties of the instances that need to be serialized and deserialized.</summary>
		protected Dictionary<int, Delegate> PropertyDelegates { get; private set; }

		/// <summary>Gets the mappings of the formatstrings. The dictionary uses the columnindex as key and formatstring as value.</summary>
		protected Dictionary<int, string> FormatMappings { get; private set; }

		/// <summary>Gets the mappings of the cultures. The dictionary uses the columnindex as key and culturename as value.</summary>
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

				if(propertyValue == null) {
					propertyValue = string.Empty;
				}

				string formatString = firstField ? "{0" : "{0}{1";
				if(this.FormatMappings.ContainsKey(columnIndex)) {
					formatString += ":" + this.FormatMappings[columnIndex];
				}

				formatString += "}";

				CultureInfo culture = null;
				if(this.CultureMappings.ContainsKey(columnIndex)) {
					culture = new CultureInfo(this.CultureMappings[columnIndex]);
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
					if(culture != null) {
						builder.AppendFormat(culture, formatString, this.Separator, propertyValue);
					}
					else {
						builder.AppendFormat(formatString, this.Separator, propertyValue);
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
			string[] columns = input.Split(this.Separator);
			for(int colIndex = 0; colIndex < columns.Count(); colIndex++) {
				if(this.ColumnNameMappings.ContainsKey(colIndex)) {
					string cultureName;
					string formatString;
					this.FormatMappings.TryGetValue(colIndex, out formatString);
					this.CultureMappings.TryGetValue(colIndex, out cultureName);

					PropertyInfo propertyInfo = obj.GetType().GetProperty(this.ColumnNameMappings[colIndex]);
					SetPropertyValue(propertyInfo, obj, columns[colIndex], formatString, cultureName);
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
    /// <summary>Set a property value of an object.</summary>
    /// <param name="propertyInfo">The property that must be assigned.</param>
    /// <param name="obj">Reference to the object whose property must be assigned.</param>
    /// <param name="value">The value that must be assigned.</param>
    /// <param name="formatString">An optional formatstring that can be used to properly parse the value.</param>
    /// <param name="cultureName">An optional culture-name that can be used to properly parse the value.</param>
    /// <exception cref="NotSupportedException">The type of the property that must be assigned is not supported.</exception>
    private static void SetPropertyValue(PropertyInfo propertyInfo, T obj, string value, string formatString, string cultureName) {
      CultureInfo culture = CultureInfo.InvariantCulture;
      if(cultureName != null) {
        culture = new CultureInfo(cultureName);
      }

      if(propertyInfo.PropertyType == typeof(string)) {
        propertyInfo.SetValue(obj, value, null);
      }
      else if(propertyInfo.PropertyType == typeof(int)) {
        value = string.IsNullOrEmpty(value) ? default(int).ToString(culture) : value;
        propertyInfo.SetValue(obj, int.Parse(value, culture), null);
      }
      else if(propertyInfo.PropertyType == typeof(float)) {
        value = string.IsNullOrEmpty(value) ? default(float).ToString(culture) : value;
        propertyInfo.SetValue(obj, float.Parse(value, culture), null);
      }
      else if(propertyInfo.PropertyType == typeof(double)) {
        value = string.IsNullOrEmpty(value) ? default(double).ToString(culture) : value;
        propertyInfo.SetValue(obj, double.Parse(value, culture), null);
      }
      else if(propertyInfo.PropertyType == typeof(bool)) {
        value = string.IsNullOrEmpty(value) ? default(bool).ToString(culture) : value;
        propertyInfo.SetValue(obj, bool.Parse(value), null);
      }
      else if(propertyInfo.PropertyType == typeof(DateTime)) {
        value = string.IsNullOrEmpty(value) ? default(DateTime).ToString(culture) : value;
        if(formatString != null) {
          propertyInfo.SetValue(obj, DateTime.ParseExact(value, formatString, culture), null);
        }
        else {
          propertyInfo.SetValue(obj, DateTime.Parse(value, culture), null);
        }
      }
      else {
        throw new NotSupportedException("Unable to parse data for data-type " + propertyInfo.PropertyType);
      }
    }
    #endregion
  }
}
