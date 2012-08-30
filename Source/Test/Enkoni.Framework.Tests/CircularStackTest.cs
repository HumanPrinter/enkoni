//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="CircularStackTest.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//     Contains testcases that test the functionality of the CircularStack-class.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Tests {
  /// <summary>This class defines the testcases that test the functionality of the <see cref="CircularStack{T}"/> class.</summary>
  [TestClass]
  public class CircularStackTest {
    /// <summary>Tests the functionality of the <see cref="CircularStack{T}"/> class for normal use when using an unlimited maximum size.</summary>
    [TestMethod]
    public void TestCase01_NoMaximumSizeNoInitialContent_NormalUse() {
      /* Create the test subject */
      CircularStack<string> testSubject = new CircularStack<string>();

      /* Test the initial values of the test subject */
      Assert.AreEqual(0, testSubject.Count);
      Assert.AreEqual(-1, testSubject.MaximumSize);

      /* Test the return-value of the Contains-method */
      Assert.IsFalse(testSubject.Contains("C"));

      /* Push items in the test subject and test the return-value of the Peek-method after each addition */
      char stackItem = 'A';
      for(int i = 0; i < 10; ++i) {
        testSubject.Push(stackItem.ToString(CultureInfo.InvariantCulture));
        Assert.AreEqual(stackItem.ToString(CultureInfo.InvariantCulture), testSubject.Peek(), false);
        ++stackItem;
      }

      /* Test the return-values of the Count-property and Contains-method */
      Assert.AreEqual(10, testSubject.Count);

      Assert.IsTrue(testSubject.Contains("C"));
      Assert.IsFalse(testSubject.Contains("X"));

      /* Pop items from the test subject test the return-values */
      for(int i = 0; i < 10; ++i) {
        string expected = (--stackItem).ToString(CultureInfo.InvariantCulture);
        string peekItem = testSubject.Peek();
        string popItem = testSubject.Pop();

        Assert.AreSame(peekItem, popItem);
        Assert.AreEqual(expected, peekItem, false);
        Assert.AreEqual(expected, popItem, false);
      }

      Assert.AreEqual(0, testSubject.Count);

      /* Re-populate the test subject to test the Clear-method */
      stackItem = 'A';
      for(int i = 0; i < 10; ++i) {
        testSubject.Push(stackItem.ToString(CultureInfo.InvariantCulture));
        ++stackItem;
      }

      testSubject.Clear();
      Assert.AreEqual(0, testSubject.Count);

      /* Make sure that Clearing an empty collection works. */
      testSubject.Clear();
      Assert.AreEqual(0, testSubject.Count);
    }

    /// <summary>Tests the functionality of the <see cref="CircularStack{T}"/> class for normal use when using an unlimited maximum size and an
    /// initial content.</summary>
    [TestMethod]
    public void TestCase02_NoMaximumSizeWithInitialContent_NormalUse() {
      /* Create initial content */
      string[] initialContent = new string[] { "0", "1", "2", "3", "4" };
      /* Create the test subject */
      CircularStack<string> testSubject = new CircularStack<string>(initialContent);

      /* Test the initial values of the test subject */
      Assert.AreEqual(5, testSubject.Count);
      Assert.AreEqual(-1, testSubject.MaximumSize);

      /* Test the return-value of the Contains-method */
      Assert.IsFalse(testSubject.Contains("C"));

      /* Test if the initial values can be retrieved correctly */
      string expected = "4";
      string peekItem = testSubject.Peek();
      string popItem = testSubject.Pop();

      Assert.AreSame(peekItem, popItem);
      Assert.AreEqual(expected, peekItem, false);
      Assert.AreEqual(expected, popItem, false);
      Assert.AreEqual(4, testSubject.Count);

      /* Push items in the test subject and test the return-value of the Peek-method after each addition */
      char stackItem = 'A';
      for(int i = 0; i < 10; ++i) {
        testSubject.Push(stackItem.ToString(CultureInfo.InvariantCulture));
        Assert.AreEqual(stackItem.ToString(CultureInfo.InvariantCulture), testSubject.Peek(), false);
        ++stackItem;
      }

      /* Test the return-values of the Count-property and Contains-method */
      Assert.AreEqual(14, testSubject.Count);

      Assert.IsTrue(testSubject.Contains("C"));
      Assert.IsTrue(testSubject.Contains("2"));
      Assert.IsFalse(testSubject.Contains("X"));

      /* Pop items from the test subject test the return-values */
      for(int i = 0; i < 10; ++i) {
        expected = (--stackItem).ToString(CultureInfo.InvariantCulture);
        peekItem = testSubject.Peek();
        popItem = testSubject.Pop();

        Assert.AreSame(peekItem, popItem);
        Assert.AreEqual(expected, peekItem, false);
        Assert.AreEqual(expected, popItem, false);
      }

      Assert.AreEqual(4, testSubject.Count);

      testSubject.Clear();
      Assert.AreEqual(0, testSubject.Count);

      /* Make sure that Clearing an empty collection works. */
      testSubject.Clear();
      Assert.AreEqual(0, testSubject.Count);
    }

    /// <summary>Tests the functionality of the <see cref="M:CircularStack{T}.Peek()"/> and <see cref="M:CircularStack{T}.Pop()"/> methods when 
    /// invoked on an empty stack.</summary>
    [TestMethod]
    public void TestCase03_NoMaximumSize_Exceptions() {
      /* Create the test subject */
      CircularStack<string> testSubject;

      /* Test the constructors when passing invalid parameters */
      try {
        testSubject = new CircularStack<string>(null);
        Assert.Fail("Constructor CircularStack(IEnumerable<string>) did not throw a System.ArgumentNullException");
      }
      catch(ArgumentNullException ex) {
        Assert.AreEqual("collection", ex.ParamName);
      }

      testSubject = new CircularStack<string>();

      try {
        string peekedValue = testSubject.Peek();
        Assert.Fail("Peek() did not throw an System.InvalidOperationException");
      }
      catch(InvalidOperationException) {
      }

      try {
        string poppedValue = testSubject.Pop();
        Assert.Fail("Peek() did not throw an System.InvalidOperationException");
      }
      catch(InvalidOperationException) {
      }
    }

    /// <summary>Tests the functionality of the <see cref="CircularStack{T}"/> class for normal use when using a maximum size.</summary>
    [TestMethod]
    public void TestCase04_WithMaximumSizeNoInitialContent_NormalUse() {
      /* Create the test subject */
      CircularStack<string> testSubject = new CircularStack<string>(8);

      /* Test the initial values of the test subject */
      Assert.AreEqual(0, testSubject.Count);
      Assert.AreEqual(8, testSubject.MaximumSize);

      /* Test the return-value of the Contains-method */
      Assert.IsFalse(testSubject.Contains("C"));

      /* Push items in the test subject without causing a rollover and test the return-value of the Peek-method after each addition */
      char stackItem = 'A';
      for(int i = 0; i < 6; ++i) {
        testSubject.Push(stackItem.ToString(CultureInfo.InvariantCulture));
        Assert.AreEqual(stackItem.ToString(CultureInfo.InvariantCulture), testSubject.Peek(), false);
        ++stackItem;
      }

      /* Test the return-values of the Count-property and Contains-method */
      Assert.AreEqual(6, testSubject.Count);

      Assert.IsTrue(testSubject.Contains("C"));
      Assert.IsFalse(testSubject.Contains("X"));

      /* Pop items from the test subject test the return-values */
      for(int i = 0; i < 6; ++i) {
        string expected = (--stackItem).ToString(CultureInfo.InvariantCulture);
        string peekItem = testSubject.Peek();
        string popItem = testSubject.Pop();

        Assert.AreSame(peekItem, popItem);
        Assert.AreEqual(expected, peekItem, false);
        Assert.AreEqual(expected, popItem, false);
      }

      Assert.AreEqual(0, testSubject.Count);

      /* Re-populate the test subject with the maximum amount of items */
      stackItem = 'A';
      for(int i = 0; i < 8; ++i) {
        testSubject.Push(stackItem.ToString(CultureInfo.InvariantCulture));
        ++stackItem;
      }

      /* Test the return-values of the Count-property and Contains-method */
      Assert.AreEqual(8, testSubject.Count);

      Assert.IsTrue(testSubject.Contains("F"));
      Assert.IsFalse(testSubject.Contains("X"));

      /* Pop items from the test subject test the return-values */
      for(int i = 0; i < 8; ++i) {
        string expected = (--stackItem).ToString(CultureInfo.InvariantCulture);
        string peekItem = testSubject.Peek();
        string popItem = testSubject.Pop();

        Assert.AreSame(peekItem, popItem);
        Assert.AreEqual(expected, peekItem, false);
        Assert.AreEqual(expected, popItem, false);
      }

      Assert.AreEqual(0, testSubject.Count);

      /* Re-populate the test subject with more then the maximum amount of items */
      stackItem = 'A';
      for(int i = 0; i < 10; ++i) {
        testSubject.Push(stackItem.ToString(CultureInfo.InvariantCulture));
        ++stackItem;
      }

      /* Test the return-values of the Count-property and Contains-method */
      Assert.AreEqual(8, testSubject.Count);

      Assert.IsTrue(testSubject.Contains("F"));
      Assert.IsFalse(testSubject.Contains("X"));
      Assert.IsFalse(testSubject.Contains("A"));

      /* Pop items from the test subject test the return-values */
      for(int i = 0; i < 8; ++i) {
        string expected = (--stackItem).ToString(CultureInfo.InvariantCulture);
        string peekItem = testSubject.Peek();
        string popItem = testSubject.Pop();

        Assert.AreSame(peekItem, popItem);
        Assert.AreEqual(expected, peekItem, false);
        Assert.AreEqual(expected, popItem, false);
      }

      Assert.AreEqual(0, testSubject.Count);
    }

    /// <summary>Tests the functionality of the <see cref="CircularStack{T}"/> class for normal use when using an unlimited maximum size.</summary>
    [TestMethod]
    public void TestCase05_WithMaximumSizeWithInitialContent_NormalUse() {
      /* Create initial content */
      string[] initialContent = new string[] { "0", "1", "2", "3", "4" };
      /* Create the test subject */
      CircularStack<string> testSubject = new CircularStack<string>(initialContent, 8);

      /* Test the initial values of the test subject */
      Assert.AreEqual(5, testSubject.Count);
      Assert.AreEqual(8, testSubject.MaximumSize);

      /* Test the return-value of the Contains-method */
      Assert.IsFalse(testSubject.Contains("C"));

      /* Test if the initial values can be retrieved correctly */
      string expected = "4";
      string peekItem = testSubject.Peek();
      string popItem = testSubject.Pop();

      Assert.AreSame(peekItem, popItem);
      Assert.AreEqual(expected, peekItem, false);
      Assert.AreEqual(expected, popItem, false);
      Assert.AreEqual(4, testSubject.Count);

      /* Push items in the test subject without causing a rollover and test the return-value of the Peek-method after each addition */
      char stackItem = 'A';
      for(int i = 0; i < 3; ++i) {
        testSubject.Push(stackItem.ToString(CultureInfo.InvariantCulture));
        Assert.AreEqual(stackItem.ToString(CultureInfo.InvariantCulture), testSubject.Peek(), false);
        ++stackItem;
      }

      /* Test the return-values of the Count-property and Contains-method */
      Assert.AreEqual(7, testSubject.Count);

      Assert.IsTrue(testSubject.Contains("C"));
      Assert.IsTrue(testSubject.Contains("2"));
      Assert.IsFalse(testSubject.Contains("X"));

      /* Pop items from the test subject test the return-values */
      for(int i = 0; i < 3; ++i) {
        expected = (--stackItem).ToString(CultureInfo.InvariantCulture);
        peekItem = testSubject.Peek();
        popItem = testSubject.Pop();

        Assert.AreSame(peekItem, popItem);
        Assert.AreEqual(expected, peekItem, false);
        Assert.AreEqual(expected, popItem, false);
      }

      Assert.AreEqual(4, testSubject.Count);

      /* Re-populate the test subject with the maximum amount of items */
      stackItem = 'A';
      for(int i = 0; i < 4; ++i) {
        testSubject.Push(stackItem.ToString(CultureInfo.InvariantCulture));
        Assert.AreEqual(stackItem.ToString(CultureInfo.InvariantCulture), testSubject.Peek(), false);
        ++stackItem;
      }

      /* Test the return-values of the Count-property and Contains-method */
      Assert.AreEqual(8, testSubject.Count);

      Assert.IsTrue(testSubject.Contains("C"));
      Assert.IsTrue(testSubject.Contains("2"));
      Assert.IsFalse(testSubject.Contains("X"));

      /* Pop items from the test subject test the return-values */
      for(int i = 0; i < 4; ++i) {
        expected = (--stackItem).ToString(CultureInfo.InvariantCulture);
        peekItem = testSubject.Peek();
        popItem = testSubject.Pop();

        Assert.AreSame(peekItem, popItem);
        Assert.AreEqual(expected, peekItem, false);
        Assert.AreEqual(expected, popItem, false);
      }

      Assert.AreEqual(4, testSubject.Count);

      /* Re-populate the test subject with more then the maximum amount of items */
      stackItem = 'A';
      for(int i = 0; i < 6; ++i) {
        testSubject.Push(stackItem.ToString(CultureInfo.InvariantCulture));
        Assert.AreEqual(stackItem.ToString(CultureInfo.InvariantCulture), testSubject.Peek(), false);
        ++stackItem;
      }

      /* Test the return-values of the Count-property and Contains-method */
      Assert.AreEqual(8, testSubject.Count);

      Assert.IsTrue(testSubject.Contains("C"));
      Assert.IsFalse(testSubject.Contains("1"));
      Assert.IsFalse(testSubject.Contains("X"));

      /* Pop items from the test subject test the return-values */
      for(int i = 0; i < 6; ++i) {
        expected = (--stackItem).ToString(CultureInfo.InvariantCulture);
        peekItem = testSubject.Peek();
        popItem = testSubject.Pop();

        Assert.AreSame(peekItem, popItem);
        Assert.AreEqual(expected, peekItem, false);
        Assert.AreEqual(expected, popItem, false);
      }

      Assert.AreEqual(2, testSubject.Count);
    }

    /// <summary>Tests the functionality of the <see cref="CircularStack{T}"/> class for normal use when using an unlimited maximum size.</summary>
    [TestMethod]
    public void TestCase06_WithMaximumSize_Exceptions() {
      /* Create the test subject */
      CircularStack<string> testSubject;

      /* Test the constructors when passing invalid parameters */
      try {
        testSubject = new CircularStack<string>(null, 8);
        Assert.Fail("Constructor CircularStack(IEnumerable<string>, int) did not throw a System.ArgumentNullException");
      }
      catch(ArgumentNullException ex) {
        Assert.AreEqual("collection", ex.ParamName);
      }

      string[] initialArray = new string[10] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
      try {
        testSubject = new CircularStack<string>(initialArray, -5);
        Assert.Fail("Constructor CircularStack(IEnumerable<string>, int) did not throw a System.ArgumentOutOfRangeException");
      }
      catch(ArgumentOutOfRangeException ex) {
        Assert.AreEqual("maximumSize", ex.ParamName);
      }

      try {
        testSubject = new CircularStack<string>(initialArray, 0);
        Assert.Fail("Constructor CircularStack(IEnumerable<string>, int) did not throw a System.ArgumentOutOfRangeException");
      }
      catch(ArgumentOutOfRangeException ex) {
        Assert.AreEqual("maximumSize", ex.ParamName);
      }

      try {
        testSubject = new CircularStack<string>(initialArray, 8);
        Assert.Fail("Constructor CircularStack(IEnumerable<string>, int) did not throw a System.ArgumentException");
      }
      catch(ArgumentException ex) {
        Assert.AreEqual(typeof(ArgumentException), ex.GetType());
      }

      testSubject = new CircularStack<string>(10);

      try {
        string peekedValue = testSubject.Peek();
        Assert.Fail("Peek() did not throw an System.InvalidOperationException");
      }
      catch(InvalidOperationException) {
      }

      try {
        string poppedValue = testSubject.Pop();
        Assert.Fail("Peek() did not throw an System.InvalidOperationException");
      }
      catch(InvalidOperationException) {
      }
    }

    /// <summary>Tests the functionality of the <see cref="M:CircularStack{T}.ToArray()"/> method.</summary>
    [TestMethod]
    public void TestCase07_NoMaximumSize_ToArray() {
      /* Create the test subject */
      CircularStack<string> testSubject = new CircularStack<string>();

      /* Test the ToArray functionality when the test subject is empty */
      string[] stackAsArray = testSubject.ToArray();
      Assert.IsNotNull(stackAsArray);
      Assert.AreEqual(0, stackAsArray.Length);

      /* Push items in the test subject and test the return-value of the Peek-method after each addition */
      char stackItem = 'A';
      for(int i = 0; i < 10; ++i) {
        testSubject.Push(stackItem.ToString(CultureInfo.InvariantCulture));
        ++stackItem;
      }

      /* Test the ToArray functionality when the test subject is filled */
      stackAsArray = testSubject.ToArray();
      Assert.IsNotNull(stackAsArray);
      Assert.AreEqual(10, stackAsArray.Length);
    }

    /// <summary>Tests the functionality of the <see cref="M:CircularStack{T}.CopyTo(T[],int)"/> method.</summary>
    [TestMethod]
    public void TestCase08_NoMaximumSize_CopyToBasic() {
      /* Create the test subject */
      CircularStack<string> testSubject = new CircularStack<string>();

      /* Test the CopyTo functionality when the test subject is empty */
      string[] testArray = new string[5];
      testSubject.CopyTo(testArray, 0);
      Assert.IsNotNull(testArray);
      Assert.AreEqual(5, testArray.Length);
      Assert.IsTrue(testArray.All(str => str == null));

      testArray = new string[5];
      testSubject.CopyTo(testArray, 2);
      Assert.IsNotNull(testArray);
      Assert.AreEqual(5, testArray.Length);
      Assert.IsTrue(testArray.All(str => str == null));

      testArray = new string[5];
      testSubject.CopyTo(testArray, 4);
      Assert.IsNotNull(testArray);
      Assert.AreEqual(5, testArray.Length);
      Assert.IsTrue(testArray.All(str => str == null));

      /* Push items in the test subject and test the return-value of the Peek-method after each addition */
      char stackItem = 'A';
      for(int i = 0; i < 10; ++i) {
        testSubject.Push(stackItem.ToString(CultureInfo.InvariantCulture));
        ++stackItem;
      }

      /* Test the CopyTo functionality when the test subject is filled */
      testArray = new string[10];
      testSubject.CopyTo(testArray, 0);
      Assert.IsNotNull(testArray);
      Assert.AreEqual(10, testArray.Length);
      Assert.AreEqual("JIHGFEDCBA", string.Join(string.Empty, testArray), false);

      testArray = new string[14];
      testArray[0] = "Y";
      testArray[1] = "Z";
      testArray[12] = "V";
      testArray[13] = "W";

      testSubject.CopyTo(testArray, 2);
      Assert.IsNotNull(testArray);
      Assert.AreEqual(14, testArray.Length);
      Assert.AreEqual("YZJIHGFEDCBAVW", string.Join(string.Empty, testArray), false);

      testArray = new string[12];
      testArray[0] = "Y";
      testArray[1] = "Z";
      testSubject.CopyTo(testArray, 2);
      Assert.IsNotNull(testArray);
      Assert.AreEqual(12, testArray.Length);
      Assert.AreEqual("YZJIHGFEDCBA", string.Join(string.Empty, testArray), false);
    }

    /// <summary>Tests the functionality of the <see cref="M:ICollection.CopyTo(Array,int)"/> method.</summary>
    [TestMethod]
    public void TestCase09_NoMaximumSize_CopyToICollection() {
      /* Create the test subject */
      CircularStack<string> testSubject = new CircularStack<string>();

      /* Test the CopyTo functionality when the test subject is empty */
      Array testArray2 = Array.CreateInstance(typeof(string), 5);
      ((ICollection)testSubject).CopyTo(testArray2, 0);
      Assert.IsNotNull(testArray2);
      Assert.AreEqual(5, testArray2.Length);
      foreach(object item in testArray2) {
        Assert.IsNull(item);
      }

      testArray2 = Array.CreateInstance(typeof(string), 5);
      ((ICollection)testSubject).CopyTo(testArray2, 2);
      Assert.IsNotNull(testArray2);
      Assert.AreEqual(5, testArray2.Length);
      foreach(object item in testArray2) {
        Assert.IsNull(item);
      }

      testArray2 = Array.CreateInstance(typeof(string), 5);
      ((ICollection)testSubject).CopyTo(testArray2, 4);
      Assert.IsNotNull(testArray2);
      Assert.AreEqual(5, testArray2.Length);
      foreach(object item in testArray2) {
        Assert.IsNull(item);
      }

      /* Push items in the test subject and test the return-value of the Peek-method after each addition */
      char stackItem = 'A';
      for(int i = 0; i < 10; ++i) {
        testSubject.Push(stackItem.ToString(CultureInfo.InvariantCulture));
        ++stackItem;
      }

      /* Test the CopyTo functionality when the test subject is filled */
      testArray2 = Array.CreateInstance(typeof(string), 10);
      ((ICollection)testSubject).CopyTo(testArray2, 0);
      Assert.IsNotNull(testArray2);
      Assert.AreEqual(10, testArray2.Length);
      StringBuilder builder = new StringBuilder();
      foreach(object item in testArray2) {
        builder.Append(item);
      }

      Assert.AreEqual("JIHGFEDCBA", builder.ToString(), false);

      testArray2 = Array.CreateInstance(typeof(string), 14);
      testArray2.SetValue("Y", 0);
      testArray2.SetValue("Z", 1);
      testArray2.SetValue("V", 12);
      testArray2.SetValue("W", 13);
      ((ICollection)testSubject).CopyTo(testArray2, 2);
      Assert.IsNotNull(testArray2);
      Assert.AreEqual(14, testArray2.Length);
      builder = new StringBuilder();
      foreach(object item in testArray2) {
        builder.Append(item);
      }

      Assert.AreEqual("YZJIHGFEDCBAVW", builder.ToString(), false);

      testArray2 = Array.CreateInstance(typeof(string), 12);
      testArray2.SetValue("Y", 0);
      testArray2.SetValue("Z", 1);
      ((ICollection)testSubject).CopyTo(testArray2, 2);
      Assert.IsNotNull(testArray2);
      Assert.AreEqual(12, testArray2.Length);
      builder = new StringBuilder();
      foreach(object item in testArray2) {
        builder.Append(item);
      }

      Assert.AreEqual("YZJIHGFEDCBA", builder.ToString(), false);
    }

    /// <summary>Tests the functionality of the <see cref="M:CircularStack{T}.CopyTo(T[],int)"/> method when passing invalid parameters.</summary>
    [TestMethod]
    public void TestCase10_NoMaximumSize_CopyToBasic_Exceptions() {
      /* Create the test subject */
      CircularStack<string> testSubject = new CircularStack<string>();

      /* Push items in the test subject and test the return-value of the Peek-method after each addition */
      char stackItem = 'A';
      for(int i = 0; i < 10; ++i) {
        testSubject.Push(stackItem.ToString(CultureInfo.InvariantCulture));
        ++stackItem;
      }

      string[] testArray = new string[5];

      /* Test the CopyTo functionality when passing a null-reference for the first parameter */
      try {
        testSubject.CopyTo(null, 0);
        Assert.Fail("CopyTo(T[], int) did not throw an System.ArgumentNullException");
      }
      catch(ArgumentNullException ex) {
        Assert.AreEqual("array", ex.ParamName);
      }

      /* Test the CopyTo functionality when passing a negative integer for the second parameter */
      try {
        testSubject.CopyTo(testArray, -2);
        Assert.Fail("CopyTo(T[], int) did not throw an System.ArgumentOutOfRangeException");
      }
      catch(ArgumentOutOfRangeException ex) {
        Assert.AreEqual("arrayIndex", ex.ParamName);
      }

      /* Test the CopyTo functionality when passing a index outside the range of the array */
      try {
        testSubject.CopyTo(testArray, 5);
        Assert.Fail("CopyTo(T[], int) did not throw an System.ArgumentException");
      }
      catch(ArgumentException ex) {
        Assert.AreEqual(typeof(ArgumentException), ex.GetType());
      }

      /* Test the CopyTo functionality when passing an array that is too small */
      try {
        testSubject.CopyTo(testArray, 0);
        Assert.Fail("CopyTo(T[], int) did not throw an System.ArgumentException");
      }
      catch(ArgumentException ex) {
        Assert.AreEqual(typeof(ArgumentException), ex.GetType());
      }

      testArray = new string[15];
      /* Test the CopyTo functionality when passing an array with a start index that does not leave enough room in the array */
      try {
        testSubject.CopyTo(testArray, 6);
        Assert.Fail("CopyTo(T[], int) did not throw an System.ArgumentException");
      }
      catch(ArgumentException ex) {
        Assert.AreEqual(typeof(ArgumentException), ex.GetType());
      }
    }

    /// <summary>Tests the functionality of the <see cref="M:CircularStack{T}.CopyTo(Array,int)"/> method when passing invalid parameters.</summary>
    [TestMethod]
    public void TestCase11_NoMaximumSize_CopyToICollection_Exceptions() {
      /* Create the test subject */
      CircularStack<string> testSubject = new CircularStack<string>();

      /* Push items in the test subject and test the return-value of the Peek-method after each addition */
      char stackItem = 'A';
      for(int i = 0; i < 10; ++i) {
        testSubject.Push(stackItem.ToString(CultureInfo.InvariantCulture));
        ++stackItem;
      }

      Array testArray = Array.CreateInstance(typeof(string), 5);

      /* Test the CopyTo functionality when passing a null-reference for the first parameter */
      try {
        ((ICollection)testSubject).CopyTo(null, 0);
        Assert.Fail("CopyTo(System.Array, int) did not throw an System.ArgumentNullException");
      }
      catch(ArgumentNullException ex) {
        Assert.AreEqual("array", ex.ParamName);
      }

      /* Test the CopyTo functionality when passing a negative integer for the second parameter */
      try {
        ((ICollection)testSubject).CopyTo(testArray, -2);
        Assert.Fail("CopyTo(System.Array, int) did not throw an System.ArgumentOutOfRangeException");
      }
      catch(ArgumentOutOfRangeException ex) {
        Assert.AreEqual("index", ex.ParamName);
      }

      /* Test the CopyTo functionality when passing a index outside the range of the array */
      try {
        ((ICollection)testSubject).CopyTo(testArray, 5);
        Assert.Fail("CopyTo(System.Array, int) did not throw an System.ArgumentException");
      }
      catch(ArgumentException ex) {
        Assert.AreEqual(typeof(ArgumentException), ex.GetType());
      }

      /* Test the CopyTo functionality when passing an array that is too small */
      try {
        ((ICollection)testSubject).CopyTo(testArray, 0);
        Assert.Fail("CopyTo(System.Array, int) did not throw an System.ArgumentException");
      }
      catch(ArgumentException ex) {
        Assert.AreEqual(typeof(ArgumentException), ex.GetType());
      }

      testArray = Array.CreateInstance(typeof(string), 15);
      /* Test the CopyTo functionality when passing an array with a start index that does not leave enough room in the array */
      try {
        ((ICollection)testSubject).CopyTo(testArray, 6);
        Assert.Fail("CopyTo(System.Array, int) did not throw an System.ArgumentException");
      }
      catch(ArgumentException ex) {
        Assert.AreEqual(typeof(ArgumentException), ex.GetType());
      }

      testArray = Array.CreateInstance(typeof(string), 15, 2, 3);
      /* Test the CopyTo functionality when passing a multi-dimensional array */
      try {
        ((ICollection)testSubject).CopyTo(testArray, 2);
        Assert.Fail("CopyTo(System.Array, int) did not throw an System.ArgumentException");
      }
      catch(ArgumentException ex) {
        Assert.AreEqual(typeof(ArgumentException), ex.GetType());
      }
    }

    /// <summary>Tests the functionality of the <see cref="CircularStack{T}.Enumerator"/> struct for normal use when using an unlimited maximum size.
    /// </summary>
    [TestMethod]
    public void TestCase12_NoMaximumSize_StackEnumerator_NormalUse() {
      CircularStack<string> stack = new CircularStack<string>();
      CircularStack<string>.Enumerator testSubject = stack.GetEnumerator();

      /* Test the functionality of the enumerator when the stack is empty */
      while(testSubject.MoveNext()) {
        Assert.Fail("CircularStack<T>.Enumerator.MoveNext() did not return 'false'");
      }

      /* Push items in the test subject and test the enumerator */
      char stackItem = 'A';
      for(int i = 0; i < 10; ++i) {
        stack.Push(stackItem.ToString(CultureInfo.InvariantCulture));
        ++stackItem;
      }

      /* Test the enumerator when the stack is filled */
      testSubject = stack.GetEnumerator();

      int loopCount = 0;
      char initialChar = (char)('A' + 9);
      while(testSubject.MoveNext()) {
        object item = testSubject.Current;
        string expected = ((char)(initialChar - loopCount)).ToString(CultureInfo.InvariantCulture);
        Assert.IsInstanceOfType(item, typeof(string));
        Assert.AreEqual(expected, (string)item);

        ++loopCount;
      }

      Assert.AreEqual(10, loopCount);
    }

    /// <summary>Tests the functionality of the <see cref="CircularStack{T}.Enumerator"/> struct for normal use when using an unlimited maximum size.
    /// </summary>
    [TestMethod]
    public void TestCase13_NoMaximumSize_IEnumerator_NormalUse() {
      CircularStack<string> stack = new CircularStack<string>();
      IEnumerator testSubject = ((ICollection)stack).GetEnumerator();

      /* Test the functionality of the enumerator when the stack is empty */
      while(testSubject.MoveNext()) {
        Assert.Fail("CircularStack<T>.Enumerator.MoveNext() did not return 'false'");
      }

      /* Push items in the test subject and test the enumerator */
      char stackItem = 'A';
      for(int i = 0; i < 10; ++i) {
        stack.Push(stackItem.ToString(CultureInfo.InvariantCulture));
        ++stackItem;
      }

      /* Test the enumerator when the stack is filled */
      testSubject = ((ICollection)stack).GetEnumerator();

      int loopCount = 0;
      char initialChar = (char)('A' + 9);
      while(testSubject.MoveNext()) {
        object item = testSubject.Current;
        string expected = ((char)(initialChar - loopCount)).ToString(CultureInfo.InvariantCulture);
        Assert.IsInstanceOfType(item, typeof(string));
        Assert.AreEqual(expected, (string)item);

        ++loopCount;
        if(loopCount == 6) {
          break;
        }
      }

      Assert.AreEqual(6, loopCount);

      testSubject.Reset();
      loopCount = 0;
      while(testSubject.MoveNext()) {
        object item = testSubject.Current;
        string expected = ((char)(initialChar - loopCount)).ToString(CultureInfo.InvariantCulture);
        Assert.IsInstanceOfType(item, typeof(string));
        Assert.AreEqual(expected, (string)item);

        ++loopCount;
      }

      Assert.AreEqual(10, loopCount);
    }

    /// <summary>Tests the functionality of the <see cref="CircularStack{T}.Enumerator"/> struct for normal use when using an unlimited maximum size.
    /// </summary>
    [TestMethod]
    public void TestCase14_NoMaximumSize_ForEach_NormalUse() {
      CircularStack<string> stack = new CircularStack<string>();

      /* Test the functionality of the enumerator when the stack is empty */
      foreach(string item in stack) {
        Assert.Fail("CircularStack<T>.Enumerator.MoveNext() did not return 'false'");
      }

      /* Push items in the test subject and test the enumerator */
      char stackItem = 'A';
      for(int i = 0; i < 10; ++i) {
        stack.Push(stackItem.ToString(CultureInfo.InvariantCulture));
        ++stackItem;
      }

      /* Test the enumerator when the stack is filled */
      int loopCount = 0;
      char initialChar = (char)('A' + 9);
      foreach(string item in stack) {
        Assert.AreEqual(initialChar.ToString(CultureInfo.InvariantCulture), item);
        --initialChar;
        ++loopCount;
      }

      Assert.AreEqual(10, loopCount);
    }

    /// <summary>Tests the functionality of the <see cref="CircularStack{T}.Enumerator"/> struct during invalid operations when using an unlimited 
    /// maximum size.</summary>
    [TestMethod]
    public void TestCase15_NoMaximumSize_Enumerator_Exceptions() {
      /* Create a stack to create the enumerator from */
      CircularStack<string> stack = new CircularStack<string>();
      char stackItem = 'A';
      for(int i = 0; i < 10; ++i) {
        stack.Push(stackItem.ToString(CultureInfo.InvariantCulture));
        ++stackItem;
      }

      /* Create the test subject */
      CircularStack<string>.Enumerator testSubject = stack.GetEnumerator();

      /* Make sure an exception is thrown when retrieving the current value before invoking MoveNext() */
      try {
        string item = testSubject.Current;
        Assert.Fail("CircularStack<T>.Enumerator.Current did not throw the expected InvalidperationException");
      }
      catch(InvalidOperationException) {
      }

      /* Move all the way to the end */
      while(testSubject.MoveNext()) {
      }

      /* Make sure an exception is thrown when retrieving the current value after the last invocation of MoveNext() */
      try {
        string item = testSubject.Current;
        Assert.Fail("CircularStack<T>.Enumerator.Current did not throw the expected InvalidperationException");
      }
      catch(InvalidOperationException) {
      }

      /* Dispose the test subject... */
      testSubject.Dispose();

      /* ...and create a new one */
      testSubject = stack.GetEnumerator();
      testSubject.MoveNext();
      testSubject.MoveNext();
      stack.Push("Z");

      /* Make sure an exception is thrown when the collection has been modified... */
      try {
        testSubject.MoveNext();
        Assert.Fail("CircularStack<T>.Enumerator.MoveNext() did not throw the expected InvalidperationException");
      }
      catch(InvalidOperationException) {
      }

      /* ...and the same when using a foreach-loop */
      try {
        int loopCount = 0;
        foreach(string item in stack) {
          ++loopCount;
          if(loopCount == 3) {
            stack.Push("Y");
          }
        }

        Assert.Fail("CircularStack<T>.Enumerator.MoveNext() did not throw the expected InvalidperationException");
      }
      catch(InvalidOperationException) {
      }
    }

    /// <summary>Tests the functionality of the <see cref="CircularStack{T}.Enumerator"/> struct for normal use when using a limited maximum size.
    /// </summary>
    [TestMethod]
    public void TestCase16_WithMaximumSize_StackEnumerator_NormalUse() {
      CircularStack<string> stack = new CircularStack<string>(8);
      CircularStack<string>.Enumerator testSubject = stack.GetEnumerator();

      /* Test the functionality of the enumerator when the stack is empty */
      while(testSubject.MoveNext()) {
        Assert.Fail("CircularStack<T>.Enumerator.MoveNext() did not return 'false'");
      }

      /* Push items in the test subject and test the enumerator */
      char stackItem = 'A';
      for(int i = 0; i < 6; ++i) {
        stack.Push(stackItem.ToString(CultureInfo.InvariantCulture));
        ++stackItem;
      }

      /* Test the enumerator when the stack is partially filled */
      testSubject = stack.GetEnumerator();

      int loopCount = 0;
      char initialChar = (char)('A' + 5);
      while(testSubject.MoveNext()) {
        string item = testSubject.Current;
        string expected = ((char)(initialChar - loopCount)).ToString(CultureInfo.InvariantCulture);
        Assert.AreEqual(expected, (string)item);

        ++loopCount;
      }

      Assert.AreEqual(6, loopCount);

      /* Fill the stack to precisly the maximum size */
      for(int i = 0; i < 2; ++i) {
        stack.Push(stackItem.ToString(CultureInfo.InvariantCulture));
        ++stackItem;
      }

      /* Test the enumerator when the stack is precisely filled */
      testSubject = stack.GetEnumerator();

      loopCount = 0;
      initialChar = (char)('A' + 7);
      while(testSubject.MoveNext()) {
        string item = testSubject.Current;
        string expected = ((char)(initialChar - loopCount)).ToString(CultureInfo.InvariantCulture);
        Assert.AreEqual(expected, (string)item);

        ++loopCount;
      }

      Assert.AreEqual(8, loopCount);

      /* Fill the stack even more to cause an roll-over */
      for(int i = 0; i < 4; ++i) {
        stack.Push(stackItem.ToString(CultureInfo.InvariantCulture));
        ++stackItem;
      }

      /* Test the enumerator when the stack is rolled over */
      testSubject = stack.GetEnumerator();

      loopCount = 0;
      initialChar = (char)('A' + 11);
      while(testSubject.MoveNext()) {
        string item = testSubject.Current;
        string expected = ((char)(initialChar - loopCount)).ToString(CultureInfo.InvariantCulture);
        Assert.AreEqual(expected, (string)item);

        ++loopCount;
      }

      Assert.AreEqual(8, loopCount);
    }

    /// <summary>Tests the functionality of the <see cref="CircularStack{T}.Enumerator"/> struct for normal use when using a limited maximum size.
    /// </summary>
    [TestMethod]
    public void TestCase17_WithMaximumSize_IEnumerator_NormalUse() {
      CircularStack<string> stack = new CircularStack<string>(8);
      IEnumerator testSubject = ((ICollection)stack).GetEnumerator();

      /* Test the functionality of the enumerator when the stack is empty */
      while(testSubject.MoveNext()) {
        Assert.Fail("CircularStack<T>.Enumerator.MoveNext() did not return 'false'");
      }

      /* Push items in the test subject and test the enumerator */
      char stackItem = 'A';
      for(int i = 0; i < 6; ++i) {
        stack.Push(stackItem.ToString(CultureInfo.InvariantCulture));
        ++stackItem;
      }

      /* Test the enumerator when the stack is partially filled */
      testSubject = stack.GetEnumerator();

      int loopCount = 0;
      char initialChar = (char)('A' + 5);
      while(testSubject.MoveNext()) {
        object item = testSubject.Current;
        string expected = ((char)(initialChar - loopCount)).ToString(CultureInfo.InvariantCulture);
        Assert.IsInstanceOfType(item, typeof(string));
        Assert.AreEqual(expected, (string)item);

        ++loopCount;
        if(loopCount == 4) {
          break;
        }
      }

      Assert.AreEqual(4, loopCount);

      testSubject.Reset();
      loopCount = 0;
      while(testSubject.MoveNext()) {
        object item = testSubject.Current;
        string expected = ((char)(initialChar - loopCount)).ToString(CultureInfo.InvariantCulture);
        Assert.IsInstanceOfType(item, typeof(string));
        Assert.AreEqual(expected, (string)item);

        ++loopCount;
      }

      Assert.AreEqual(6, loopCount);

      /* Fill the stack to precisly the maximum size */
      for(int i = 0; i < 2; ++i) {
        stack.Push(stackItem.ToString(CultureInfo.InvariantCulture));
        ++stackItem;
      }

      /* Test the enumerator when the stack is precisely filled */
      testSubject = stack.GetEnumerator();

      loopCount = 0;
      initialChar = (char)('A' + 7);
      while(testSubject.MoveNext()) {
        object item = testSubject.Current;
        string expected = ((char)(initialChar - loopCount)).ToString(CultureInfo.InvariantCulture);
        Assert.IsInstanceOfType(item, typeof(string));
        Assert.AreEqual(expected, (string)item);

        ++loopCount;
        if(loopCount == 6) {
          break;
        }
      }

      Assert.AreEqual(6, loopCount);

      testSubject.Reset();
      loopCount = 0;
      while(testSubject.MoveNext()) {
        object item = testSubject.Current;
        string expected = ((char)(initialChar - loopCount)).ToString(CultureInfo.InvariantCulture);
        Assert.IsInstanceOfType(item, typeof(string));
        Assert.AreEqual(expected, (string)item);

        ++loopCount;
      }

      Assert.AreEqual(8, loopCount);

      /* Fill the stack even more to cause an roll-over */
      for(int i = 0; i < 4; ++i) {
        stack.Push(stackItem.ToString(CultureInfo.InvariantCulture));
        ++stackItem;
      }

      /* Test the enumerator when the stack is rolled over */
      testSubject = stack.GetEnumerator();

      loopCount = 0;
      initialChar = (char)('A' + 11);
      while(testSubject.MoveNext()) {
        object item = testSubject.Current;
        string expected = ((char)(initialChar - loopCount)).ToString(CultureInfo.InvariantCulture);
        Assert.IsInstanceOfType(item, typeof(string));
        Assert.AreEqual(expected, (string)item);

        ++loopCount;
        if(loopCount == 6) {
          break;
        }
      }

      Assert.AreEqual(6, loopCount);

      testSubject.Reset();
      loopCount = 0;
      while(testSubject.MoveNext()) {
        object item = testSubject.Current;
        string expected = ((char)(initialChar - loopCount)).ToString(CultureInfo.InvariantCulture);
        Assert.IsInstanceOfType(item, typeof(string));
        Assert.AreEqual(expected, (string)item);

        ++loopCount;
      }

      Assert.AreEqual(8, loopCount);
    }

    /// <summary>Tests the functionality of the <see cref="CircularStack{T}.Enumerator"/> struct for normal use when using a limited maximum size.
    /// </summary>
    [TestMethod]
    public void TestCase18_WithMaximumSize_ForEach_NormalUse() {
      CircularStack<string> stack = new CircularStack<string>(8);

      /* Test the functionality of the enumerator when the stack is empty */
      foreach(string item in stack) {
        Assert.Fail("CircularStack<T>.Enumerator.MoveNext() did not return 'false'");
      }

      /* Push items in the test subject and test the enumerator */
      char stackItem = 'A';
      for(int i = 0; i < 6; ++i) {
        stack.Push(stackItem.ToString(CultureInfo.InvariantCulture));
        ++stackItem;
      }

      int loopCount = 0;
      char initialChar = (char)('A' + 5);
      foreach(string item in stack) {
        string expected = ((char)(initialChar - loopCount)).ToString(CultureInfo.InvariantCulture);
        Assert.AreEqual(expected, (string)item);

        ++loopCount;
      }

      Assert.AreEqual(6, loopCount);

      /* Fill the stack to precisly the maximum size */
      for(int i = 0; i < 2; ++i) {
        stack.Push(stackItem.ToString(CultureInfo.InvariantCulture));
        ++stackItem;
      }

      /* Test the enumerator when the stack is precisely filled */
      loopCount = 0;
      initialChar = (char)('A' + 7);
      foreach(string item in stack) {
        string expected = ((char)(initialChar - loopCount)).ToString(CultureInfo.InvariantCulture);
        Assert.AreEqual(expected, (string)item);

        ++loopCount;
      }

      Assert.AreEqual(8, loopCount);

      /* Fill the stack even more to cause an roll-over */
      for(int i = 0; i < 4; ++i) {
        stack.Push(stackItem.ToString(CultureInfo.InvariantCulture));
        ++stackItem;
      }

      /* Test the enumerator when the stack is rolled over */
      loopCount = 0;
      initialChar = (char)('A' + 11);
      foreach(string item in stack) {
        string expected = ((char)(initialChar - loopCount)).ToString(CultureInfo.InvariantCulture);
        Assert.AreEqual(expected, (string)item);

        ++loopCount;
      }

      Assert.AreEqual(8, loopCount);
    }

    /// <summary>Tests the functionality of the <see cref="CircularStack{T}.Enumerator"/> struct during invalid operations when using an fixed 
    /// maximum size.</summary>
    [TestMethod]
    public void TestCase19_WithMaximumSize_Enumerator_Exceptions() {
      /* Create a stack to create the enumerator from */
      CircularStack<string> stack = new CircularStack<string>(8);
      char stackItem = 'A';
      for(int i = 0; i < 10; ++i) {
        stack.Push(stackItem.ToString(CultureInfo.InvariantCulture));
        ++stackItem;
      }

      /* Create the test subject */
      CircularStack<string>.Enumerator testSubject = stack.GetEnumerator();

      /* Make sure an exception is thrown when retrieving the current value before invoking MoveNext() */
      try {
        string item = testSubject.Current;
        Assert.Fail("CircularStack<T>.Enumerator.Current did not throw the expected InvalidperationException");
      }
      catch(InvalidOperationException) {
      }

      /* Move all the way to the end */
      while(testSubject.MoveNext()) {
      }

      /* Make sure an exception is thrown when retrieving the current value after the last invocation of MoveNext() */
      try {
        string item = testSubject.Current;
        Assert.Fail("CircularStack<T>.Enumerator.Current did not throw the expected InvalidperationException");
      }
      catch(InvalidOperationException) {
      }

      /* Dispose the test subject... */
      testSubject.Dispose();

      /* ...and create a new one */
      testSubject = stack.GetEnumerator();
      testSubject.MoveNext();
      testSubject.MoveNext();
      stack.Push("Z");

      /* Make sure an exception is thrown when the collection has been modified... */
      try {
        testSubject.MoveNext();
        Assert.Fail("CircularStack<T>.Enumerator.MoveNext() did not throw the expected InvalidperationException");
      }
      catch(InvalidOperationException) {
      }

      /* ...and the same when using a foreach-loop */
      try {
        int loopCount = 0;
        foreach(string item in stack) {
          ++loopCount;
          if(loopCount == 3) {
            stack.Push("Y");
          }
        }

        Assert.Fail("CircularStack<T>.Enumerator.MoveNext() did not throw the expected InvalidperationException");
      }
      catch(InvalidOperationException) {
      }
    }
  }
}
