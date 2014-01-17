//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="Mvc3FixedModelBinder.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2013. All rights reserved.
// </copyright>
// <summary>
//     Holds a custom model binder that fixes some bugs in the MVC3's default model binder.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Web.Mvc;

namespace Enkoni.Framework.Web.Mvc {
  /// <summary>Provides an override for <see cref="DefaultModelBinder"/> in order to implement fixes to its behaviour. The MVC3 library from 
  /// Microsoft contains a couple of bugs that cause runtime exceptions when binding received data to the model. The default translation of an enum 
  /// from server to client is enum-to-int. However, the default translation of an enum from client to server is string-to-enum. This bug has been 
  /// fixed in MVC4.</summary>
  public class Mvc3FixedModelBinder : DefaultModelBinder {
    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="Mvc3FixedModelBinder"/> class.</summary>
    public Mvc3FixedModelBinder()
      : base() {
    }
    #endregion

    #region DefaultModelBinder overrides
    /// <summary>Returns the value of a property using the specified controller context, binding context, property descriptor, and property binder. 
    /// It implements a fix for the default model binder's failure to decode enum types when binding to JSON.</summary>
    /// <param name="controllerContext">The context within which the controller operates. The context information includes the controller, HTTP 
    /// content, request context, and route data.</param>
    /// <param name="bindingContext">The context within which the model is bound. The context includes information such as the model object, model 
    /// name, model type, property filter, and value provider.</param>
    /// <param name="propertyDescriptor">The descriptor for the property to access. The descriptor provides information such as the component type, 
    /// property type, and property value. It also provides methods to get or set the property value.</param>
    /// <param name="propertyBinder">An object that provides a way to bind the property.</param>
    /// <returns>An object that represents the property value.</returns>
    protected override object GetPropertyValue(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor, IModelBinder propertyBinder) {
      if(propertyDescriptor == null) {
        throw new ArgumentNullException("propertyDescriptor");
      }

      if(bindingContext == null) {
        throw new ArgumentNullException("bindingContext");
      }

      Type propertyType = propertyDescriptor.PropertyType;

      /* Check if the model property is an enum; otherwise, let the default behaviour handle the binding */
      if(propertyType.IsEnum) {
        /* Retrieve the value information from the received data */
        ValueProviderResult providerValue = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        if(providerValue != null) {
          /* Retrieve the raw value from the data. If the raw value is 'null', let the default behaviour handle the binding */
          object value = providerValue.RawValue;
          if(value != null) {
            /* Get the type of the raw value. If the type is not an enum, we take over; otherwise, let the default behaviour handle the binding (enum-to-enum should work fine) */
            Type valueType = value.GetType();
            if(!valueType.IsEnum) {
              try {
                return Enum.ToObject(propertyType, value);
              }
              catch(ArgumentException ex) {
                bindingContext.ModelState.AddModelError("Binding exception", ex.Message);
              }
            }
          }
        }
      }
      
      return base.GetPropertyValue(controllerContext, bindingContext, propertyDescriptor, propertyBinder);
    }
    #endregion
  }
}
