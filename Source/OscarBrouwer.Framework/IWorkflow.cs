//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="IWorkflow.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2011. All rights reserved.
// </copyright>
// <summary>
//     Defines the basic workflow API.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;

namespace Enkoni.Framework {
  /// <summary>Represents a process or workflow that executes a defined task or set of tasks.</summary>
  public interface IWorkflow {
    #region Properties
    /// <summary>Gets a value indicating whether the workflow is able to pause and continue its internal processes. By default, a <b>Workflow</b> 
    /// cannot be paused or continued.</summary>
    bool CanPauseAndContinue { get; }

    /// <summary>Gets the current <see cref="WorkflowState"/> of the workflow.</summary>
    WorkflowState State { get; }
    #endregion

    #region Workflow Synchronous Methods
    /// <summary>Tries to start the workflow. It blocks untill the workflow is started.</summary>
    /// <exception cref="InvalidOperationException">The workflow is not in a state in which it can be started. Only when the workflow is in the state
    /// <see cref="WorkflowState.Init"/> or <see cref="WorkflowState.Stopped"/>, it can be started.</exception>
    void Start();

    /// <summary>Tries to stop the workflow. It blocks untill the workflow is stopped.</summary>
    /// <exception cref="InvalidOperationException">The workflow is not in a state in which it can be stopped. Only when the workflow is in the state 
    /// <see cref="WorkflowState.Started"/>, <see cref="WorkflowState.Pausing"/> or <see cref="WorkflowState.Continued"/>, it can be stopped.</exception>
    void Stop();

    /// <summary>Tries to pause the workflow. It blocks untill the workflow is paused.</summary>
    /// <exception cref="NotSupportedException">The workflow does not support pausing and resuming.</exception>
    /// <exception cref="InvalidOperationException">The workflow is not in a state in which it can be paused. Only when the workflow is in the state
    /// <see cref="WorkflowState.Started"/> or <see cref="WorkflowState.Continued"/>, it can be paused.</exception>
    /// <seealso cref="P:CanPauseAndContinue"/>
    void Pause();

    /// <summary>Tries to continue the workflow. It blocks untill the workflow is continued.</summary>
    /// <exception cref="NotSupportedException">The workflow does not support pausing and continuing.</exception>
    /// <exception cref="InvalidOperationException">The workflow is not in a state in which it can be continued. Only when the workflow is in the
    /// state <see cref="WorkflowState.Paused"/>, it can be continued.</exception>
    /// <seealso cref="P:CanPauseAndContinue"/>
    void Continue();
    #endregion

    #region Workflow Asynchronous Methods
    /// <summary>Begins to start the workflow.</summary>
    /// <param name="callback">The method to be called when the asynchronous start operation is completed.</param>
    /// <param name="state">A user-provided object that distinguishes this particular asynchronous start request from other requests.</param>
    /// <returns>An <see cref="IAsyncResult"/> that references the asynchronous start.</returns>
    IAsyncResult BeginStart(AsyncCallback callback, object state);

    /// <summary>Waits for the pending asynchronous start to complete.</summary>
    /// <param name="asyncResult">The reference to the pending asynchronous request to wait for.</param>
    /// <exception cref="InvalidOperationException">The workflow is not in a state in which it can be started. Only when the workflow is in the state 
    /// <see cref="WorkflowState.Init"/> or <see cref="WorkflowState.Stopped"/>, it can be started.</exception>
    void EndStart(IAsyncResult asyncResult);

    /// <summary>Begins to stop the workflow.</summary>
    /// <param name="callback">The method to be called when the asynchronous stop operation is completed.</param>
    /// <param name="state">A user-provided object that distinguishes this particular asynchronous stop request from other requests.</param>
    /// <returns>An <see cref="IAsyncResult"/> that references the asynchronous stop.</returns>
    IAsyncResult BeginStop(AsyncCallback callback, object state);

    /// <summary>Waits for the pending asynchronous stop to complete.</summary>
    /// <param name="asyncResult">The reference to the pending asynchronous request to wait for.</param>
    /// <exception cref="InvalidOperationException">The workflow is not in a state in which it can be stopped. Only when the workflow is in the state
    /// <see cref="WorkflowState.Started"/>, <see cref="WorkflowState.Pausing"/> or <see cref="WorkflowState.Continued"/>, it can be stopped.</exception>
    void EndStop(IAsyncResult asyncResult);

    /// <summary>Begins to pause the workflow.</summary>
    /// <param name="callback">The method to be called when the asynchronous pause operation is completed.</param>
    /// <param name="state">A user-provided object that distinguishes this particular asynchronous pause request from other requests.</param>
    /// <returns>An <see cref="IAsyncResult"/> that references the asynchronous pause.</returns>
    IAsyncResult BeginPause(AsyncCallback callback, object state);

    /// <summary>Waits for the pending asynchronous pause to complete.</summary>
    /// <param name="asyncResult">The reference to the pending asynchronous request to wait for.</param>
    /// <exception cref="NotSupportedException">The workflow does not support pausing and resuming.</exception>
    /// <exception cref="InvalidOperationException">The workflow is not in a state in which it can be paused. Only when the workflow is in the state 
    /// <see cref="WorkflowState.Started"/> or <see cref="WorkflowState.Continued"/>, it can be paused.</exception>
    /// <seealso cref="P:CanPauseAndContinue"/>
    void EndPause(IAsyncResult asyncResult);

    /// <summary>Begins to continue the workflow.</summary>
    /// <param name="callback">The method to be called when the asynchronous continue operation is completed.</param>
    /// <param name="state">A user-provided object that distinguishes this particular asynchronous continue request from other requests.</param>
    /// <returns>An <see cref="IAsyncResult"/> that references the asynchronous continue.</returns>
    IAsyncResult BeginContinue(AsyncCallback callback, object state);

    /// <summary>Waits for the pending asynchronous continue to complete.</summary>
    /// <param name="asyncResult">The reference to the pending asynchronous request to wait for.</param>
    /// <exception cref="NotSupportedException">The workflow does not support pausing and continuing.</exception>
    /// <exception cref="InvalidOperationException">The workflow is not in a state in which it can be continued. Only when the workflow is in the 
    /// state <see cref="WorkflowState.Paused"/>, it can be continued.</exception>
    /// <seealso cref="P:CanPauseAndContinue"/>
    void EndContinue(IAsyncResult asyncResult);
    #endregion
  }
}
