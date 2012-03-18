//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeExtensionTest.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2011. All rights reserved.
// </copyright>
// <summary>
//     Contains testcases that test the functionality of the extension methods for the Type-class.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Reflection;
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

    /// <summary>Tests the functionality of the <see cref="Extensions.GetBaseClasses(Type)"/> extension method.</summary>
    [TestMethod]
    public void TestCase03_GetBaseClasses() {
      Type subject = typeof(ClassC);
      Type[] result = subject.GetBaseClasses();
      Type[] expected = new Type[] { typeof(ClassA), typeof(object) };

      Assert.IsNotNull(result);
      Assert.AreEqual(expected.Length, result.Length);
      Assert.AreEqual(0, expected.Except(result).Count());

      subject = typeof(ClassD);
      result = subject.GetBaseClasses();
      expected = new Type[] { typeof(ClassA), typeof(ClassC), typeof(object) };

      Assert.IsNotNull(result);
      Assert.AreEqual(expected.Length, result.Length);
      Assert.AreEqual(0, expected.Except(result).Count());

      subject = typeof(ClassA);
      result = subject.GetBaseClasses();
      expected = new Type[] { typeof(object) };

      Assert.IsNotNull(result);
      Assert.AreEqual(expected.Length, result.Length);
      Assert.AreEqual(0, expected.Except(result).Count());
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.GetBaseTypes(Type)"/> extension method.</summary>
    [TestMethod]
    public void TestCase04_GetBaseTypes() {
      Type subject = typeof(ClassC);
      Type[] result = subject.GetBaseTypes();
      Type[] expected = new Type[] { typeof(IInterfaceA), typeof(IInterfaceC) ,typeof(ClassA), typeof(object) };

      Assert.IsNotNull(result);
      Assert.AreEqual(expected.Length, result.Length);
      Assert.AreEqual(0, expected.Except(result).Count());

      subject = typeof(ClassD);
      result = subject.GetBaseTypes();
      expected = new Type[] { typeof(IInterfaceA), typeof(IInterfaceC), typeof(ClassA), typeof(ClassC), typeof(object) };

      Assert.IsNotNull(result);
      Assert.AreEqual(expected.Length, result.Length);
      Assert.AreEqual(0, expected.Except(result).Count());

      subject = typeof(ClassA);
      result = subject.GetBaseTypes();
      expected = new Type[] { typeof(IInterfaceA), typeof(object) };

      Assert.IsNotNull(result);
      Assert.AreEqual(expected.Length, result.Length);
      Assert.AreEqual(0, expected.Except(result).Count());
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.Implements(Type,Type)"/> extension method.</summary>
    [TestMethod]
    public void TestCase05_Implements() {
      Type subjectParent = typeof(ClassC);
      Type subjectChild = typeof(ClassD);

      bool result = subjectParent.Implements(subjectChild);
      Assert.IsFalse(result);

      result = subjectChild.Implements(subjectParent);
      Assert.IsTrue(result);

      subjectParent = typeof(IInterfaceA);
      
      result = subjectParent.Implements(subjectChild);
      Assert.IsFalse(result);

      result = subjectChild.Implements(subjectParent);
      Assert.IsTrue(result);

      subjectParent = typeof(ClassD);
      
      result = subjectParent.Implements(subjectChild);
      Assert.IsTrue(result);

      result = subjectChild.Implements(subjectParent);
      Assert.IsTrue(result);

      subjectParent = typeof(ClassB);
      result = subjectParent.Implements(subjectChild);
      Assert.IsFalse(result);

      result = subjectChild.Implements(subjectParent);
      Assert.IsFalse(result);

      subjectParent = typeof(object);
      result = subjectParent.Implements(subjectChild);
      Assert.IsFalse(result);

      result = subjectChild.Implements(subjectParent);
      Assert.IsTrue(result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.GetDerivedTypes(Type)"/> and <see cref="Extensions.GetDerivedTypes(Type,bool)"/>
    /// extension methods.</summary>
    [TestMethod]
#if DEBUG
    [DeploymentItem(@"Test\Enkoni.Framework.Tests.Addition\bin\Debug\Enkoni.Framework.Tests.Addition.dll", @"Additions")]
#else
    [DeploymentItem(@"Test\Enkoni.Framework.Tests.Addition\bin\Release\Enkoni.Framework.Tests.Addition.dll", @"Additions")]
#endif
    public void TestCase06_GetDerivedTypes() {
      Type subject = typeof(ClassD);

      Type[] result = subject.GetDerivedTypes();
      Assert.IsNotNull(result);
      Assert.AreEqual(0, result.Length);

      subject = typeof(ClassC);
      result = subject.GetDerivedTypes();
      Type[] expected = new Type[] { typeof(ClassD) };
      Assert.IsNotNull(result);
      Assert.AreEqual(expected.Length, result.Length);
      Assert.AreEqual(0, expected.Except(result).Count());

      subject = typeof(ClassA);
      result = subject.GetDerivedTypes();
      expected = new Type[] { typeof(ClassB), typeof(ClassC), typeof(ClassD) };
      Assert.IsNotNull(result);
      Assert.AreEqual(expected.Length, result.Length);
      Assert.AreEqual(0, expected.Except(result).Count());

      subject = typeof(IInterfaceA);
      result = subject.GetDerivedTypes();
      expected = new Type[] { typeof(ClassA), typeof(ClassB), typeof(ClassC), typeof(ClassD) };
      Assert.IsNotNull(result);
      Assert.AreEqual(expected.Length, result.Length);
      Assert.AreEqual(0, expected.Except(result).Count());

      subject = typeof(IInterfaceC);
      result = subject.GetDerivedTypes();
      expected = new Type[] { typeof(ClassC), typeof(ClassD) };
      Assert.IsNotNull(result);
      Assert.AreEqual(expected.Length, result.Length);
      Assert.AreEqual(0, expected.Except(result).Count());

      AssemblyName assemblyName = AssemblyName.GetAssemblyName(@"Additions\Enkoni.Framework.Tests.Addition.dll");
      Assembly loadedAssembly = AppDomain.CurrentDomain.Load(assemblyName);
      subject = typeof(ClassD);
      result = subject.GetDerivedTypes();
      Assert.IsNotNull(result);
      Assert.AreEqual(0, result.Length);

      result = subject.GetDerivedTypes(true);
      Assert.IsNotNull(result);
      Assert.AreEqual(1, result.Length);
    }
  }
}
