using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Enkoni.Framework.Collections {
  /// <summary>Provides a generic implementation of <see cref="IComparer{T}"/> that is able to compare two objects based on a specified field name, 
  /// which can even be concatenated using the '.' character. The final field by which the objects must be compared must implement the 
  /// <c>CompareTo</c> method which is defined by the <see cref="IComparable"/> and <see cref="IComparable{T}"/> interfaces.</summary>
  /// <typeparam name="T">The type of object that must be compared.</typeparam>
  public class Comparer<T> : IComparer<T> {
    #region Instance variables
    /// <summary>The field by which the objects must be ordered.</summary>
    private string fieldName;

    /// <summary>The direction of the order.</summary>
    private SortOrder order = SortOrder.Ascending;

    /// <summary>The Func-delegate that does the actual work.</summary>
    private Func<T, T, int> compareDelegate;
    #endregion

    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="Comparer{T}"/> class that compares objects in an ascending order.</summary>
    /// <param name="fieldName">The field that must be used to compare two objects. Use the '.' character to specify a field that is deeper in the
    /// chain of hierarchy.</param>
    public Comparer(string fieldName)
      : this(fieldName, SortOrder.Ascending) {
    }

    /// <summary>Initializes a new instance of the <see cref="Comparer{T}"/> class.</summary>
    /// <param name="fieldName">The field that must be used to compare two objects. Use the '.' character to specify a field that is deeper in the 
    /// chain of hierarchy.</param>
    /// <param name="order">The ordering direction that must be used.</param>
    public Comparer(string fieldName, SortOrder order) {
      Guard.ArgumentIsNotNullOrEmpty(fieldName, nameof(fieldName), "The fieldName cannot be null or empty");

      this.fieldName = fieldName;
      this.order = order;
    }
    #endregion

    #region Methods
    /// <summary>Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.</summary>
    /// <param name="x">The first object to compare.</param>
    /// <param name="y">The second object to compare.</param>
    /// <returns>If <b>x</b> is smaller than <b>y</b>, a value less than zero is returned. If <b>x</b> equals <b>y</b>, zero is returned. If <b>x</b> 
    /// is greater than <b>y</b>, a value greater than zero is returned.<br/>
    /// If the sort-order was set to <see cref="SortOrder.Descending"/>, the results are inverted.</returns>
    public int Compare(T x, T y) {
      Guard.ArgumentIsNotNull(x, nameof(x));
      Guard.ArgumentIsNotNull(y, nameof(y));

      if(this.compareDelegate == null) {
        /* Split the fieldname into the individual parts */
        string[] fieldNames = this.fieldName.Split('.');

        /* Make the parameter-expressions for the left and right parameter */
        ParameterExpression left = Expression.Parameter(typeof(T), "x");
        ParameterExpression right = Expression.Parameter(typeof(T), "y");

        /* Make the expressions that point to the targeted property of the parameter */
        Expression leftProperty = Expression.Property(left, fieldNames[0]);
        Expression rightProperty = Expression.Property(right, fieldNames[0]);
        foreach(string field in fieldNames.Skip(1)) {
          leftProperty = Expression.Property(leftProperty, field);
          rightProperty = Expression.Property(rightProperty, field);
        }

        /* Convert (cast) the property into an 'IComparable' otherwise .NET cannot determine which overload of 
         * 'CompareTo' it must use */
        leftProperty = Expression.Convert(leftProperty, typeof(IComparable));
        rightProperty = Expression.Convert(rightProperty, typeof(IComparable));

        /* Make the expression that will call the 'CompareTo' method */
        Expression resultMethod = Expression.Call(leftProperty, "CompareTo", null, rightProperty);

        if(this.order == SortOrder.Descending) {
          resultMethod = Expression.Multiply(resultMethod, Expression.Constant(-1, typeof(int)));
        }

        /* Compile the expression and store it in the delegate, so that we don't have to go through all the 
         * Expression-stuff again */
        LambdaExpression lambda = Expression.Lambda(resultMethod, left, right);
        this.compareDelegate = (Func<T, T, int>)lambda.Compile();
      }

      /* Execute the delegate and return the value */
      return this.compareDelegate(x, y);
    }
    #endregion
  }
}
