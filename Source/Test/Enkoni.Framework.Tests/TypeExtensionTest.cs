using System;
using System.Linq;
using System.Reflection;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Tests {
  /// <summary>This class defines the testcases that test the functionality of the extension methods for the Type-class.</summary>
  [TestClass]
  public class TypeExtensionTest {
    #region IsNullable test-cases
    /// <summary>Tests the functionality of the <see cref="Extensions.IsNullable(Type)"/> extension method.</summary>
    [TestMethod]
    public void TypeExtensions_IsNullable_ReferenceType_IsNotNullable() {
      /* Test the method using a reference type */
      Type subject = typeof(string);
      bool result = subject.IsNullable();
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.IsNullable(Type)"/> extension method.</summary>
    [TestMethod]
    public void TypeExtensions_IsNullable_ValueType_IsNotNullable() {
      /* Test the method using a not-nullable value type */
      Type subject = typeof(int);
      bool result = subject.IsNullable();
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.IsNullable(Type)"/> extension method.</summary>
    [TestMethod]
    public void TypeExtensions_IsNullable_NullableType_IsNullable() {
      /* Test the method using a nullable value type */
      Type subject = typeof(int?);
      bool result = subject.IsNullable();
      Assert.IsTrue(result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.IsNullable(Type)"/> extension method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TypeExtensions_IsNullable_NullValue_ThrowsException() {
      /* Test the method using a reference type */
      Type subject = null;
      bool result = subject.IsNullable();
    }
    #endregion

    #region ActualType test-cases
    /// <summary>Tests the functionality of the <see cref="Extensions.ActualType(Type)"/> extension method.</summary>
    [TestMethod]
    public void TypeExtensions_ActualType_ReferenceType_ReturnsSameType() {
      /* Test the method using a reference type */
      Type subject = typeof(string);
      Type result = subject.ActualType();
      Assert.AreEqual(typeof(string), result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.ActualType(Type)"/> extension method.</summary>
    [TestMethod]
    public void TypeExtensions_ActualType_ValueType_ReturnsSameType() {
      /* Test the method using a not-nullable value type */
      Type subject = typeof(int);
      Type result = subject.ActualType();
      Assert.AreEqual(typeof(int), result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.ActualType(Type)"/> extension method.</summary>
    [TestMethod]
    public void TypeExtensions_ActualType_NullableType_ReturnsNonNullableType() {
      /* Test the method using a nullable value type */
      Type subject = typeof(int?);
      Type result = subject.ActualType();
      Assert.AreEqual(typeof(int), result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.ActualType(Type)"/> extension method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TypeExtensions_ActualType_NullValue_ThrowsException() {
      /* Test the method using a reference type */
      Type subject = null;
      Type result = subject.ActualType();
    }
    #endregion

    #region GetBaseClasses test-cases
    /// <summary>Tests the functionality of the <see cref="Extensions.GetBaseClasses(Type)"/> extension method.</summary>
    [TestMethod]
    public void TypeExtensions_GetBaseClasses_FirstLevelClass_ReturnsAllBaseClasses() {
      Type subject = typeof(ClassA);
      Type[] result = subject.GetBaseClasses();
      Type[] expected = new Type[] { typeof(object) };

      Assert.IsNotNull(result);
      Assert.AreEqual(expected.Length, result.Length);
      CollectionAssert.AreEqual(expected, result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.GetBaseClasses(Type)"/> extension method.</summary>
    [TestMethod]
    public void TypeExtensions_GetBaseClasses_SecondLevelClass_ReturnsAllBaseClasses() {
      Type subject = typeof(ClassC);
      Type[] result = subject.GetBaseClasses();
      Type[] expected = new Type[] { typeof(ClassA), typeof(object) };

      Assert.IsNotNull(result);
      Assert.AreEqual(expected.Length, result.Length);
      CollectionAssert.AreEqual(expected, result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.GetBaseClasses(Type)"/> extension method.</summary>
    [TestMethod]
    public void TypeExtensions_GetBaseClasses_ThirdLevelClass_ReturnsAllBaseClasses() {
      Type subject = typeof(ClassD);
      Type[] result = subject.GetBaseClasses();
      Type[] expected = new Type[] { typeof(ClassC), typeof(ClassA), typeof(object) };

      Assert.IsNotNull(result);
      Assert.AreEqual(expected.Length, result.Length);
      CollectionAssert.AreEqual(expected, result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.GetBaseClasses(Type)"/> extension method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TypeExtensions_GetBaseClasses_NullValue_ThrowsException() {
      Type subject = null;
      Type[] result = subject.GetBaseClasses();
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.GetBaseClasses(Type)"/> extension method.</summary>
    [TestMethod]
    public void TypeExtensions_GetBaseClasses_GenericTypeInHierarchie_ReturnsAllBaseClasses() {
      Type subject = typeof(ClassF);
      Type[] result = subject.GetBaseClasses();
      Type[] expected = new Type[] { typeof(ClassE<int>), typeof(ClassB), typeof(ClassA), typeof(object) };

      Assert.IsNotNull(result);
      Assert.AreEqual(expected.Length, result.Length);
      CollectionAssert.AreEqual(expected, result);
    }
    #endregion

    #region GetBaseTypes test-cases
    /// <summary>Tests the functionality of the <see cref="Extensions.GetBaseTypes(Type)"/> extension method.</summary>
    [TestMethod]
    public void TypeExtensions_GetBaseTypes_FirstLevelClass_ReturnsAllBaseTypes() {
      Type subject = typeof(ClassA);
      Type[] result = subject.GetBaseTypes();
      Type[] expected = new Type[] { typeof(IInterfaceA), typeof(object) };

      Assert.IsNotNull(result);
      Assert.AreEqual(expected.Length, result.Length);
      CollectionAssert.AreEqual(expected, result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.GetBaseTypes(Type)"/> extension method.</summary>
    [TestMethod]
    public void TypeExtensions_GetBaseTypes_SecondLevelClass_ReturnsAllBaseTypes() {
      Type subject = typeof(ClassC);
      Type[] result = subject.GetBaseTypes();
      Type[] expected = new Type[] { typeof(IInterfaceA), typeof(IInterfaceC), typeof(ClassA), typeof(object) };

      Assert.IsNotNull(result);
      Assert.AreEqual(expected.Length, result.Length);
      CollectionAssert.AreEqual(expected, result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.GetBaseTypes(Type)"/> extension method.</summary>
    [TestMethod]
    public void TypeExtensions_GetBaseTypes_ThirdLevelClass_ReturnsAllBaseTypes() {
      Type subject = typeof(ClassD);
      Type[] result = subject.GetBaseTypes();
      Type[] expected = new Type[] { typeof(IInterfaceA), typeof(IInterfaceC), typeof(ClassC), typeof(ClassA), typeof(object) };

      Assert.IsNotNull(result);
      Assert.AreEqual(expected.Length, result.Length);
      CollectionAssert.AreEqual(expected, result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.GetBaseTypes(Type)"/> extension method.</summary>
    [TestMethod]
    public void TypeExtensions_GetBaseTypes_InterfaceType_ReturnsAllBaseTypes() {
      Type subject = typeof(IInterfaceX);
      Type[] result = subject.GetBaseTypes();
      Type[] expected = new Type[] { typeof(IInterfaceA) };

      Assert.IsNotNull(result);
      Assert.AreEqual(expected.Length, result.Length);
      CollectionAssert.AreEqual(expected, result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.GetBaseTypes(Type)"/> extension method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TypeExtensions_GetBaseTypes_NullValue_ThrowsException() {
      Type subject = null;
      Type[] result = subject.GetBaseTypes();
    }
    #endregion

    #region Implements test-cases
    /// <summary>Tests the functionality of the <see cref="Extensions.Implements(Type,Type)"/> extension method.</summary>
    [TestMethod]
    public void TypeExtensions_Implements_BaseClassImplementsChildClass_ReturnsFalse() {
      Type subjectParent = typeof(ClassC);
      Type subjectChild = typeof(ClassD);

      bool result = subjectParent.Implements(subjectChild);
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.Implements(Type,Type)"/> extension method.</summary>
    [TestMethod]
    public void TypeExtensions_Implements_ChildClassImplementsBaseClass_ReturnsTrue() {
      Type subjectParent = typeof(ClassC);
      Type subjectChild = typeof(ClassD);

      bool result = subjectChild.Implements(subjectParent);
      Assert.IsTrue(result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.Implements(Type,Type)"/> extension method.</summary>
    [TestMethod]
    public void TypeExtensions_Implements_InterfaceImplementsClass_ReturnsFalse() {
      Type subjectChild = typeof(ClassD);
      Type subjectParent = typeof(IInterfaceA);

      bool result = subjectParent.Implements(subjectChild);
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.Implements(Type,Type)"/> extension method.</summary>
    [TestMethod]
    public void TypeExtensions_Implements_ClassImplementsInterface_ReturnsTrue() {
      Type subjectParent = typeof(IInterfaceA);
      Type subjectChild = typeof(ClassD);

      bool result = subjectChild.Implements(subjectParent);
      Assert.IsTrue(result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.Implements(Type,Type)"/> extension method.</summary>
    [TestMethod]
    public void TypeExtensions_Implements_UnrelatedClasses_ReturnsFalse() {
      Type subjectParent = typeof(ClassB);
      Type subjectChild = typeof(ClassC);

      bool result = subjectParent.Implements(subjectChild);
      Assert.IsFalse(result);

      result = subjectChild.Implements(subjectParent);
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.Implements(Type,Type)"/> extension method.</summary>
    [TestMethod]
    public void TypeExtensions_Implements_SameClass_ReturnsTrue() {
      Type subjectParent = typeof(ClassB);
      Type subjectChild = typeof(ClassB);

      bool result = subjectParent.Implements(subjectChild);
      Assert.IsTrue(result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.Implements(Type,Type)"/> extension method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TypeExtensions_Implements_NullSource_ThrowsException() {
      Type subjectParent = null;
      Type subjectChild = typeof(ClassD);

      bool result = subjectParent.Implements(subjectChild);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.Implements(Type,Type)"/> extension method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TypeExtensions_Implements_NullBaseType_ThrowsException() {
      Type subjectParent = null;
      Type subjectChild = typeof(ClassD);

      bool result = subjectChild.Implements(subjectParent);
    }
    #endregion

    #region GetDerivedTypes test-cases
    /// <summary>Tests the functionality of the <see cref="Extensions.GetDerivedTypes(Type)"/> and <see cref="Extensions.GetDerivedTypes(Type,bool)"/>
    /// extension methods.</summary>
    [TestMethod]
    public void TypeExtensions_GetDerivedTypes_ClassWithNoDerivedTypes_EmptyListIsReturned() {
      Type subject = typeof(ClassF);

      Type[] result = subject.GetDerivedTypes();
      Assert.IsNotNull(result);
      Assert.AreEqual(0, result.Length);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.GetDerivedTypes(Type)"/> and <see cref="Extensions.GetDerivedTypes(Type,bool)"/>
    /// extension methods.</summary>
    [TestMethod]
    public void TypeExtensions_GetDerivedTypes_GenericClassWithOneDerivedType_DerivedTypesAreReturned() {
      Type subject = typeof(ClassE<int>);
      Type[] result = subject.GetDerivedTypes();
      Type[] expected = new Type[] { typeof(ClassF) };
      
      Assert.IsNotNull(result);
      CollectionAssert.AreEqual(expected, result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.GetDerivedTypes(Type)"/> and <see cref="Extensions.GetDerivedTypes(Type,bool)"/>
    /// extension methods.</summary>
    [TestMethod]
    public void TypeExtensions_GetDerivedTypes_GenericClassWithNoDerivedType_DerivedTypesAreReturned() {
      Type subject = typeof(ClassE<string>);
      Type[] result = subject.GetDerivedTypes();
      
      Assert.IsNotNull(result);
      Assert.AreEqual(0, result.Length);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.GetDerivedTypes(Type)"/> and <see cref="Extensions.GetDerivedTypes(Type,bool)"/>
    /// extension methods.</summary>
    [TestMethod]
    public void TypeExtensions_GetDerivedTypes_ClassWithMultipleDerivedTypes_DerivedTypesAreReturned() {
      Type subject = typeof(ClassA);
      Type[] result = subject.GetDerivedTypes();
      Type[] expected = new Type[] { typeof(ClassB), typeof(ClassC), typeof(ClassD), typeof(ClassE<int>).GetGenericTypeDefinition(), typeof(ClassF) };
      
      Assert.IsNotNull(result);
      CollectionAssert.AreEqual(expected, result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.GetDerivedTypes(Type)"/> and <see cref="Extensions.GetDerivedTypes(Type,bool)"/>
    /// extension methods.</summary>
    [TestMethod]
    public void TypeExtensions_GetDerivedTypes_InterfaceWithMultipleDerivedTypes_DerivedTypesAreReturned() {
      Type subject = typeof(IInterfaceA);
      Type[] result = subject.GetDerivedTypes();
      Type[] expected = new Type[] { typeof(IInterfaceX), typeof(ClassA), typeof(ClassB), typeof(ClassC), typeof(ClassD), typeof(ClassE<int>).GetGenericTypeDefinition(), typeof(ClassF) };
      
      Assert.IsNotNull(result);
      CollectionAssert.AreEqual(expected, result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.GetDerivedTypes(Type)"/> and <see cref="Extensions.GetDerivedTypes(Type,bool)"/>
    /// extension methods.</summary>
    [TestMethod]
    [DeploymentItem(@"Enkoni.Framework.Tests.Addition.dll", @"Additions")]
    public void TypeExtensions_GetDerivedTypes_DerivedTypeInSeperateAssembly_DerivedTypesAreReturned() {
      AssemblyName assemblyName = AssemblyName.GetAssemblyName(@"Additions\Enkoni.Framework.Tests.Addition.dll");
      Assembly loadedAssembly = AppDomain.CurrentDomain.Load(assemblyName);
      Type subject = typeof(ClassX);
      Type[] result = subject.GetDerivedTypes();
      Assert.IsNotNull(result);
      Assert.AreEqual(1, result.Length);

      result = subject.GetDerivedTypes(true);
      Assert.IsNotNull(result);
      Assert.AreEqual(1, result.Length);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.GetDerivedTypes(Type)"/> and <see cref="Extensions.GetDerivedTypes(Type,bool)"/>
    /// extension methods.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TypeExtensions_GetDerivedTypes_NullSource_ExceptionIsThrown() {
      Type subject = null;
      Type[] result = subject.GetDerivedTypes();
    }
    #endregion
  }
}
