namespace Enkoni.Framework.ServiceModel.Tests {
  /// <summary>Implements the SchemaValidationBehaviorExtensionElement in order to give access to the protected members for testing purposes.</summary>
  public class SchemaValidationBehaviorExtensionElementTestWrapper : SchemaValidationBehaviorExtensionElement {
    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="SchemaValidationBehaviorExtensionElementTestWrapper"/> class.</summary>
    public SchemaValidationBehaviorExtensionElementTestWrapper() {
    }
    #endregion

    #region Public methods
    /// <summary>Creates a new instance of the behavior.</summary>
    /// <returns>The created behavior.</returns>
    public object ExecuteCreateBehavior() {
      return base.CreateBehavior();
    }
    #endregion
  }
}
