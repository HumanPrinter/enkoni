//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeExtensionTest.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2011. All rights reserved.
// </copyright>
// <summary>
//     Contains testcases that test the functionality of the extension methods for the Type-class.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Tests {
  /// <summary>This class defines the testcases that test the functionality of the extension methods for the Type-class.</summary>
  [TestClass]
  public class TypeExtensionTest {
    /// <summary>Tests the functionality of the <see cref="Extensions.IsNullable(Type)"/> extension method.</summary>
    [TestMethod]
    public void TestCase01_IsNullable() {
			/* Test the method using a reference type */
			Type subject = typeof(string);
			bool result = subject.IsNullable();
			Assert.IsFalse(result);

			/* Test the method using a not-nullable value type */
			subject = typeof(int);
			result = subject.IsNullable();
			Assert.IsFalse(result);

			/* Test the method using a nullable value type */
			subject = typeof(int?);
			result = subject.IsNullable();
			Assert.IsTrue(result);
    }

		/// <summary>Tests the functionality of the <see cref="Extensions.ActualType(Type)"/> extension method.</summary>
		[TestMethod]
		public void TestCase02_ActualType() {
			/* Test the method using a reference type */
			Type subject = typeof(string);
			Type result = subject.ActualType();
			Assert.AreEqual(typeof(string), result);

			/* Test the method using a not-nullable value type */
			subject = typeof(int);
			result = subject.ActualType();
			Assert.AreEqual(typeof(int), result);

			/* Test the method using a nullable value type */
			subject = typeof(int?);
			result = subject.ActualType();
			Assert.AreEqual(typeof(int), result);
		}
  }
}
