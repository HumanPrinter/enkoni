using System;
using System.Threading;

namespace Enkoni.Framework {
  /// <summary>Represents a process or workflow that executes a defined task or set of tasks. This class provides the basic logic to start, stop, 
  /// pause and continue a workflow both synchronously and asynchronously.</summary>
  public abstract class Workflow : IWorkflow {
    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="Workflow"/> class. It sets the state to the initial value of 
    /// <see cref="WorkflowState.Init"/>.</summary>
    protected Workflow() {
      this.State = WorkflowState.Init;
    }
    #endregion

    #region Properties
    /// <summary>Gets a value indicating whether the workflow is able to pause and continue its internal processes. By default, a <b>Workflow</b> 
    /// cannot be paused or continued.</summary>
    public virtual bool CanPauseAndContinue {
      get { return false; }
    }

    /// <summary>Gets or sets the current <see cref="WorkflowState"/> of the workflow.</summary>
    public WorkflowState State { get; protected set; }

    /// <summary>Gets a value indicating whether this workflow is in a state in which it can be started. Only when the workflow is in the state 
    /// <see cref="WorkflowState.Init"/> or <see cref="WorkflowState.Stopped"/>, it can be started.</summary>
    public bool CanStart {
      get { return this.State == WorkflowState.Init || this.State == WorkflowState.Stopped; }
    }

    /// <summary>Gets a value indicating whether this workflow is in a state in which it can be stopped. Only when the workflow is in the state 
    /// <see cref="WorkflowState.Started"/>, <see cref="WorkflowState.Pausing"/> or <see cref="WorkflowState.Continued"/>, it can be stopped.</summary>
    public bool CanStop {
      get { return this.State == WorkflowState.Started || this.State == WorkflowState.Pausing || this.State == WorkflowState.Continued; }
    }

    /// <summary>Gets a value indicating whether this workflow is in a state in which it can be paused. Only when the workflow supports pausing and 
    /// the workflow is in the state <see cref="WorkflowState.Started"/> or <see cref="WorkflowState.Continued"/>, it can be paused.</summary>
    public bool CanPause {
      get { return this.CanPauseAndContinue && (this.State == WorkflowState.Started || this.State == WorkflowState.Continued); }
    }

    /// <summary>Gets a value indicating whether this workflow is in a state in which it can be continued. Only when the workflow supports pausing and 
    /// the workflow is in the state <see cref="WorkflowState.Paused"/>, it can be continued.</summary>
    public bool CanContinue {
      get { return this.CanPauseAndContinue && this.State == WorkflowState.Paused; }
    }
    #endregion

    #region Workflow Synchronous Methods
    /// <summary>Tries to start the workflow. It blocks until the workflow is started.</summary>
    /// <exception cref="InvalidOperationException">The workflow is not in a state in which it can be started. Only when the workflow is in the state 
    /// <see cref="WorkflowState.Init"/> or <see cref="WorkflowState.Stopped"/>, it can be started.</exception>
    public void Start() {
      if(this.State == WorkflowState.Init || this.State == WorkflowState.Stopped) {
        /* Set the state to indicate that the workflow is starting. */
        this.State = WorkflowState.Starting;

        try {
          /* Start the workflow */
          this.OnStart();

          /* Set the state to indicate that the workflow has finished its start-sequence. */
          this.State = WorkflowState.Started;
        }
        catch(InvalidOperationException) {
          this.State = WorkflowState.Faulted;
          throw;
        }
      }
      else {
        throw new InvalidOperationException("The workflow cannot be started because it is in a non-startable state");
      }
    }

    /// <summary>Tries to stop the workflow. It blocks until the workflow is stopped.</summary>
    /// <exception cref="InvalidOperationException">The workflow is not in a state in which it can be stopped. Only when the workflow is in the state 
    /// <see cref="WorkflowState.Started"/>, <see cref="WorkflowState.Pausing"/> or <see cref="WorkflowState.Continued"/>, it can be stopped.
    /// </exception>
    public void Stop() {
      if(this.State == WorkflowState.Started || this.State == WorkflowState.Pausing ||
        this.State == WorkflowState.Continued) {
        /* Set the state to indicate that the workflow is stopping. */
        this.State = WorkflowState.Stopping;

        try {
          /* Stop the workflow */
          this.OnStop();

          /* Set the state to indicate that the workflow has finished its stop-sequence. */
          this.State = WorkflowState.Stopped;
        }
        catch(InvalidOperationException) {
          this.State = WorkflowState.Faulted;
          throw;
        }
      }
      else {
        throw new InvalidOperationException("The workflow cannot be stopped because it is in a non-stoppable state");
      }
    }

