using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

using Enkoni.Framework.Collections;

namespace Enkoni.Framework {
  /// <summary>This class contains some all-purpose extension-methods.</summary>
  public static class Extensions {
    #region Private static variables
    /// <summary>Caches types and there derived types for performance reasons.</summary>
    private static Dictionary<Type, Type[]> typeCache = new Dictionary<Type, Type[]>();
    #endregion

    #region ICloneable extensions
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
    #endregion

    #region Event extensions
    /// <summary>Fires an event in a sequential way. An eventhandler needs to finish before the next eventhandler will be called. This method will 
    /// return when all the eventhandlers have finished. <br/>
    /// This method will automatically check if there are any eventhandlers subscribed to <paramref name="handler"/> and it will automatically 
    /// propagate the call the the appropriate thread.</summary>
    /// <param name="handler">The multicast delegate that must be executed.</param>
    /// <param name="sender">The object that triggered the event.</param>
    /// <param name="e">Any metadata that must be passed to the eventhandlers.</param>
    public static void Fire(this EventHandler handler, object sender, EventArgs e) {
      UnsafeFire(handler, sender, e);
    }

    /// <summary>Fires an event in a sequential way. An eventhandler needs to finish before the next eventhandler will be called. This method will 
    /// return when all the eventhandlers have finished. <br/>
    /// This method will automatically check if there are any eventhandlers subscribed to <paramref name="handler"/> and it will automatically 
    /// propagate the call the the appropriate thread.</summary>
    /// <typeparam name="T">The type of eventargs that is passed with the event.</typeparam>
    /// <param name="handler">The multicast delegate that must be executed.</param>
    /// <param name="sender">The object that triggered the event.</param>
    /// <param name="e">Any metadata that must be passed to the eventhandlers.</param>
    public static void Fire<T>(this EventHandler<T> handler, object sender, T e) where T : EventArgs {
      UnsafeFire(handler, sender, e);
    }

    /// <summary>Fires an event in a parallel way. This method will return when all the eventhandlers have finished.<br/>
    /// This method will automatically check if there are any eventhandlers subscribed to <paramref name="handler"/> and it will automatically 
    /// propagate the call the the appropriate thread.</summary>
    /// <param name="handler">The multicast delegate that must be executed.</param>
    /// <param name="sender">The object that triggered the event.</param>
    /// <param name="e">Any metadata that must be passed to the eventhandlers.</param>
    public static void FireInParallel(this EventHandler handler, object sender, EventArgs e) {
      UnsafeFireInParallel(handler, sender, e);
    }

    /// <summary>Fires an event in a parallel way. This method will return when all the eventhandlers have finished.<br/>
    /// This method will automatically check if there are any eventhandlers subscribed to <paramref name="handler"/> and it will automatically 
    /// propagate the call the the appropriate thread.</summary>
    /// <typeparam name="T">The type of eventargs that is passed with the event.</typeparam>
    /// <param name="handler">The multicast delegate that must be executed.</param>
    /// <param name="sender">The object that triggered the event.</param>
    /// <param name="e">Any metadata that must be passed to the eventhandlers.</param>
    public static void FireInParallel<T>(this EventHandler<T> handler, object sender, T e) where T : EventArgs {
      UnsafeFireInParallel(handler, sender, e);
    }

    /// <summary>Fires an event in an asynchronous way. When this method returns, the eventhandlers may still be running.<br/>
    /// This method will automatically check if there are any eventhandlers subscribed to <paramref name="handler"/> and it will automatically 
    /// propagate the call the the appropriate thread.</summary>
    /// <param name="handler">The multicast delegate that must be executed.</param>
    /// <param name="sender">The object that triggered the event.</param>
    /// <param name="e">Any metadata that must be passed to the eventhandlers.</param>
    public static void FireAsync(this EventHandler handler, object sender, EventArgs e) {
      UnsafeFireAsync(handler, sender, e);
    }

