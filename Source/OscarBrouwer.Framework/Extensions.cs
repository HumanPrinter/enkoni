//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2011. All rights reserved.
// </copyright>
// <summary>
//     Defines several all-purpose extension methods.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Enkoni.Framework {
  /// <summary>This class contains some all-purpose extension-methods.</summary>
  public static class Extensions {
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
    /// <summary>Fires an event in a sequential way. An eventhandler needs to finish before the next eventhandler will be 
    /// called. This method will return when all the eventhandlers have finished. <br/>
    /// This method will automatically check if there are any eventhandlers subscribed to <paramref name="handler"/> 
    /// and it will automatically propagate the call the the appropriate thread.</summary>
    /// <param name="handler">The multicast delegate that must be executed.</param>
    /// <param name="sender">The object that triggered the event.</param>
    /// <param name="e">Any metadata that must be passed to the eventhandlers.</param>
    public static void Fire(this EventHandler handler, object sender, EventArgs e) {
      UnsafeFire(handler, sender, e);
    }

    /// <summary>Fires an event in a sequential way. An eventhandler needs to finish before the next eventhandler will be 
    /// called. This method will return when all the eventhandlers have finished. <br/>
    /// This method will automatically check if there are any eventhandlers subscribed to <paramref name="handler"/>
    /// and it will automatically propagate the call the the appropriate thread.</summary>
    /// <typeparam name="T">The type of eventargs that is passed with the event.</typeparam>
    /// <param name="handler">The multicast delegate that must be executed.</param>
    /// <param name="sender">The object that triggered the event.</param>
    /// <param name="e">Any metadata that must be passed to the eventhandlers.</param>
    public static void Fire<T>(this EventHandler<T> handler, object sender, T e) where T : EventArgs {
      UnsafeFire(handler, sender, e);
    }

    /// <summary>Fires an event in a parallel way. This method will return when all the eventhandlers have finished. 
    /// <br/>
    /// This method will automatically check if there are any eventhandlers subscribed to <paramref name="handler"/> 
    /// and it will automatically propagate the call the the appropriate thread.</summary>
    /// <param name="handler">The multicast delegate that must be executed.</param>
    /// <param name="sender">The object that triggered the event.</param>
    /// <param name="e">Any metadata that must be passed to the eventhandlers.</param>
    public static void FireInParallel(this EventHandler handler, object sender, EventArgs e) {
      UnsafeFireInParallel(handler, sender, e);
    }

    /// <summary>Fires an event in a parallel way. This method will return when all the eventhandlers have finished. 
    /// <br/>
    /// This method will automatically check if there are any eventhandlers subscribed to <paramref name="handler"/> 
    /// and it will automatically propagate the call the the appropriate thread.</summary>
    /// <typeparam name="T">The type of eventargs that is passed with the event.</typeparam>
    /// <param name="handler">The multicast delegate that must be executed.</param>
    /// <param name="sender">The object that triggered the event.</param>
    /// <param name="e">Any metadata that must be passed to the eventhandlers.</param>
    public static void FireInParallel<T>(this EventHandler<T> handler, object sender, T e) where T : EventArgs {
      UnsafeFireInParallel(handler, sender, e);
    }

    /// <summary>Fires an event in an asynchronous way. When this method returns, the eventhandlers may still be 
    /// running.<br/>
    /// This method will automatically check if there are any eventhandlers subscribed to <paramref name="handler"/> 
    /// and it will automatically propagate the call the the appropriate thread.</summary>
    /// <param name="handler">The multicast delegate that must be executed.</param>
    /// <param name="sender">The object that triggered the event.</param>
    /// <param name="e">Any metadata that must be passed to the eventhandlers.</param>
    public static void FireAsync(this EventHandler handler, object sender, EventArgs e) {
      UnsafeFireAsync(handler, sender, e);
    }

    /// <summary>Fires an event in an asynchronous way. When this method returns, the eventhandlers may still be 
    /// running.<br/>
    /// This method will automatically check if there are any eventhandlers subscribed to <paramref name="handler"/> 
    /// and it will automatically propagate the call the the appropriate thread.</summary>
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
    /// <returns>The capitalized string.</returns>
    public static string Capitalize(this string source) {
      return source.Capitalize(null);
    }

    /// <summary>Capitalizes the first letter of each word assuming that words are separated by a single space.</summary>
    /// <param name="source">The string that must be capitalized.</param>
    /// <param name="keepExistingCapitals">Indicates if any capitals that are already in the string must be preserved or 
    /// must be lowered.</param>
    /// <returns>The capitalized string.</returns>
    public static string Capitalize(this string source, bool keepExistingCapitals) {
      return source.Capitalize(keepExistingCapitals, null);
    }

    /// <summary>Capitalizes the first letter of each word assuming that words are separated by a single space.</summary>
    /// <param name="source">The string that must be capitalized.</param>
    /// <param name="culture">A <see cref="CultureInfo"/> object that supplies culture-specific casing rules.</param>
    /// <returns>The capitalized string.</returns>
    public static string Capitalize(this string source, CultureInfo culture) {
      return source.Capitalize(false, culture);
    }

    /// <summary>Capitalizes the first letter of each word assuming that words are separated by a single space.</summary>
    /// <param name="source">The string that must be capitalized.</param>
    /// <param name="keepExistingCapitals">Indicates if any capitals that are already in the string must be preserved or 
    /// must be lowered.</param>
    /// <param name="culture">A <see cref="CultureInfo"/> object that supplies culture-specific casing rules.</param>
    /// <returns>The capitalized string.</returns>
    public static string Capitalize(this string source, bool keepExistingCapitals, CultureInfo culture) {
      if(source == null) {
        throw new ArgumentNullException("source");
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
    #endregion

    #region ICollection<T> extension methods
    /// <summary>Adds an overload for the ICollection-method 'Remove(T)' which lets the user define a comparer that must be 
    /// used.</summary>
    /// <typeparam name="T">The type of element that is stored in the collection.</typeparam>
    /// <param name="source">An <see cref="ICollection{T}"/> to remove the item from.</param>
    /// <param name="item">The item that must be removed.</param>
    /// <param name="comparer">The comparer that must be used to find the appropriate item in the collection.</param>
    /// <returns><see langword="true"/> if item was successfully removed from the <see cref="ICollection{T}"/>; otherwise, 
    /// <see langword="false"/>. This method also returns false if item is not found in the original 
    /// <see cref="ICollection{T}"/>.</returns>
    /// <exception cref="ArgumentNullException">One or more parameters are null.</exception>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1628:DocumentationTextMustBeginWithACapitalLetter",
      Justification = "The keywords 'true' and 'false' start with a lowercase letter")]
    public static bool Remove<T>(this ICollection<T> source, T item, IEqualityComparer<T> comparer) {
      if(source == null) {
        throw new ArgumentNullException("source");
      }

      if(comparer == null) {
        throw new ArgumentNullException("comparer");
      }

      if(source.Any(t => comparer.Equals(t, item))) {
        return source.Remove(source.First(t => comparer.Equals(t, item)));
      }
      else {
        return false;
      }
    }
    #endregion

    #region IList<T> extension methods
    /// <summary>Adds an overload for the IList-method 'IndexOf(T)' which lets the user define a comparer to look for the 
    /// desired item.</summary>
    /// <typeparam name="T">The type of element that is stored in the list.</typeparam>
    /// <param name="source">An <see cref="IList{T}"/> that must be searched.</param>
    /// <param name="item">The item that must be removed from the list.</param>
    /// <param name="comparer">The comparer that must be used to find the appropriate item in the list.</param>
    /// <returns>The index of <paramref name="item"/> if found in the list; otherwise, -1.</returns>
    public static int IndexOf<T>(this IList<T> source, T item, IEqualityComparer<T> comparer) {
      if(comparer == null) {
        throw new ArgumentNullException("comparer");
      }

      var itemIndexes = source.Select((t, i) => new { Item = t, Index = i });
      if(itemIndexes.Any(a => comparer.Equals(a.Item, item))) {
        return itemIndexes.First(a => comparer.Equals(a.Item, item)).Index;
      }
      else {
        return -1;
      }
    }
    #endregion

    #region List<T> extension methods
    /// <summary>Adds an overload for the List-method 'IndexOf(T, int)' which lets the user define a comparer to look for 
    /// the desired item.</summary>
    /// <typeparam name="T">The type of element that is stored in the list.</typeparam>
    /// <param name="source">An <see cref="List{T}"/> that must be searched.</param>
    /// <param name="item">The object to locate in the <see cref="List{T}"/>. The value can be <see langword="null"/> for 
    /// reference types.</param>
    /// <param name="index">The zero-based starting index of the search. 0 (zero) is valid in an empty list.</param>
    /// <param name="comparer">The comparer that must be used to find the appropriate item in the list.</param>
    /// <returns>The zero-based index of the first occurrence of item within the range of elements in the 
    /// <see cref="List{T}"/> that extends from index to the last element, if found; otherwise, –1.</returns>
    /// <exception cref="ArgumentNullException">Parameter <paramref name="comparer"/> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is outside the range of valid indexes for the
    /// <see cref="List{T}"/>.</exception>
    public static int IndexOf<T>(this List<T> source, T item, int index, IEqualityComparer<T> comparer) {
      if(comparer == null) {
        throw new ArgumentNullException("comparer");
      }

      if(index < 0 || (index > 0 && index >= source.Count())) {
        throw new ArgumentOutOfRangeException("index", index, "Index is out of range");
      }

      var itemIndexes = source.Skip(index).Select((t, i) => new { Item = t, Index = i });
      if(itemIndexes.Any(a => comparer.Equals(a.Item, item))) {
        return itemIndexes.First(a => comparer.Equals(a.Item, item)).Index + index;
      }
      else {
        return -1;
      }
    }

    /// <summary>Adds an overload for the List-method 'IndexOf(T, int, int)' which lets the user define a comparer to look 
    /// for the desired item.</summary>
    /// <typeparam name="T">The type of element that is stored in the list.</typeparam>
    /// <param name="source">An <see cref="List{T}"/> that must be searched.</param>
    /// <param name="item">The object to locate in the <see cref="List{T}"/>. The value can be <see langword="null"/> for 
    /// reference types.</param>
    /// <param name="index">The zero-based starting index of the search. 0 (zero) is valid in an empty list.</param>
    /// <param name="count">The number of elements in the section to search.</param>
    /// <param name="comparer">The comparer that must be used to find the appropriate item in the list.</param>
    /// <returns>The zero-based index of the first occurrence of item within the range of elements in the 
    /// <see cref="List{T}"/> that starts at index and contains count number of elements, if found; otherwise, –1.</returns>
    /// <exception cref="ArgumentNullException">Parameter <paramref name="comparer"/> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is outside the range of valid indexes for the
    /// <see cref="List{T}"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is less than 0.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> and <paramref name="count"/> do not specify a 
    /// valid section in the <see cref="List{T}"/>.</exception>
    public static int IndexOf<T>(this List<T> source, T item, int index, int count, IEqualityComparer<T> comparer) {
      if(comparer == null) {
        throw new ArgumentNullException("comparer");
      }

      if(index < 0 || (index > 0 && index >= source.Count())) {
        throw new ArgumentOutOfRangeException("index", index, "Index is out of range");
      }

      if(count < 0) {
        throw new ArgumentOutOfRangeException("count", count, "Count cannot be less than zero.");
      }

      if(index + count > source.Count()) {
        throw new ArgumentOutOfRangeException("count", count, "Collection does not contain enough items.");
      }

      var itemIndexes = source.Skip(index).Take(count).Select((t, i) => new { Item = t, Index = i });
      if(itemIndexes.Any(a => comparer.Equals(a.Item, item))) {
        return itemIndexes.First(a => comparer.Equals(a.Item, item)).Index + index;
      }
      else {
        return -1;
      }
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
    #endregion
  }
}
