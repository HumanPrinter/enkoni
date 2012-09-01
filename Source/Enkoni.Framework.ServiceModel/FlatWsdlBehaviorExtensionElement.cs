// --------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="FlatWsdlBehaviorExtensionElement.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//   Implementatie van een extension element waarmee de FlatWsdlBehavior aan een endpoint gekoppeld kan worden via de configuratie.
// </summary>
// <remark>
//   Implementatie gebaseerd op de code van Glav (http://weblogs.asp.net/pglavich/archive/2010/03/16/making-wcf-output-a-single-wsdl-file-for-interop-purposes.aspx)
// </remark>
// --------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.ServiceModel.Configuration;

namespace Enkoni.Framework.ServiceModel {
  /// <summary>Implementeert een behavior extension element waarmee de <see cref="FlatWsdlBehaviorAttribute"/> aan een endpoint kan worden gekoppeld via de 
  /// configuratie.</summary>
  /// <remarks>
  /// The implementation is based on the code of Glav (http://weblogs.asp.net/pglavich/archive/2010/03/16/making-wcf-output-a-single-wsdl-file-for-interop-purposes.aspx).
  /// </remarks>
  public class FlatWsdlBehaviorExtensionElement : BehaviorExtensionElement {
    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="FlatWsdlBehaviorExtensionElement"/> class.</summary>
    public FlatWsdlBehaviorExtensionElement() {
    }
    #endregion

    #region Properties
    /// <summary>Gets the type of behavior that is handled by this extension element.</summary>
    public override Type BehaviorType {
      get { return typeof(FlatWsdlBehaviorAttribute); }
    }

    /// <summary>Creates a new instance of the behavior.</summary>
    /// <returns>The created behavior.</returns>
    protected override object CreateBehavior() {
      return new FlatWsdlBehaviorAttribute();
    }
    #endregion
  }
}
