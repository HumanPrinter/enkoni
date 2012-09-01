//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="CircularStack.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//     Defines a stack that is ciculair.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Enkoni.Framework {
  /// <summary>Represents a fixed size last-in-first-out (LIFO) collection of instances of the same arbitrary type that uses a circulair collection
  /// as internal storage.</summary>
  /// <typeparam name="T">Specifies the type of elements in the stack.</typeparam>
  [Serializable]
  [DebuggerDisplay("Count = {Count}")]
  public class CircularStack<T> : IEnumerable<T>, ICollection, IEnumerable {
    #region Instance variables
    /// <summary>The actual storage that contains the 'stack' items. The virtual index of each item is used as key.</summary>
    private Dictionary<int, T> storage;

    /// <summary>The index of the last added item. In other words the index of the item that is returned by the Pop or Peek operation.</summary>
    private int currentIndex = -1;

    /// <summary>The version that is used to determine if the stack is modified.</summary>
    private int version;
    #endregion

    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="CircularStack{T}"/> class using an unlimited maximum size and the default capacity.
    /// </summary>
    public CircularStack()
      : this(-1) {
    }

    /// <summary>Initializes a new instance of the <see cref="CircularStack{T}"/> class using a maximum size.</summary>
    /// <param name="maximumSize">The number of items this stack can contain at most. Once this number has been reached, the oldest elements will be 
    /// overwritten. Use <c>-1</c> to use an unlimited maximum size.</param>
    /// <exception cref="ArgumentOutOfRangeException">The maximum size is not set to a valid value.</exception>
    public CircularStack(int maximumSize) {
      if(maximumSize != -1 && maximumSize <= 0) {
        throw new ArgumentOutOfRangeException("maximumSize", maximumSize, "Only positive integers or '-1' are allowed as valid input.");
      }

      this.MaximumSize = maximumSize;
      if(maximumSize == -1) {
        this.storage = new Dictionary<int, T>();
      }
      else {
        this.storage = new Dictionary<int, T>(maximumSize);
      }
    }

    /// <summary>Initializes a new instance of the <see cref="CircularStack{T}"/> class that contains elements copied from the specified collection
    /// and has sufficient capacity to accommodate the number of elements copied.</summary>
    /// <param name="collection">The collection whose elements are copied to the new stack.</param>
    /// <exception cref="ArgumentNullException">The specified collection is <see langword="null"/>.</exception>
    public CircularStack(IEnumerable<T> collection)
      : this(collection, -1) {
    }

    /// <summary>Initializes a new instance of the <see cref="CircularStack{T}"/> class that contains elements copied from the specified collection
    /// and has a maximum size.</summary>
    /// <param name="collection">The collection whose elements are copied to the new stack.</param>
    /// <param name="maximumSize">The number of items this stack can contain at most. Once this number has been reached, the oldest elements will be 
    /// overwritten. Use <c>-1</c> to use an unlimited maximum size.</param>
    /// <exception cref="ArgumentNullException">The specified collection is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException">The maximum size is not set to a valid value.</exception>
    /// <exception cref="ArgumentException">The specified collection exceeds the specified maximum size.</exception>
    public CircularStack(IEnumerable<T> collection, int maximumSize) {
      if(collection == null) {
        throw new ArgumentNullException("collection", "collection is null.");
      }

      if(maximumSize != -1 && maximumSize <= 0) {
        throw new ArgumentOutOfRangeException("maximumSize", maximumSize, "Only positive integers or '-1' are allowed as valid input.");
      }

      if(maximumSize != -1 && maximumSize < collection.Count()) {
        throw new ArgumentException("The size of the initial collection cannot exceed the maximum size of the circulair stack.");
      }

      this.MaximumSize = maximumSize;
      this.storage = collection.Select((item, index) => new { Index = index, Item = item }).ToDictionary(a => a.Index, a => a.Item);
      this.currentIndex = this.storage.Count - 1;
    }
    #endregion

    #region Explicit implementation of ICollection properties
    /// <summary>Gets a value indicating whether access to the <see cref="ICollection"/> is synchronized (thread safe).</summary>
    bool ICollection.IsSynchronized {
      get { return this.IsSynchronized; }
    }

    /// <summary>Gets an object that can be used to synchronize access to the <see cref="ICollection"/>.</summary>
    object ICollection.SyncRoot {
      get { return this.SyncRoot; }
    }
    #endregion

    #region Properties
    /// <summary>Gets the number of elements actually contained in the <see cref="CircularStack{T}"/>.</summary>
    public int Count {
      get {
        /* The following situations are possible:
         * - No items have yet been added -> return the count of the storage.
         * - The maximum size is unlimited, so the storage will never roll over -> return the count of the storage.
         * - The count of the storage equals the maximum size, so the storage is about to roll over or has already rolled over -> return the count of the storage.
         * - The maximum size has not yet been reached -> return the count of the storage.
         */
        return this.storage.Count;
      }
    }

    /// <summary>Gets the maximum size of this <see cref="CircularStack{T}"/>. The value <c>-1</c> indicates an unlimited maximum size.</summary>
    public int MaximumSize { get; private set; }

    /// <summary>Gets a value indicating whether access to the <see cref="CircularStack{T}"/> is synchronized (thread safe).</summary>
    protected virtual bool IsSynchronized {
      get { return false; }
    }

    /// <summary>Gets an object that can be used to synchronize access to the <see cref="CircularStack{T}"/>.</summary>
    protected virtual object SyncRoot {
      get { return this; }
    }
    #endregion

    #region Explicit implementation of IEnumerable<T> methods
    /// <summary>Returns an enumerator that iterates through the collection.</summary>
    /// <returns>A <see cref="IEnumerator{T}"/> that can be used to iterate through the collection.</returns>
    IEnumerator<T> IEnumerable<T>.GetEnumerator() {
      return this.GetEnumerator();
    }
    #endregion

    #region Explicit implementation of IEnumerable methods
    /// <summary>Returns an enumerator that iterates through a collection.</summary>
    /// <returns>An <see cref="IEnumerator"/> object that can be used to iterate through the collection.</returns>
    IEnumerator IEnumerable.GetEnumerator() {
      return this.GetEnumerator();
    }
    #endregion

    #region Explicit implementation of ICollection methods
    /// <summary>Copies the elements of the <see cref="ICollection"/> to an <see cref="Array"/>, starting at a particular <see cref="Array"/> index.
    /// </summary>
    /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the elements copied from <see cref="ICollection"/>. 
    /// The <see cref="Array"/> must have zero-based indexing.</param>
    /// <param name="index">The zero-based index in <paramref name="array"/> at which copying begins.</param>
    /// <exception cref="ArgumentNullException"><paramref name="array"/> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than zero.</exception>
    /// <exception cref="ArgumentException"><paramref name="array"/> is multidimensional.<br/>
    /// -or- <paramref name="index"/> is equal to or greater than the length of <paramref name="array"/>.<br/>
    /// -or- The number of elements in the source <see cref="ICollection"/> is greater than the available space from index to the end of the 
    /// destination array.</exception>
    void ICollection.CopyTo(Array array, int index) {
      if(array == null) {
        throw new ArgumentNullException("array", "The destination array cannot be null.");
      }

      if(index < 0) {
        throw new ArgumentOutOfRangeException("index", index, "index cannot be less than zero.");
      }

      if(array.Rank > 1) {
        throw new ArgumentException("Multi-dimensional arrays are not supported.");
      }

      if(index >= array.Length) {
        throw new ArgumentException("The index matches or exceeds the length of the array.");
      }

      if(this.Count > array.Length - index) {
        throw new ArgumentException("Not enoughspace in the destination array.");
      }

      this.CopyTo(array, index);
    }
    #endregion

    #region Public methods
    /// <summary>Removes all objects from the <see cref="CircularStack{T}"/>.</summary>
    public void Clear() {
      this.ClearCore();
      ++this.version;
    }

    /// <summary>Determines whether an element is in the <see cref="CircularStack{T}"/>.</summary>
    /// <param name="item">The object to locate in the <see cref="CircularStack{T}"/>. The value can be <see langword="null"/> for reference types.
    /// </param>
    /// <returns><see langword="true"/> if item is found in the <see cref="CircularStack{T}"/>; otherwise, <see langword="false"/>.</returns>
    public bool Contains(T item) {
      return this.ContainsCore(item);
    }

    /// <summary>Copies the <see cref="CircularStack{T}"/> to an existing one-dimensional <see cref="Array"/>, starting at the specified array index.
    /// </summary>
    /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the elements copied from 
    /// <see cref="CircularStack{T}"/>. The <see cref="Array"/> must have zero-based indexing.</param>
    /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
    /// <exception cref="ArgumentNullException"><paramref name="array"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is less than zero.</exception>
    /// <exception cref="ArgumentException"><paramref name="arrayIndex"/> is equal to or greater than the length of array.<br/>
    /// -or- The number of elements in the source <see cref="CircularStack{T}"/> is greater than the available space from 
    /// <paramref name="arrayIndex"/> to the end of the destination array.</exception>
    public void CopyTo(T[] array, int arrayIndex) {
      if(array == null) {
        throw new ArgumentNullException("array", "The destination array cannot be null.");
      }

      if(arrayIndex < 0) {
        throw new ArgumentOutOfRangeException("arrayIndex", arrayIndex, "arrayIndex cannot be less than zero.");
      }

      if(arrayIndex >= array.Length) {
        throw new ArgumentException("The arrayIndex matches or exceeds the length of the array.");
      }

      if(this.Count > array.Length - arrayIndex) {
        throw new ArgumentException("Not enoughspace in the destination array.");
      }

      this.CopyToCore(array, arrayIndex);
    }

    /// <summary>Returns an enumerator for the <see cref="CircularStack{T}"/>.</summary>
    /// <returns>An <see cref="CircularStack{T}.Enumerator"/> for the <see cref="CircularStack{T}"/>.</returns>
    public CircularStack<T>.Enumerator GetEnumerator() {
      return this.GetEnumeratorCore();
    }

    /// <summary>Returns the object at the top of the <see cref="CircularStack{T}"/> without removing it.</summary>
    /// <returns>The object at the top of the <see cref="CircularStack{T}"/>.</returns>
    /// <exception cref="InvalidOperationException">The <see cref="CircularStack{T}"/> is empty.</exception>
    public T Peek() {
      if(this.currentIndex == -1) {
        throw new InvalidOperationException("The stack is empty.");
      }

      return this.PeekCore();
    }

    /// <summary>Removes and returns the object at the top of the <see cref="CircularStack{T}"/>.</summary>
    /// <returns>The object removed from the top of the <see cref="CircularStack{T}"/>.</returns>
    /// <exception cref="InvalidOperationException">The <see cref="CircularStack{T}"/> is empty.</exception>
    public T Pop() {
      if(this.currentIndex == -1) {
        throw new InvalidOperationException("The stack is empty.");
      }

      T poppedItem = this.PopCore();
      ++this.version;
      return poppedItem;
    }

    /// <summary>Inserts an object at the top of the <see cref="CircularStack{T}"/>. If the current size of the <see cref="CircularStack{T}"/> has
    /// reached the maximum size, <paramref name="item"/> will overwrite the first or oldest item in the <see cref="CircularStack{T}"/>.</summary>
    /// <param name="item">The object to push onto the <see cref="CircularStack{T}"/>. The value can be <see langword="null"/> for reference types.
    /// </param>
    public void Push(T item) {
      this.PushCore(item);
      ++this.version;
    }

    /// <summary>Copies the <see cref="CircularStack{T}"/> to a new array.</summary>
    /// <returns>A new array containing copies of the elements of the <see cref="CircularStack{T}"/>.</returns>
    public T[] ToArray() {
      return this.ToArrayCore();
    }

    /// <summary>Sets the capacity to the actual number of elements in the <see cref="CircularStack{T}"/>, if that number is less than 90 percent of 
    /// current capacity.</summary>
    public void TrimExcess() {
      this.TrimExcessCore();
    }
    #endregion

    #region Extensibility methods
    /// <summary>Removes all objects from the <see cref="CircularStack{T}"/>.</summary>
    protected virtual void ClearCore() {
      this.storage.Clear();
      this.currentIndex = -1;
    }

    /// <summary>Determines whether an element is in the <see cref="CircularStack{T}"/>.</summary>
    /// <param name="item">The object to locate in the <see cref="CircularStack{T}"/>. The value can be <see langword="null"/> for reference types.
    /// </param>
    /// <returns><see langword="true"/> if item is found in the <see cref="CircularStack{T}"/>; otherwise, <see langword="false"/>.</returns>
    protected virtual bool ContainsCore(T item) {
      /* All the required parameter validation is done by the List<T> class. */
      return this.storage.Values.Contains(item);
    }

    /// <summary>Copies the <see cref="CircularStack{T}"/> to an existing one-dimensional <see cref="Array"/>, starting at the specified array index.
    /// </summary>
    /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the elements copied from 
    /// <see cref="CircularStack{T}"/>. The <see cref="Array"/> must have zero-based indexing.</param>
    /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
    protected virtual void CopyToCore(T[] array, int arrayIndex) {
      if(this.storage.Count == 0) {
        /* If the storage is empty, there is nothing to do */
        return;
      }

      T[] stackAsArray = this.ToArrayCore();
      for(int stackIndex = 0; stackIndex < stackAsArray.Length; ++stackIndex) {
        array.SetValue(stackAsArray[stackIndex], stackIndex + arrayIndex);
      }
    }

    /// <summary>Returns an enumerator for the <see cref="CircularStack{T}"/>.</summary>
    /// <returns>An <see cref="CircularStack{T}.Enumerator"/> for the <see cref="CircularStack{T}"/>.</returns>
    protected virtual CircularStack<T>.Enumerator GetEnumeratorCore() {
      return new CircularStack<T>.Enumerator(this);
    }

    /// <summary>Returns the object at the top of the <see cref="CircularStack{T}"/> without removing it.</summary>
    /// <returns>The object at the top of the <see cref="CircularStack{T}"/>.</returns>
    protected virtual T PeekCore() {
      return this.storage[this.currentIndex];
    }

    /// <summary>Removes and returns the object at the top of the <see cref="CircularStack{T}"/>.</summary>
    /// <returns>The object removed from the top of the <see cref="CircularStack{T}"/>.</returns>
    protected virtual T PopCore() {
      /* Get the item at the current index */
      T poppedItem = this.storage[this.currentIndex];
      /* Remove the item from the storage */
      this.storage.Remove(this.currentIndex);
      /* Decrease the index */
      --this.currentIndex;

      /* If the index is negative, move to the end of the storage  */
      if(this.currentIndex == -1) {
        this.currentIndex = this.MaximumSize - 1;
      }

      /* If there are no more items left in the storage, set the index to a 'special' value */
      if(this.storage.Count == 0) {
        this.currentIndex = -1;
      }

      return poppedItem;
    }

    /// <summary>Inserts an object at the top of the <see cref="CircularStack{T}"/>. If the current size of the <see cref="CircularStack{T}"/> has
    /// reached the maximum size, <paramref name="item"/> will overwrite the first or oldest item in the <see cref="CircularStack{T}"/>.</summary>
    /// <param name="item">The object to push onto the <see cref="CircularStack{T}"/>. The value can be <see langword="null"/> for reference types.
    /// </param>
    protected virtual void PushCore(T item) {
      ++this.currentIndex;
      if(this.MaximumSize == -1) {
        /* The maximum size is unlimited, simply add the item to the storage */
        this.storage.Add(this.currentIndex, item);
        return;
      }

      /* There is a maximum size */
      if(this.currentIndex == this.MaximumSize) {
        /* The maximum has been reached. Roll over by setting the index back to zero */
        this.currentIndex = 0;
      }

      this.storage[this.currentIndex] = item;
    }

    /// <summary>Copies the <see cref="CircularStack{T}"/> to a new array.</summary>
    /// <returns>A new array containing copies of the elements of the <see cref="CircularStack{T}"/>.</returns>
    protected virtual T[] ToArrayCore() {
      if(this.storage.Count == 0) {
        /* If the storage is empty, return an empty array */
        return new T[0];
      }

      /* Create the array that will be returned */
      T[] createdArray = new T[this.storage.Count];

      /*      no maximum size             maximum not yet reached              last item at the end of the storage   */
      if(this.MaximumSize == -1 || this.storage.Count < this.MaximumSize || this.currentIndex == this.MaximumSize - 1) {
        int copyIndex = this.currentIndex;
        int arrayIndex = 0;
        while(copyIndex >= 0) {
          createdArray[arrayIndex++] = this.storage[copyIndex--];
        }
      }
      else {
        /* First copy from current index back to zero... */
        int copyIndex = this.currentIndex;
        int arrayIndex = 0;
        while(copyIndex >= 0) {
          createdArray[arrayIndex++] = this.storage[copyIndex--];
        }

        /* ...then copy from maximum back to current index */
        copyIndex = this.MaximumSize - 1;
        while(copyIndex > this.currentIndex) {
          createdArray[arrayIndex++] = this.storage[copyIndex--];
        }
      }

      return createdArray;
    }

    /// <summary>Sets the capacity to the actual number of elements in the <see cref="CircularStack{T}"/>, if that number is less than 90 percent of 
    /// current capacity.</summary>
    protected virtual void TrimExcessCore() {
      /* Because a dictionary is used, there never is any excess */
    }

    /// <summary>Copies the elements of the <see cref="ICollection"/> to an <see cref="Array"/>, starting at a particular <see cref="Array"/> index.
    /// </summary>
    /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the elements copied from <see cref="ICollection"/>. 
    /// The <see cref="Array"/> must have zero-based indexing.</param>
    /// <param name="index">The zero-based index in <paramref name="array"/> at which copying begins.</param>
    protected virtual void CopyTo(Array array, int index) {
      if(this.storage.Count == 0) {
        /* If the storage is empty, there is nothing to do */
        return;
      }

      T[] stackAsArray = this.ToArrayCore();
      for(int stackIndex = 0; stackIndex < stackAsArray.Length; ++stackIndex) {
        array.SetValue(stackAsArray[stackIndex], stackIndex + index);
      }
    }
    #endregion

    #region Structs
    /// <summary>Enumerates the elements of a <see cref="CircularStack{T}"/>.</summary>
    public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable {
      #region Private members
      /// <summary>The collection that is enumerated.</summary>
      private CircularStack<T> enumeratorSource;

      /// <summary>The index at which the enumerator is currenly looking.</summary>
      private int enumeratorIndex;

      /// <summary>The version of the stack at the time the enumerator was created.</summary>
      private int stackVersion;

      /// <summary>The index at which the enumerator started the look.</summary>
      private int startIndex;

      /// <summary>Indicates if the enumerator has already enumerated over the first item.</summary>
      private bool isPassedFirstItem;
      #endregion

      #region Constructors
      /// <summary>Initializes a new instance of the <see cref="Enumerator"/> struct.</summary>
      /// <param name="enumeratorSource">The collection that must be enumerated.</param>
      internal Enumerator(CircularStack<T> enumeratorSource) {
        this.enumeratorSource = enumeratorSource;
        /* '-1' means 'not yet initialized' */
        this.enumeratorIndex = -1;
        this.startIndex = -1;
        this.stackVersion = enumeratorSource.version;
        this.isPassedFirstItem = false;
      }
      #endregion

      #region Properties
      /// <summary>Gets the element at the current position of the enumerator.</summary>
      /// <exception cref="InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last 
      /// element.</exception>
      public T Current {
        get {
          if(this.enumeratorSource == null) {
            throw new InvalidOperationException("The enumerator source is not set. The enumerator must only be created through the GetEnumerator method");
          }

          if(this.enumeratorIndex == -1) {
            /* The first MoveNext() has not yet been invoked or has returned 'false' the first time */
            throw new InvalidOperationException("The enumerator is positioned before the first available item.");
          }
          else if(this.enumeratorIndex >= this.enumeratorSource.storage.Count) {
            /* The last MoveNext() moved the cursor passed the last available item */
            throw new InvalidOperationException("The enumerator is positioned after the last available item.");
          }
          else if(this.isPassedFirstItem && this.enumeratorIndex == this.startIndex) {
            /* If the first item has been passed (the cursor points at the second item or further) and the cursor is back at its starting position, the round-trip is complete */
            throw new InvalidOperationException("The enumerator is positioned after the last available item.");
          }

          return this.enumeratorSource.storage[this.enumeratorIndex];
        }
      }
      #endregion

      #region Explicit implementation of IEnumerator properties
      /// <summary>Gets the current element in the collection.</summary>
      /// <exception cref="InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last 
      /// element.</exception>
      object IEnumerator.Current {
        get { return this.Current; }
      }
      #endregion

      #region Methods
      /// <summary>Advances the enumerator to the next element of the <see cref="CircularStack{T}"/>.</summary>
      /// <returns><see langword="true"/> if the enumerator was successfully advanced to the next element; <see langword="false"/> if the enumerator 
      /// has passed the end of the collection.</returns>
      /// <exception cref="InvalidOperationException">The collection was modified after the enumerator was created.</exception>
      public bool MoveNext() {
        /* Perform a version check to see if something has changed in the meantime */
        if(this.stackVersion != this.enumeratorSource.version) {
          throw new InvalidOperationException("The collection has been modified since the enumerator has been created.");
        }

        /* If the stack is empty, there is not much to do */
        if(this.enumeratorSource.Count == 0) {
          return false;
        }

        bool firstRun = false;

        if(this.enumeratorIndex == -1) {
          /* The cursor has not yet been initialized. Do some administartive work... */
          /* ...initialize the cursor... */
          this.enumeratorIndex = this.enumeratorSource.currentIndex;
          /* ...mark the start position of the cursor to detect roundtrips... */
          this.startIndex = this.enumeratorSource.currentIndex;
          /* ...indicate that this is the first time that MoveNext is invoked */
          firstRun = true;
        }
        else {
          /* This is at least the second time MoveNext() is invoked, simply move the cursor */
          this.isPassedFirstItem = true;
          --this.enumeratorIndex;
        }

        if(this.enumeratorSource.MaximumSize == -1) {
          /* If there is no maximum size, no fancy roundtrip detection is required */
          return this.enumeratorIndex >= 0;
        }
        else {
          if(this.enumeratorIndex == -1) {
            /* If the cursor is before index '0', determine if it is possible to make a roundtrip */
            if(this.startIndex == this.enumeratorSource.Count - 1) {
              /* The cursor started at the end of the stack (the stack has not yet rolled over), so no roundtrip is performed */
              return false;
            }
            else {
              /* Begin the roundtrip */
              this.enumeratorIndex = this.enumeratorSource.MaximumSize - 1;
              return true;
            }
          }
          else {
            /* determine if the cursor is back at the current index of the stack (where it all began) */
            return firstRun || this.enumeratorIndex != this.enumeratorSource.currentIndex;
          }
        }
      }

      /// <summary>Releases all resources used by the <see cref="CircularStack{T}.Enumerator"/>.</summary>
      public void Dispose() {
        /* There is nothing to dispose */
      }
      #endregion

      #region Explicit implementation of IEnumerator methods
      /// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
      void IEnumerator.Reset() {
        this.enumeratorIndex = -1;
        this.isPassedFirstItem = false;
      }

      /// <summary>Advances the enumerator to the next element of the collection.</summary>
      /// <returns><see langword="true"/> if the enumerator was successfully advanced to the next element; <see langword="false"/> if the enumerator 
      /// has passed the end of the collection.</returns>
      /// <exception cref="InvalidOperationException">The collection was modified after the enumerator was created.</exception>
      bool IEnumerator.MoveNext() {
        return this.MoveNext();
      }
      #endregion
    }
    #endregion
  }
}
