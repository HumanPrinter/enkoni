//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExtensionTest.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//     Contains testcases that test the functionality of the extension methods for the string-class.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System.Globalization;
using System.Threading;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Tests {
  /// <summary>This class defines the testcases that test the functionality of the extension methods for the string-class.</summary>
  [TestClass]
  public class StringExtensionTest {
    /// <summary>Tests the functionality of the <see cref="Extensions.Capitalize(string, bool)"/> extension method.</summary>
    [TestMethod]
    public void StringExtensions_CapitalizeNoCultureKeepCapitals() {
      Thread.CurrentThread.CurrentCulture = new CultureInfo("nl-NL");

      string startValue = "hello world and goodmorning";
      string result = startValue.Capitalize(true);
      Assert.AreEqual("Hello World And Goodmorning", result, false);

      startValue = "hello World And goodmorning";
      result = startValue.Capitalize(true);
      Assert.AreEqual("Hello World And Goodmorning", result, false);

      startValue = "hello woRld aNd goodmorning";
      result = startValue.Capitalize(true);
      Assert.AreEqual("Hello WoRld ANd Goodmorning", result, false);

      startValue = string.Empty;
      result = startValue.Capitalize(true);
      Assert.AreEqual(string.Empty, result, false);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.Capitalize(string)"/> extension method.</summary>
    [TestMethod]
    public void StringExtensions_CapitalizeNoCultureResetCapitals() {
      Thread.CurrentThread.CurrentCulture = new CultureInfo("nl-NL");

      string startValue = "hello world and goodmorning";
      string result = startValue.Capitalize();
      Assert.AreEqual("Hello World And Goodmorning", result, false);

      startValue = "hello World And goodmorning";
      result = startValue.Capitalize();
      Assert.AreEqual("Hello World And Goodmorning", result, false);

      startValue = "hello woRld aNd goodmorning";
      result = startValue.Capitalize();
      Assert.AreEqual("Hello World And Goodmorning", result, false);

      startValue = string.Empty;
      result = startValue.Capitalize();
      Assert.AreEqual(string.Empty, result, false);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.Capitalize(string, bool, CultureInfo)"/> extension method.</summary>
    [TestMethod]
    public void StringExtensions_CapitalizeWithCultureKeepCapitals() {
      /* Set the current culture so a different value to make sure that the testsubject uses the CultureInfo parameter */
      Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

      CultureInfo culture = new CultureInfo("nl-NL");

      string startValue = "hello world and goodmorning";
      string result = startValue.Capitalize(true, culture);
      Assert.AreEqual("Hello World And Goodmorning", result, false);

      startValue = "hello World And goodmorning";
      result = startValue.Capitalize(true, culture);
      Assert.AreEqual("Hello World And Goodmorning", result, false);

      startValue = "hello woRld aNd goodmorning";
      result = startValue.Capitalize(true, culture);
      Assert.AreEqual("Hello WoRld ANd Goodmorning", result, false);

      startValue = string.Empty;
      result = startValue.Capitalize(true, culture);
      Assert.AreEqual(string.Empty, result, false);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.Capitalize(string, CultureInfo)"/> extension method.</summary>
    [TestMethod]
    public void StringExtensions_CapitalizeWithCultureResetCapitals() {
      /* Set the current culture so a different value to make sure that the testsubject uses the CultureInfo parameter */
      Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

      CultureInfo culture = new CultureInfo("nl-NL");

      string startValue = "hello world and goodmorning";
      string result = startValue.Capitalize(culture);
      Assert.AreEqual("Hello World And Goodmorning", result, false);

      startValue = "hello World And goodmorning";
      result = startValue.Capitalize(culture);
      Assert.AreEqual("Hello World And Goodmorning", result, false);

      startValue = "hello woRld aNd goodmorning";
      result = startValue.Capitalize(culture);
      Assert.AreEqual("Hello World And Goodmorning", result, false);

      startValue = string.Empty;
      result = startValue.Capitalize(culture);
      Assert.AreEqual(string.Empty, result, false);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CapitalizeSentence(string, bool)"/> extension method.</summary>
    [TestMethod]
    public void StringExtensions_CapitalizeSentenceNoCultureKeepCapitals() {
      Thread.CurrentThread.CurrentCulture = new CultureInfo("nl-NL");

      string startValue = "hello world and goodmorning";
      string result = startValue.CapitalizeSentence(true);
      Assert.AreEqual("Hello world and goodmorning", result, false);
        
      startValue = "hello World And goodmorning";
      result = startValue.CapitalizeSentence(true);
      Assert.AreEqual("Hello World And goodmorning", result, false);

      startValue = "hello woRld aNd goodmorning";
      result = startValue.CapitalizeSentence(true);
      Assert.AreEqual("Hello woRld aNd goodmorning", result, false);

      startValue = string.Empty;
      result = startValue.CapitalizeSentence(true);
      Assert.AreEqual(string.Empty, result, false);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CapitalizeSentence(string)"/> extension method.</summary>
    [TestMethod]
    public void StringExtensions_CapitalizeSentenceNoCultureResetCapitals() {
      Thread.CurrentThread.CurrentCulture = new CultureInfo("nl-NL");

      string startValue = "hello world and goodmorning";
      string result = startValue.CapitalizeSentence();
      Assert.AreEqual("Hello world and goodmorning", result, false);

      startValue = "hello World And goodmorning";
      result = startValue.CapitalizeSentence();
      Assert.AreEqual("Hello world and goodmorning", result, false);

      startValue = "hello woRld aNd goodmorning";
      result = startValue.CapitalizeSentence();
      Assert.AreEqual("Hello world and goodmorning", result, false);

      startValue = string.Empty;
      result = startValue.CapitalizeSentence();
      Assert.AreEqual(string.Empty, result, false);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CapitalizeSentence(string, bool, CultureInfo)"/> extension method.</summary>
    [TestMethod]
    public void StringExtensions_CapitalizeSentenceWithCultureKeepCapitals() {
      /* Set the current culture so a different value to make sure that the testsubject uses the CultureInfo parameter */
      Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

      CultureInfo culture = new CultureInfo("nl-NL");

      string startValue = "hello world and goodmorning";
      string result = startValue.CapitalizeSentence(true, culture);
      Assert.AreEqual("Hello world and goodmorning", result, false);

      startValue = "hello World And goodmorning";
      result = startValue.CapitalizeSentence(true, culture);
      Assert.AreEqual("Hello World And goodmorning", result, false);

      startValue = "hello woRld aNd goodmorning";
      result = startValue.CapitalizeSentence(true, culture);
      Assert.AreEqual("Hello woRld aNd goodmorning", result, false);

      startValue = string.Empty;
      result = startValue.CapitalizeSentence(true, culture);
      Assert.AreEqual(string.Empty, result, false);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.CapitalizeSentence(string, CultureInfo)"/> extension method.</summary>
    [TestMethod]
    public void StringExtensions_CapitalizeSentenceWithCultureResetCapitals() {
      /* Set the current culture so a different value to make sure that the testsubject uses the CultureInfo parameter */
      Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

      CultureInfo culture = new CultureInfo("nl-NL");

      string startValue = "hello world and goodmorning";
      string result = startValue.CapitalizeSentence(culture);
      Assert.AreEqual("Hello world and goodmorning", result, false);

      startValue = "hello World And goodmorning";
      result = startValue.CapitalizeSentence(culture);
      Assert.AreEqual("Hello world and goodmorning", result, false);

      startValue = "hello woRld aNd goodmorning";
      result = startValue.CapitalizeSentence(culture);
      Assert.AreEqual("Hello world and goodmorning", result, false);

      startValue = string.Empty;
      result = startValue.CapitalizeSentence(culture);
      Assert.AreEqual(string.Empty, result, false);
    }
  }
}
