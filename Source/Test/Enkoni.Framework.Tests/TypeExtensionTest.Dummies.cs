namespace Enkoni.Framework.Tests {
  #region Dummy interfaces
  /// <summary>Defines dummy interface <see cref="IInterfaceA"/>.</summary>
  public interface IInterfaceA {
    /// <summary>A dummy method.</summary>
    void SomeMethodByInterfaceA();
  }

  /// <summary>Defines dummy interface <see cref="IInterfaceC"/>.</summary>
  public interface IInterfaceC {
    /// <summary>A dummy method.</summary>
    void SomeMethodByInterfaceC();
  }

  /// <summary>Defines dummy interface <see cref="IInterfaceX"/>.</summary>
  public interface IInterfaceX : IInterfaceA {
  }
  #endregion

  #region Dummy classes
  /// <summary>Defines dummy class <see cref="ClassA"/>.</summary>
  public class ClassA : IInterfaceA {
    /// <summary>A dummy method.</summary>
    public void SomeMethodByInterfaceA() {
    }

    /// <summary>A dummy method.</summary>
    public virtual void SomeMethodByClassA() {
    }
  }

  /// <summary>Defines dummy class <see cref="ClassB"/>.</summary>
  public class ClassB : ClassA {
  }

  /// <summary>Defines dummy class <see cref="ClassC"/>.</summary>
  public class ClassC : ClassA, IInterfaceC {
    /// <summary>A dummy method.</summary>
    public void SomeMethodByInterfaceC() {
    }
  }

  /// <summary>Defines dummy class <see cref="ClassD"/>.</summary>
  public class ClassD : ClassC {
    /// <summary>A dummy method.</summary>
    public override void SomeMethodByClassA() {
    }
  }

  /// <summary>Defines dummy class <see cref="ClassE{T}"/>.</summary>
  public class ClassE<T> : ClassB {
    /// <summary>A dummy method.</summary>
    /// <param name="input">A dummy parameter.</param>
    public void SomeGenericMethod(T input) {
    }
  }

  /// <summary>Defines dummy class <see cref="ClassF"/>.</summary>
  public class ClassF : ClassE<int> {
  }

  /// <summary>Defines dummy class <see cref="ClassX"/>.</summary>
  public class ClassX {
  }
  #endregion
}