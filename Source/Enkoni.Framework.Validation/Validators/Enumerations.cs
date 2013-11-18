//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="Enumerations.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2013. All rights reserved.
// </copyright>
// <summary>
//     Contains the enumerations that are defined in this project.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;

namespace Enkoni.Framework.Validation.Validators {
  /// <summary>Defines the supported categories of phone numbers for the <see cref="DutchPhoneNumberValidator"/>.</summary>
  [Flags]
  public enum PhoneNumberCategories {
    /// <summary>Match no phone numbers.</summary>
    None = 0,

    /// <summary>Match only regular phone numbers.</summary>
    Regular = 1,

    /// <summary>Match only mobile phone numbers.</summary>
    Mobile = 2,

    /// <summary>Match only service phone numbers.</summary>
    Service = 4,

    /// <summary>Match only emergency phone numbers.</summary>
    Emergency = 8,

    /// <summary>Match phone numbers that to not fall into the other categories.</summary>
    Other = 16,

    /// <summary>Match regular and mobile phone numbers.</summary>
    Default = Regular | Mobile,

    /// <summary>Match all phone numbers.</summary>
    All = Regular | Mobile | Service | Emergency | Other
  }
}