    /// <summary>Fires an event in an asynchronous way. When this method returns, the eventhandlers may still be running.<br/>
    /// This method will automatically check if there are any eventhandlers subscribed to <paramref name="handler"/> and it will automatically 
    /// propagate the call the the appropriate thread.</summary>
    /// <typeparam name="T">The type of eventargs that is passed with the event.</typeparam>
    /// <param name="handler">The multicast delegate that must be executed.</param>
    /// <param name="sender">The object that triggered the event.</param>
    /// <param name="e">Any metadata that must be passed to the eventhandlers.</param>
    public static void FireAsync<T>(this EventHandler<T> handler, object sender, T e) where T : EventArgs {
      UnsafeFireAsync(handler, sender, e);
    }
    #endregion

    #region String extensions
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

    /// <summary>Capitalizes the first letter of each word assuming that words are separated by a single space.</summary>
    /// <param name="source">The string that must be capitalized.</param>
    /// <param name="keepExistingCapitals">Indicates if any capitals that are already in the string must be preserved or must be lowered.</param>
    /// <param name="culture">A <see cref="CultureInfo"/> object that supplies culture-specific casing rules.</param>
    /// <returns>The capitalized string. If <paramref name="source"/> is empty, <paramref name="source"/> is returned without
    /// modifications.</returns>
    public static string Capitalize(this string source, bool keepExistingCapitals, CultureInfo culture) {
      if(source == null) {
        throw new ArgumentNullException("source");
      }

      if(string.IsNullOrEmpty(source.Trim())) {
        return source;
      }

      if(culture == null) {
        culture = CultureInfo.CurrentCulture;
      }

      if(keepExistingCapitals) {
        IEnumerable<string> capitalizedStrings = source.Split(' ')
          .Select(str => new string(new char[] { str.First() }).ToUpper(culture) + /* Capitalize the first character */
          new string(str.Skip(1).ToArray()));                                      /* Keep the remaining characters as they are */
        return string.Join(" ", capitalizedStrings.ToArray());
      }
      else {
        IEnumerable<string> capitalizedStrings = source.Split(' ')
          .Select(str => new string(new char[] { str.First() }).ToUpper(culture) + /* Capitalize the first character */
          new string(str.Skip(1).ToArray()).ToLower(culture));                     /* Lower the remaining characters */
        return string.Join(" ", capitalizedStrings.ToArray());
      }
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

    /// <summary>Capitalizes the first letter of a sentence assuming that words are separated by a single space.</summary>
    /// <param name="source">The sentence that must be capitalized.</param>
    /// <param name="keepExistingCapitals">Indicates if any capitals that are already in the string must be preserved or must be lowered.</param>
    /// <param name="culture">A <see cref="CultureInfo"/> object that supplies culture-specific casing rules.</param>
    /// <returns>The capitalized sentence. If <paramref name="source"/> is empty, <paramref name="source"/> is returned without
    /// modifications.</returns>
    public static string CapitalizeSentence(this string source, bool keepExistingCapitals, CultureInfo culture) {
      if(source == null) {
        throw new ArgumentNullException("source");
      }

      if(string.IsNullOrEmpty(source.Trim())) {
        return source;
      }

      if(culture == null) {
        culture = CultureInfo.CurrentCulture;
      }

      if(keepExistingCapitals) {
        IEnumerable<string> capitalizedStrings = source.Split(' ')
          .Select((str, i) => i == 0
              ? new string(new char[] { str.First() }).ToUpper(culture) + /* Capitalize the first character of the first word */
                new string(str.Skip(1).ToArray())                         /* Keep the remaining characters of the first word as they are */
              : str);                                                     /* Keep the remaining words as they are */
        return string.Join(" ", capitalizedStrings.ToArray());
      }
      else {
        IEnumerable<string> capitalizedStrings = source.Split(' ')
          .Select((str, i) => i == 0
              ? new string(new char[] { str.First() }).ToUpper(culture) + /* Capitalize the first character of the first word */
                new string(str.Skip(1).ToArray()).ToLower(culture)        /* Lower the remaining characters of the first word*/
              : str.ToLower(culture));                                    /* Lower the remaining characters of the remaining words*/
        return string.Join(" ", capitalizedStrings.ToArray());
      }
    }
    #endregion

    #region Double extensions
    /// <summary>Returns a value indicating whether both values represent the same value.</summary>
    /// <param name="source">The first value to compare.</param>
    /// <param name="obj">The second value to compare.</param>
    /// <param name="comparisonFactor">The factor that must be taken into account. If <paramref name="compareOption"/> is set to 
    /// <see cref="DoubleCompareOption.Margin"/>, the comparison factor will be treated as an absolute margin. If <paramref name="compareOption"/> is
    /// set to <see cref="DoubleCompareOption.SignificantDigits"/> the comparison factor will be treated as the number of digits that must be 
    /// examined will comparing. Note that the comparison factor in that case will be truncated to an integer.</param>
    /// <param name="compareOption">Defines the method that must be used to compare the double values.</param>
    /// <returns><see langword="true"/> if the two values are equal; otherwise, <see langword="false"/>.</returns>
    public static bool Equals(this double source, double obj, double comparisonFactor, DoubleCompareOption compareOption) {
      return new DoubleEqualityComparer(comparisonFactor, compareOption).Equals(source, obj);
    }

    /// <summary>Compares the two values and returns an integer that indicates whether the first value is less than, equal to, or greater than the 
    /// second value.</summary>
    /// <param name="source">The first value to compare.</param>
    /// <param name="value">The second value to compare.</param>
    /// <param name="comparisonFactor">The factor that must be taken into account. If <paramref name="compareOption"/> is set to 
    /// <see cref="DoubleCompareOption.Margin"/>, the comparison factor will be treated as an absolute margin. If <paramref name="compareOption"/> is
    /// set to <see cref="DoubleCompareOption.SignificantDigits"/> the comparison factor will be treated as the number of digits that must be 
    /// examined will comparing. Note that the comparison factor in that case will be truncated to an integer.</param>
    /// <param name="compareOption">Defines the method that must be used to compare the double values.</param>
    /// <returns>A signed number indicating the relative values of the two numbers. <br />
    /// Return Value Description <br/>
    /// Less than zero: <paramref name="source"/> is less than <paramref name="value"/> -or- <paramref name="source"/> is not a number 
    /// (<see cref="Double.NaN"/>) and value is a number.<br/>
    /// Zero: <paramref name="source"/> is equal to <paramref name="value"/> -or- Both <paramref name="source"/> and <paramref name="value"/> are 
    /// not a number (<see cref="Double.NaN"/>), <see cref="Double.PositiveInfinity"/>, or <see cref="Double.NegativeInfinity"/>.<br/>
    /// Greater than zero: <paramref name="source"/> is greater than <paramref name="value"/> -or- <paramref name="source"/> is a number and 
    /// <paramref name="value"/> is not a number (<see cref="Double.NaN"/>).</returns>
    public static int CompareTo(this double source, double value, double comparisonFactor, DoubleCompareOption compareOption) {
      return new DoubleComparer(comparisonFactor, compareOption).Compare(source, value);
    }
    #endregion

    #region DateTime extensions
    /// <summary>Determines the weeknumber of the given <see cref="DateTime"/> value using the ISO 8601 specification.</summary>
    /// <param name="source">The date time of which the week number must be determined.</param>
    /// <returns>The determined weeknumber.</returns>
    public static int GetWeekNumber(this DateTime source) {
      /* This implementation is inspired on the article written by Shawn Steele which is available on 
       * http://blogs.msdn.com/b/shawnste/archive/2006/01/24/517178.aspx */
      /* This implementation is slightly more complicated than simply calling GetWeekOfYear() on Calendar, because the default implementation is not 
       * entirely ISO 8601 compliant (even though the documentation says it is). */

      /* Since this method calculates the weeknumber in accordance with the ISO 8601 specification, which is culture independant, the calendar of the
       * invariant culture is used. */
      Calendar calendar = CultureInfo.InvariantCulture.Calendar;

      /* Get the day of the week */
      DayOfWeek weekDay = calendar.GetDayOfWeek(source);

      /* Make sure the date points to the Thursday (or some day later in the same week) to make sure the weeknumber calculation succeeds. */
      if(weekDay >= DayOfWeek.Monday && weekDay <= DayOfWeek.Wednesday) {
        /* Sinde the DateTime parameter isn't passed by reference, there is no harm in overwritting the parameters value. */
        source = source.AddDays(3);
      }

      /* Now the GetWeekOfYear method can be used and it will return the correct value in accordance with the ISO 8601 specification. */
      return calendar.GetWeekOfYear(source, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
    }
    #endregion

    #region Type extensions
    /// <summary>Determines if the specified type actually a nullable type.</summary>
    /// <param name="source">The type that is investigated.</param>
    /// <returns><see langword="true"/> is <paramref name="source"/> denotes a nullable type, <see langword="false"/>
    /// otherwise.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
    public static bool IsNullable(this Type source) {
      if(source == null) {
        throw new ArgumentNullException("source");
      }

      return source.IsGenericType && source.GetGenericTypeDefinition() == typeof(Nullable<>);
    }

    /// <summary>Returns the actual type of <paramref name="source"/>. If <paramref name="source"/> denotes a nullable type,
    /// the underlying type is returned. Otherwise, <paramref name="source"/> is returned.</summary>
    /// <param name="source">The type that is investigated.</param>
    /// <returns>The underlying type if <paramref name="source"/> is nullable or <paramref name="source"/> if it is not 
    /// nullable.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
    public static Type ActualType(this Type source) {
      if(source == null) {
        throw new ArgumentNullException("source");
      }

      if(source.IsNullable()) {
        NullableConverter converter = new NullableConverter(source);
        return converter.UnderlyingType;
      }
      else {
        return source;
      }
    }

    /// <summary>Gets all the base classes extended or inherited by the current <see cref="Type"/>.</summary>
    /// <param name="source">The <see cref="Type"/> whose base classes must be retrieved.</param>
    /// <returns>The base classes that are extended or inherited by the current <see cref="Type"/>.</returns>
    public static Type[] GetBaseClasses(this Type source) {
      if(source == null) {
        throw new ArgumentNullException("source");
      }

      List<Type> baseTypes = new List<Type>();

      Type baseType = source.BaseType;
      while(baseType != null) {
        if(source.IsGenericTypeDefinition && baseType.IsGenericType) {
          baseType = baseType.GetGenericTypeDefinition();
        }

        baseTypes.Add(baseType);
        baseType = baseType.BaseType;
      }

      return baseTypes.ToArray();
    }

    /// <summary>Gets all the base types (base classes and interfaces) extended, implemented or inherited by the current <see cref="Type"/>.</summary>
    /// <param name="source">The <see cref="Type"/> whose base types must be retrieved.</param>
    /// <returns>The base types that are extended, implemented or inherited by the current <see cref="Type"/>.</returns>
    public static Type[] GetBaseTypes(this Type source) {
      if(source == null) {
        throw new ArgumentNullException("source");
      }

      Type[] interfaces = source.GetInterfaces();
      Type[] baseClasses = source.GetBaseClasses();
      if(interfaces.Length == 0) {
        return baseClasses;
      }

      if(baseClasses.Length == 0) {
        return interfaces;
      }

      Type[] baseTypes = new Type[interfaces.Length + baseClasses.Length];
      Array.Copy(interfaces, baseTypes, interfaces.Length);
      Array.Copy(baseClasses, 0, baseTypes, interfaces.Length, baseClasses.Length);

      return baseTypes;
    }

    /// <summary>Determines if the source implements the specified base type.</summary>
    /// <param name="source">The instance that must be evaluated.</param>
    /// <param name="baseType">The base type that must be implemented.</param>
    /// <returns><see langword="true"/> if <paramref name="source"/> implements <paramref name="baseType"/>;
    /// <see langword="false"/> otherwise.</returns>
    public static bool Implements(this Type source, Type baseType) {
      if(source == null) {
        throw new ArgumentNullException("source");
      }

      if(baseType == null) {
        throw new ArgumentNullException("baseType");
      }

      return source.Equals(baseType) || source.GetBaseTypes().Contains(baseType, new TypeEqualityComparer());
    }

    /// <summary>Gets the types that derive from <paramref name="source"/> and are available in the loaded assemblies. If this type has been evaluated 
    /// before, the previous results are returned regardless of any assembly load or unload event.</summary>
    /// <param name="source">The instance that must be evaluated.</param>
    /// <returns>The detected types that derive from <paramref name="source"/>.</returns>
    public static Type[] GetDerivedTypes(this Type source) {
      return GetDerivedTypes(source, false);
    }

    /// <summary>Gets the types that derive from <paramref name="source"/> and are available in the loaded assemblies.</summary>
    /// <param name="source">The instance that must be evaluated.</param>
    /// <param name="forceRescan">Forces to ignore the previous scan-results and rescan the loaded assemblies.</param>
    /// <returns>The detected types that derive from <paramref name="source"/>.</returns>
    public static Type[] GetDerivedTypes(this Type source, bool forceRescan) {
      if(source == null) {
        throw new ArgumentNullException("source");
      }

      return Find(source, forceRescan);
    }
    #endregion

    #region Helper methods
    /// <summary>Invokes a delegate. If required, the call is transfered to the appropriate thread.</summary>
    /// <param name="del">The delegate that must be invoked.</param>
    /// <param name="args">The arguments that must be passed to the delegate.</param>
    private static void InvokeDelegate(Delegate del, object[] args) {
      ISynchronizeInvoke synchronizer = del.Target as ISynchronizeInvoke;
      if(synchronizer != null) {
        /* Requires thread affinity */
        if(synchronizer.InvokeRequired) {
          synchronizer.Invoke(del, args);
          return;
        }
      }

      /* Not requiring thread affinity or invoke is not required */
      del.DynamicInvoke(args);
    }

    /// <summary>Invokes the subscribers of the specified delegate in a sequential way.</summary>
    /// <param name="del">The delegate whose subscribers must be invoked.</param>
    /// <param name="args">The arguments that must be passed to the delegate-subscribers.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void UnsafeFire(Delegate del, params object[] args) {
      /* Check if there are any subscribers */
      if(del == null) {
        return;
      }

      /* Get the subscribers and invoke them sequential */
      Delegate[] delegates = del.GetInvocationList();
      foreach(Delegate sink in delegates) {
        InvokeDelegate(sink, args);
      }
    }

    /// <summary>Invokes the subscribers of the specified delegate in a parallel way.</summary>
    /// <param name="del">The delegate whose subscribers must be invoked.</param>
    /// <param name="args">The arguments that must be passed to the delegate-subscribers.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void UnsafeFireInParallel(Delegate del, params object[] args) {
      /* Check if there are any subscribers */
      if(del == null) {
        return;
      }

      /* Get the subscribers and prepare the asynchronous invocations */
      Delegate[] delegates = del.GetInvocationList();
      List<WaitHandle> calls = new List<WaitHandle>(delegates.Length);
      Action<Delegate, object[]> asyncFire = InvokeDelegate;

      /* Invoke the subscribers */
      foreach(Delegate sink in delegates) {
        IAsyncResult asyncResult = asyncFire.BeginInvoke(sink, args, null, null);
        calls.Add(asyncResult.AsyncWaitHandle);
      }

      /* Wait untill all the subscribers are finished */
      WaitHandle[] handles = calls.ToArray();
      WaitHandle.WaitAll(handles);

      Array.ForEach(handles, handle => handle.Close());
    }

