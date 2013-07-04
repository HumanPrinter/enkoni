//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="Enumerations.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2013. All rights reserved.
// </copyright>
// <summary>
//     Holds the enumerations of this project.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Enkoni.Framework {
  #region SortOrder enum
  /// <summary>Defines the supported sort orders.</summary>
  public enum SortOrder {
    /// <summary>Indicates that the objects should be sorted 'smallest first'.</summary>
    Ascending,

    /// <summary>Indicates that the objects should be sorted 'biggest first'.</summary>
    Descending
  }
  #endregion

  #region WorkflowState enum
  /// <summary>Contains the states in which a workflow can be. A workflow can only be in one state at a time.</summary>
  public enum WorkflowState {
    /// <summary>Indicates that the workflow is created but not yet started.</summary>
    Init,

    /// <summary>Indicates that the workflow is currently starting.</summary>
    Starting,

    /// <summary>Indicates that the workflow is running.</summary>
    Started,

    /// <summary>Indicates that the workflow is pausing its processes.</summary>
    Pausing,

    /// <summary>Indicates that the workflow is currently paused.</summary>
    Paused,

    /// <summary>Indicates that the workflow is resuming its processes.</summary>
    Continuing,

    /// <summary>Indicates that the workflow is resuming. This basically is equal to the <see cref="Started"/> state.</summary>
    Continued,

    /// <summary>Indicates that the workflow is stopping its processes.</summary>
    Stopping,

    /// <summary>Indicates that the workflow has stopped.</summary>
    Stopped,

    /// <summary>Indicates that the workflow is stopped as a result of an error.</summary>
    Faulted
  }
  #endregion

  #region DoubleCompareOption enum
  /// <summary>Contains the options that can be passed to a double comparison function to specify the method to compare doubles.</summary>
  public enum DoubleCompareOption {
    /// <summary>Compare two doubles by looking if the difference is within a specific margin.</summary>
    Margin,

    /// <summary>Compare two doubles by looking at their significant digits.</summary>
    SignificantDigits
  }
  #endregion
}