    /// <summary>Tries to pause the workflow. It blocks until the workflow is paused.</summary>
    /// <exception cref="NotSupportedException">The workflow does not support pausing and resuming.</exception>
    /// <exception cref="InvalidOperationException">The workflow is not in a state in which it can be paused. Only when the workflow is in the state 
    /// <see cref="WorkflowState.Started"/> or <see cref="WorkflowState.Continued"/>, it can be paused.</exception>
    /// <seealso cref="CanPauseAndContinue"/>
    public void Pause() {
      if(this.CanPauseAndContinue) {
        if(this.State == WorkflowState.Started || this.State == WorkflowState.Continued) {
          /* Set the state to indicate that the workflow is pausing. */
          this.State = WorkflowState.Pausing;

          try {
            /* Pause the workflow */
            this.OnPause();

            /* Set the state to indicate that the workflow has finished its pause-sequence. */
            this.State = WorkflowState.Paused;
          }
          catch(InvalidOperationException) {
            this.State = WorkflowState.Faulted;
            throw;
          }
        }
        else {
          throw new InvalidOperationException("The workflow cannot be paused because it is in a non-pausable state");
        }
      }
      else {
        throw new NotSupportedException("This workflow does not support pause and continue");
      }
    }

    /// <summary>Tries to continue the workflow. It blocks until the workflow is continued.</summary>
    /// <exception cref="NotSupportedException">The workflow does not support pausing and continuing.</exception>
    /// <exception cref="InvalidOperationException">The workflow is not in a state in which it can be continued. Only when the workflow is in the 
    /// state <see cref="WorkflowState.Paused"/>, it can be continued.</exception>
    /// <seealso cref="CanPauseAndContinue"/>
    public void Continue() {
      if(this.CanPauseAndContinue) {
        if(this.State == WorkflowState.Paused) {
          /* Set the state to indicate that the workflow is continuing. */
          this.State = WorkflowState.Continuing;

          try {
            /* Pause the workflow */
            this.OnContinue();

            /* Set the state to indicate that the workflow has finished its continue-sequence. */
            this.State = WorkflowState.Continued;
          }
          catch(InvalidOperationException) {
            this.State = WorkflowState.Faulted;
            throw;
          }
        }
        else {
          throw new InvalidOperationException("The workflow cannot be continued because it is in a non-continuable state");
        }
      }
      else {
        throw new NotSupportedException("This workflow does not support pause and continue");
      }
    }
    #endregion

    #region Workflow Asynchronous Methods
    /// <summary>Begins to start the workflow.</summary>
    /// <param name="callback">The method to be called when the asynchronous start operation is completed.</param>
    /// <param name="state">A user-provided object that distinguishes this particular asynchronous start request from other requests.</param>
    /// <returns>An <see cref="IAsyncResult"/> that references the asynchronous start.</returns>
    public IAsyncResult BeginStart(AsyncCallback callback, object state) {
      AsyncResultVoid result = new AsyncResultVoid(callback, state);
      ThreadPool.QueueUserWorkItem(this.StartWorkflowHelper, result);
      return result;
    }

    /// <summary>Waits for the pending asynchronous start to complete.</summary>
    /// <param name="asyncResult">The reference to the pending asynchronous request to wait for.</param>
    /// <exception cref="InvalidOperationException">The workflow is not in a state in which it can be started. Only when the workflow is in the state 
    /// <see cref="WorkflowState.Init"/> or <see cref="WorkflowState.Stopped"/>, it can be started.</exception>
    public void EndStart(IAsyncResult asyncResult) {
      if(asyncResult == null) {
        throw new ArgumentNullException("asyncResult");
      }

      AsyncResultVoid result = asyncResult as AsyncResultVoid;
      if(result == null) {
        throw new ArgumentException("The specified IAsyncResult was not of the expected type AsyncResultVoid", "asyncResult");
      }

      result.EndInvoke();
      return;
    }

    /// <summary>Begins to stop the workflow.</summary>
    /// <param name="callback">The method to be called when the asynchronous stop operation is completed.</param>
    /// <param name="state">A user-provided object that distinguishes this particular asynchronous stop request from other requests.</param>
    /// <returns>An <see cref="IAsyncResult"/> that references the asynchronous stop.</returns>
    public IAsyncResult BeginStop(AsyncCallback callback, object state) {
      AsyncResultVoid result = new AsyncResultVoid(callback, state);
      ThreadPool.QueueUserWorkItem(this.StopWorkflowHelper, result);
      return result;
    }