    /// <summary>Invokes the subscribers of the specified delegate in an asynchronous way.</summary>
    /// <param name="del">The delegate whose subscribers must be invoked.</param>
    /// <param name="args">The arguments that must be passed to the delegate-subscribers.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void UnsafeFireAsync(Delegate del, params object[] args) {
      /* Check if there are any subscribers */
      if(del == null) {
        return;
      }

      /* Get the subscribers and prepare the asynchronous invocations */
      Delegate[] delegates = del.GetInvocationList();
      Action<Delegate, object[]> asyncFire = InvokeDelegate;
      AsyncCallback cleanUp = asyncResult => asyncResult.AsyncWaitHandle.Close();

      /* Invoke the subscribers */
      foreach(Delegate sink in delegates) {
        asyncFire.BeginInvoke(sink, args, cleanUp, null);
      }
    }

    /// <summary>Gets the derived types from the cache for the specified type.</summary>
    /// <param name="parent">The parent type.</param>
    /// <param name="forceRescan">Indicates whether or not to ignore the cache.</param>
    /// <returns>The found types.</returns>
    private static Type[] Find(Type parent, bool forceRescan) {
      if(forceRescan) {
        lock(typeCache) {
          if(typeCache.ContainsKey(parent)) {
            typeCache.Remove(parent);
          }
        }
      }

      Type[] result;
      if(typeCache.TryGetValue(parent, out result)) {
        return result;
      }

      List<Type> list = new List<Type>();
      foreach(Assembly assembly in AppDomain.CurrentDomain.GetAssemblies()) {
        try {
          if(parent.IsInterface) {
            list.AddRange(assembly.GetTypes().Where(t => !t.Equals(parent) && parent.IsAssignableFrom(t)));
          }
          else if(parent.IsGenericParameter) {
            Type[] contrains = parent.GetGenericParameterConstraints();
            if(contrains.Length > 0) {
              foreach(Type type in assembly.GetTypes()) {
                bool add = true;
                foreach(Type item in contrains) {
                  if(!item.IsAssignableFrom(type)) {
                    add = false;
                    break;
                  }
                }

                if(add) {
                  list.Add(type);
                }
              }
            }
          }
          else {
            list.AddRange(assembly.GetTypes().Where(t => t.IsSubclassOf(parent)));
          }
        }
        catch(ReflectionTypeLoadException) {
          /* Ignore this assembly and move on */
        }
      }

      lock(typeCache) {
        if(!typeCache.TryGetValue(parent, out result)) {
          result = list.ToArray();
          typeCache.Add(parent, result);
        }
      }

      return result;
    }
    #endregion

