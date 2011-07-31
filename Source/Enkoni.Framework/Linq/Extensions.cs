//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2011. All rights reserved.
// </copyright>
// <summary>
//     Holds numerous extension-methods that extend the standard .NET Linq capabilities.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Enkoni.Framework.Linq {
	/// <summary>This class contains some all-purpose extension-methods.</summary>
	public static class Extensions {
		#region IEnumerable<T> extension methods
		/// <summary>Adds an overload for the Linq-method 'SingleOrDefault' which lets the user define the default value that must be returned when the 
		/// standard 'SingleOrDefault' operation yields no results.</summary>
		/// <typeparam name="T">The type of the elements of source.</typeparam>
		/// <param name="source">An <see cref="IEnumerable{T}"/> to return a single element from.</param>
		/// <param name="predicate">A function to test an element for a condition.</param>
		/// <param name="defaultValue">The default value that must be used.</param>
		/// <returns>The single element of the input sequence that satisfies the condition, or 
		/// <paramref name="defaultValue"/> if no such element is found.</returns>
		public static T SingleOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate, T defaultValue) {
			T result = source.Where(predicate).DefaultIfEmpty(defaultValue).Single();
			return result;
		}

		/// <summary>Adds an overload for the Linq-method 'ElementAtOrDefault' which lets the user define the default value that must be returned when 
		/// the standard 'ElementAtOrDefault' operation yields no results.</summary>
		/// <typeparam name="T">The type of the elements of source.</typeparam>
		/// <param name="source">An <see cref="IEnumerable{T}"/> to return a single element from.</param>
		/// <param name="index">The zero-based index of the element to retrieve.</param>
		/// <param name="defaultValue">The default value that must be used.</param>
		/// <returns><paramref name="defaultValue"/> if the index is outside the bounds of the source sequence; otherwise, the element at the specified 
		/// position in the source sequence.</returns>
		public static T ElementAtOrDefault<T>(this IEnumerable<T> source, int index, T defaultValue) {
			T result = source.ElementAtOrDefault(index);

			if(object.Equals(result, default(T))) {
				return defaultValue;
			}
			else {
				return result;
			}
		}

		/// <summary>Adds an overload for the Linq-method 'FirstOrDefault' which lets the user define the default value that must be returned when the 
		/// standard 'FirstOrDefault' operation yields no results.</summary>
		/// <typeparam name="T">The type of the elements of source.</typeparam>
		/// <param name="source">An <see cref="IEnumerable{T}"/> to return a single element from.</param>
		/// <param name="defaultValue">The default value that must be used.</param>
		/// <returns>The first element of the input sequence, or <paramref name="defaultValue"/> if no such element is found.</returns>
		public static T FirstOrDefault<T>(this IEnumerable<T> source, T defaultValue) {
			T result = source.DefaultIfEmpty(defaultValue).First();
			return result;
		}

		/// <summary>Adds an overload for the Linq-method 'FirstOrDefault' which lets the user define the default value that must be returned when the 
		/// standard 'FirstOrDefault' operation yields no results.</summary>
		/// <typeparam name="T">The type of the elements of source.</typeparam>
		/// <param name="source">An <see cref="IEnumerable{T}"/> to return a single element from.</param>
		/// <param name="predicate">A function to test an element for a condition.</param>
		/// <param name="defaultValue">The default value that must be used.</param>
		/// <returns>The first element of the input sequence that satisfies the condition, or <paramref name="defaultValue"/> if no such element is 
		/// found.</returns>
		public static T FirstOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate, T defaultValue) {
			T result = source.Where(predicate).DefaultIfEmpty(defaultValue).First();
			return result;
		}

		/// <summary>Adds an overload for the Linq-method 'LastOrDefault' which lets the user define the default value that must be returned when the 
		/// standard 'LastOrDefault' operation yields no results.</summary>
		/// <typeparam name="T">The type of the elements of source.</typeparam>
		/// <param name="source">An <see cref="IEnumerable{T}"/> to return a single element from.</param>
		/// <param name="defaultValue">The default value that must be used.</param>
		/// <returns>The last element of the input sequence, or <paramref name="defaultValue"/> if no such element is found.</returns>
		public static T LastOrDefault<T>(this IEnumerable<T> source, T defaultValue) {
			T result = source.DefaultIfEmpty(defaultValue).Last();
			return result;
		}

		/// <summary>Adds an overload for the Linq-method 'LastOrDefault' which lets the user define the default value that must be returned when the 
		/// standard 'LastOrDefault' operation yields no results.</summary>
		/// <typeparam name="T">The type of the elements of source.</typeparam>
		/// <param name="source">An <see cref="IEnumerable{T}"/> to return a single element from.</param>
		/// <param name="predicate">A function to test an element for a condition.</param>
		/// <param name="defaultValue">The default value that must be used.</param>
		/// <returns>The last element of the input sequence that satisfies the condition, or <paramref name="defaultValue"/> if no such element is found.
		/// </returns>
		public static T LastOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate, T defaultValue) {
			T result = source.Where(predicate).DefaultIfEmpty(defaultValue).Last();
			return result;
		}

		/// <summary>Performs an operation on each member in <paramref name="source"/>.</summary>
		/// <typeparam name="T">The type of element that is stored in the enumerable.</typeparam>
		/// <param name="source">An <see cref="IEnumerable{T}"/> to perform the operations on.</param>
		/// <param name="action">The operation that must be performed for each item in the enumerable.</param>
		/// <exception cref="ArgumentNullException">One or more of the parameters are null.</exception>
		public static void ForEach<T>(this IEnumerable<T> source, Action<T> action) {
			if(source == null) {
				throw new ArgumentNullException("source");
			}

			if(action == null) {
				throw new ArgumentNullException("action");
			}

			foreach(T item in source) {
				action(item);
			}
		}

		/// <summary>Sorts the sequence according to the sortspecifications.</summary>
		/// <typeparam name="T">The type of object that must be sorted.</typeparam>
		/// <param name="source">The sequence that must be sorted.</param>
		/// <param name="sortSpecifications">The specifications for the sorting.</param>
		/// <returns>The sorted sequence.</returns>
		public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> source, SortSpecifications<T> sortSpecifications) {
			if(source == null) {
				throw new ArgumentNullException("source", "The IEnumerable-instance is mandatory.");
			}

			if(sortSpecifications == null) {
				return source;
			}
			else {
				return sortSpecifications.Sort(source);
			}
		}

		/// <summary>Creates a new <see cref="IEqualityComparer{T}"/> using the signature of <paramref name="source"/>. The 
		/// comparer will compare two instances of type <typeparamref name="T"/> by evaluating field <paramref name="field"/> of
		/// each instance of <typeparamref name="T"/>.</summary>
		/// <typeparam name="T">The type of objects that must be compared.</typeparam>
		/// <typeparam name="TField">The type of the field that must be used for the comparison.</typeparam>
		/// <param name="source">The collection whose signature will be used to create the equality comparer. The collection does 
		/// not actually have to contain any items.</param>
		/// <param name="field">The function that gives access to the field that must be used in the comparison.</param>
		/// <returns>An <see cref="IEqualityComparer{T}"/> that compares objects of type <typeparamref name="T"/>.</returns>
		public static IEqualityComparer<T> CreateEqualityComparer<T, TField>(this IEnumerable<T> source, Func<T, TField> field) {
			if(field == null) {
				throw new ArgumentNullException("field");
			}

			return new LambdaEqualityComparer<T, TField>(field);
		}
		#endregion

		#region IQueryable<T> extension methods
		/// <summary>Adds an overload for the Linq-method 'SingleOrDefault' which lets the user define the default value that must be returned when the 
		/// standard 'SingleOrDefault' operation yields no results.</summary>
		/// <typeparam name="T">The type of the elements of source.</typeparam>
		/// <param name="source">An <see cref="IQueryable{T}"/> to return a single element from.</param>
		/// <param name="predicate">A function to test an element for a condition.</param>
		/// <param name="defaultValue">The default value that must be used.</param>
		/// <returns>The single element of the input sequence that satisfies the condition, or <paramref name="defaultValue"/> if no such element is 
		/// found.</returns>
		public static T SingleOrDefault<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate, T defaultValue) {
			IQueryable<T> queryResult = source.Where(predicate);
			if(queryResult.Any()) {
				return queryResult.Single();
			}
			else {
				return defaultValue;
			}
		}

		/// <summary>Adds an overload for the Linq-method 'FirstOrDefault' which lets the user define the default value that must be returned when the 
		/// standard 'FirstOrDefault' operation yields no results.</summary>
		/// <typeparam name="T">The type of the elements of source.</typeparam>
		/// <param name="source">An <see cref="IQueryable{T}"/> to return a single element from.</param>
		/// <param name="predicate">A function to test an element for a condition.</param>
		/// <param name="defaultValue">The default value that must be used.</param>
		/// <returns>The first element of the input sequence that satisfies the condition, or <paramref name="defaultValue"/> if no such element is 
		/// found.</returns>
		public static T FirstOrDefault<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate, T defaultValue) {
			IQueryable<T> queryResult = source.Where(predicate);
			if(queryResult.Any()) {
				return queryResult.First();
			}
			else {
				return defaultValue;
			}
		}

		/// <summary>Adds an overload for the Linq-method 'LastOrDefault' which lets the user define the default value that must be returned when the 
		/// standard 'LastOrDefault' operation yields no results.</summary>
		/// <typeparam name="T">The type of the elements of source.</typeparam>
		/// <param name="source">An <see cref="IQueryable{T}"/> to return a single element from.</param>
		/// <param name="predicate">A function to test an element for a condition.</param>
		/// <param name="defaultValue">The default value that must be used.</param>
		/// <returns>The last element of the input sequence that satisfies the condition, or <paramref name="defaultValue"/> if no such element is found.
		/// </returns>
		public static T LastOrDefault<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate, T defaultValue) {
			IQueryable<T> queryResult = source.Where(predicate);
			if(queryResult.Any()) {
				return queryResult.Last();
			}
			else {
				return defaultValue;
			}
		}

		/// <summary>Sorts the sequence according to the sortspecifications.</summary>
		/// <typeparam name="T">The type of object that must be sorted.</typeparam>
		/// <param name="source">The sequence that must be sorted.</param>
		/// <param name="sortSpecifications">The specifications for the sorting.</param>
		/// <returns>The sorted sequence.</returns>
		public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, SortSpecifications<T> sortSpecifications) {
			if(source == null) {
				throw new ArgumentNullException("source", "The IQueryable-instance is mandatory.");
			}

			if(sortSpecifications == null) {
				return source;
			}
			else {
				return sortSpecifications.Sort(source);
			}
		}
		#endregion

		#region Expression<Func<T,bool>> extension methods
		/// <summary>Creates an <see cref="Expression{TDelegate}"/> that inverts the result of <paramref name="source"/>.</summary>
		/// <typeparam name="T">The type that is used as input for the expression.</typeparam>
		/// <param name="source">An <see cref="Expression{TDelegate}"/> whose result must be inverted.</param>
		/// <returns>An expression that will invert the result of <paramref name="source"/>.</returns>
		public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> source) {
			if(source == null) {
				throw new ArgumentNullException("source");
			}

			return Expression.Lambda<Func<T, bool>>(Expression.Not(source.Body), source.Parameters[0]);
		}
		#endregion
	}
}