    /// <summary>Waits for the pending asynchronous stop to complete.</summary>
    /// <param name="asyncResult">The reference to the pending asynchronous request to wait for.</param>
    /// <exception cref="InvalidOperationException">The workflow is not in a state in which it can be stopped. Only when the workflow is in the state 
    /// <see cref="WorkflowState.Started"/>, <see cref="WorkflowState.Pausing"/> or <see cref="WorkflowState.Continued"/>, it can be stopped.</exception>
    public void EndStop(IAsyncResult asyncResult) {
      if(asyncResult == null) {
        throw new ArgumentNullException("asyncResult");
      }

      AsyncResultVoid result = asyncResult as AsyncResultVoid;
      if(result == null) {
        throw new ArgumentException("The specified IAsyncResult was not of the expected type AsyncResultVoid", "asyncResult");
      }

      result.EndInvoke();
      return;
    }

    /// <summary>Begins to pause the workflow.</summary>
    /// <param name="callback">The method to be called when the asynchronous pause operation is completed.</param>
    /// <param name="state">A user-provided object that distinguishes this particular asynchronous pause request from other requests.</param>
    /// <returns>An <see cref="IAsyncResult"/> that references the asynchronous pause.</returns>
    public IAsyncResult BeginPause(AsyncCallback callback, object state) {
      AsyncResultVoid result = new AsyncResultVoid(callback, state);
      ThreadPool.QueueUserWorkItem(this.PauseWorkflowHelper, result);
      return result;
    }

    /// <summary>Waits for the pending asynchronous pause to complete.</summary>
    /// <param name="asyncResult">The reference to the pending asynchronous request to wait for.</param>
    /// <exception cref="NotSupportedException">The workflow does not support pausing and resuming.</exception>
    /// <exception cref="InvalidOperationException">The workflow is not in a state in which it can be paused. Only when the workflow is in the state 
    /// <see cref="WorkflowState.Started"/> or <see cref="WorkflowState.Continued"/>, it can be paused.</exception>
    /// <seealso cref="CanPauseAndContinue"/>
    public void EndPause(IAsyncResult asyncResult) {
      if(asyncResult == null) {
        throw new ArgumentNullException("asyncResult");
      }

      AsyncResultVoid result = asyncResult as AsyncResultVoid;
      if(result == null) {
        throw new ArgumentException("The specified IAsyncResult was not of the expected type AsyncResultVoid", "asyncResult");
      }

      result.EndInvoke();
      return;
    }

    /// <summary>Begins to continue the workflow.</summary>
    /// <param name="callback">The method to be called when the asynchronous continue operation is completed.</param>
    /// <param name="state">A user-provided object that distinguishes this particular asynchronous continue request from other requests.</param>
    /// <returns>An <see cref="IAsyncResult"/> that references the asynchronous continue.</returns>
    public IAsyncResult BeginContinue(AsyncCallback callback, object state) {
      AsyncResultVoid result = new AsyncResultVoid(callback, state);
      ThreadPool.QueueUserWorkItem(this.ContinueWorkflowHelper, result);
      return result;
    }

    /// <summary>Waits for the pending asynchronous continue to complete.</summary>
    /// <param name="asyncResult">The reference to the pending asynchronous request to wait for.</param>
    /// <exception cref="NotSupportedException">The workflow does not support pausing and continuing.</exception>
    /// <exception cref="InvalidOperationException">The workflow is not in a state in which it can be continued. Only when the workflow is in the 
    /// state <see cref="WorkflowState.Paused"/>, it can be continued.</exception>
    /// <seealso cref="CanPauseAndContinue"/>
    public void EndContinue(IAsyncResult asyncResult) {
      if(asyncResult == null) {
        throw new ArgumentNullException("asyncResult");
      }

      AsyncResultVoid result = asyncResult as AsyncResultVoid;
      if(result == null) {
        throw new ArgumentException("The specified IAsyncResult was not of the expected type AsyncResultVoid", "asyncResult");
      }

      result.EndInvoke();
      return;
    }
    #endregion

    #region Protected methods
    /// <summary>Contains the actual start-up logic. The default implementation is empty, subclasses will have to provide the implementation by 
    /// overriding this method. Implementers of this method do not have to check the state of the workflow or change the state of the workflow as 
    /// this is already done by the public <see cref="Start()"/> method.</summary>
    /// <remarks>If the implementer detects a situation in which it cannot be started, it should throw an <see cref="InvalidOperationException"/>. 
    /// This will cause the <b>Workflow</b> to move to the <see cref="WorkflowState.Faulted"/> state.</remarks>
    protected virtual void OnStart() {
      /* Empty implementation. Implementation is to be provided by the subclasses. */
    }