    #region Private classes
    /// <summary>Provides the functionality to compare two <see cref="Type"/> instances.</summary>
    private class TypeEqualityComparer : IEqualityComparer<Type> {
      #region Constructors
      /// <summary>Initializes a new instance of the <see cref="TypeEqualityComparer"/> class.</summary>
      public TypeEqualityComparer() {
      }
      #endregion

      #region IEqualityComparer implementation
      /// <summary>Determines if the specified instances are equal.</summary>
      /// <param name="x">The left operand.</param>
      /// <param name="y">The right operand.</param>
      /// <returns><see langword="true"/> if both parameters are equal; <see langword="false"/> otherwise.</returns>
      public bool Equals(Type x, Type y) {
        if(x == null && y == null) {
          return true;
        }

        if(x == null || y == null) {
          return false;
        }

        if(ReferenceEquals(x, y)) {
          return true;
        }

        if(x == y) {
          return true;
        }

        return x.GUID == y.GUID && x.Assembly == y.Assembly && x.Name == y.Name && x.Namespace == y.Namespace;
      }

      /// <summary>Returns the hash code for the given instance.</summary>
      /// <param name="obj">The instance whose hash code must be returned.</param>
      /// <returns>The hash code of <paramref name="obj"/> or zero if <paramref name="obj"/> is 
      /// <see langword="null"/>.</returns>
      public int GetHashCode(Type obj) {
        return obj == null ? 0 : obj.GetHashCode();
      }
      #endregion
    }
    #endregion
  }
}
