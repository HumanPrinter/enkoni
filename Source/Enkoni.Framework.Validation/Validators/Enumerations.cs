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

  /// <summary>Defines the supported categories of e-mail addresses for the <see cref="EmailValidator"/>.</summary>
  public enum EmailCategory {
    /// <summary>Match basic e-mail addresses only. Basic e-mail addresses may contain alfa numeric characters and the '-' and '_' characters.</summary>
    Basic,

    /// <summary>Match all e-mail addresses except e-mail addresses that contain quoted strings.</summary>
    Extended,

    /// <summary>Match all e-mail addresses according to RFC5322.</summary>
    Complete
  }
}
