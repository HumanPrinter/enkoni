using System;
using System.Globalization;
using System.Web.Mvc;

namespace Enkoni.Framework.Web.Mvc {
  /// <summary>Provides a custom model binder that is capable of binding decimal values using a configurable format. It also allows
  /// for binding decimal values that do not contain a decimal point.</summary>
  public class DecimalModelBinder : IModelBinder {
    #region Instance variables
    /// <summary>The format provider that is used to convert the decimals.</summary>
    private readonly IFormatProvider formatProvider;
    #endregion

    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="DecimalModelBinder"/> class that will use a default culture invariant formatting to
    /// convert the <see langword="decimal"/> values.</summary>
    public DecimalModelBinder()
      : this(CultureInfo.InvariantCulture) {
    }

    /// <summary>Initializes a new instance of the <see cref="DecimalModelBinder"/> class that will use the specified <see cref="IFormatProvider"/>
    /// instance to convert the <see langword="decimal"/> values.</summary>
    /// <param name="provider">The <see cref="IFormatProvider"/> that will be used to convert the <see langword="decimal"/> values.</param>
    /// <exception cref="ArgumentNullException"><paramref name="provider"/> is <see langword="null"/>.</exception>
    public DecimalModelBinder(IFormatProvider provider) {
      if(provider == null) {
        throw new ArgumentNullException("provider");
      }

      this.formatProvider = provider;
    }
    #endregion

    #region Public methods
    /// <summary>Binds a received decimal value to the view model.</summary>
    /// <param name="controllerContext">An instance that encapsulates information about the HTTP request.</param>
    /// <param name="bindingContext">The context for the model binder.</param>
    /// <returns>The converted decimal value.</returns>
    public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
      if(bindingContext == null) {
        throw new ArgumentNullException("bindingContext");
      }

      /* Retrieve the value information from the received data */
      ValueProviderResult providerValue = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
      ModelState modelState = new ModelState { Value = providerValue };

      object actualValue = null;
      if(providerValue != null && providerValue.RawValue != null) {
        /* Retrieve the raw value and convert it to a decimal */
        actualValue = Convert.ToDecimal(providerValue.RawValue, this.formatProvider);
      }

      /* Register the succesfull conversion and return the converted value */
      bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
      return actualValue;
    }
    #endregion
  }
}