    /// <summary>Contains the actual stop logic. The default implementation is empty, subclasses will have to provide the implementation by 
    /// overriding this method. Implementers of this method do not have to check the state of the workflow or change the state of the workflow as 
    /// this is already done by the public <see cref="Stop()"/> method.</summary>
    /// <remarks>If the implementer detects a situation in which it cannot be stopped, it should throw an <see cref="InvalidOperationException"/>. 
    /// This will cause the <b>Workflow</b> to move to the <see cref="WorkflowState.Faulted"/> state.</remarks>
    protected virtual void OnStop() {
      /* Empty implementation. Implementation is to be provided by the subclasses. */
    }

    /// <summary>Contains the actual pause logic. The default implementation is empty, subclasses will have to provide the implementation by 
    /// overriding this method. Implementers of this method do not have to check the state of the workflow or change the state of the workflow as 
    /// this is already done by the public <see cref="Pause()"/> method.</summary>
    /// <remarks>If the implementer detects a situation in which it cannot be paused, it should throw an <see cref="InvalidOperationException"/>. 
    /// This will cause the <b>Workflow</b> to move to the <see cref="WorkflowState.Faulted"/> state.</remarks>
    protected virtual void OnPause() {
      /* Empty implementation. Implementation is to be provided by the subclasses. */
    }

    /// <summary>Contains the actual continue logic. The default implementation is empty, subclasses will have to provide the implementation by 
    /// overriding this method. Implementers of this method do not have to check the state of the workflow or change the state of the workflow as 
    /// this is already done by the public <see cref="Continue()"/> method.</summary>
    /// <remarks>If the implementer detects a situation in which it cannot be continued, it should throw an <see cref="InvalidOperationException"/>. 
    /// This will cause the <b>Workflow</b> to move to the <see cref="WorkflowState.Faulted"/> state.</remarks>
    protected virtual void OnContinue() {
      /* Empty implementation. Implementation is to be provided by the subclasses. */
    }
    #endregion

    #region Private methods
    /// <summary>Executes the <see cref="Start()"/> method in a separate thread. This is used to support asynchronous operations.</summary>
    /// <param name="asyncResult">The object that holds the status of the asynchronous operation.</param>
    private void StartWorkflowHelper(object asyncResult) {
      AsyncResultVoid result = asyncResult as AsyncResultVoid;
      if(result == null) {
        throw new ArgumentException("The specified object was not of the expected type AsyncResultVoid", "asyncResult");
      }

      try {
        this.Start();
        result.SetAsCompleted(null, false);
      }
      catch(Exception ex) {
        /* We deliberately catch every exception. It is up to the caller of EndStart() to do a more fine-grained exception handling */
        result.SetAsCompleted(ex, false);
      }
    }

    /// <summary>Executes the <see cref="Stop()"/> method in a separate thread. This is used to support asynchronous operations.</summary>
    /// <param name="asyncResult">The object that holds the status of the asynchronous operation.</param>
    private void StopWorkflowHelper(object asyncResult) {
      AsyncResultVoid result = asyncResult as AsyncResultVoid;
      if(result == null) {
        throw new ArgumentException("The specified object was not of the expected type AsyncResultVoid", "asyncResult");
      }

      try {
        this.Stop();
        result.SetAsCompleted(null, false);
      }
      catch(Exception ex) {
        /* We deliberately catch every exception. It is up to the caller of EndStop() to do a more fine-grained exception handling */
        result.SetAsCompleted(ex, false);
      }
    }

    /// <summary>Executes the <see cref="Pause()"/> method in a separate thread. This is used to support asynchronous operations.</summary>
    /// <param name="asyncResult">The object that holds the status of the asynchronous operation.</param>
    private void PauseWorkflowHelper(object asyncResult) {
      AsyncResultVoid result = asyncResult as AsyncResultVoid;
      if(result == null) {
        throw new ArgumentException("The specified object was not of the expected type AsyncResultVoid", "asyncResult");
      }

      try {
        this.Pause();
        result.SetAsCompleted(null, false);
      }
      catch(Exception ex) {
        /* We deliberately catch every exception. It is up to the caller of EndPause() to do a more fine-grained exception handling */
        result.SetAsCompleted(ex, false);
      }
    }

    /// <summary>Executes the <see cref="Continue()"/> method in a separate thread. This is used to support asynchronous operations.</summary>
    /// <param name="asyncResult">The object that holds the status of the asynchronous operation.</param>
    private void ContinueWorkflowHelper(object asyncResult) {
      AsyncResultVoid result = asyncResult as AsyncResultVoid;
      if(result == null) {
        throw new ArgumentException("The specified object was not of the expected type AsyncResultVoid", "asyncResult");
      }

      try {
        this.Continue();
        result.SetAsCompleted(null, false);
      }
      catch(Exception ex) {
        /* We deliberately catch every exception. It is up to the caller of EndContinue() to do a more fine-grained exception handling */
        result.SetAsCompleted(ex, false);
      }
    }
    #endregion
  }
}