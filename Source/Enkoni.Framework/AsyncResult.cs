using System;
using System.Threading;

namespace Enkoni.Framework {
  /// <summary>Represents the status of an asynchronous operation.</summary>
  /// <typeparam name="T">The type that is returned by the asynchronous operation.</typeparam>
  public class AsyncResult<T> : IAsyncResult {
    #region Private constants
    /// <summary>Indicates that the operation is pending.</summary>
    private const int PendingState = 0;

    /// <summary>Indicates that the operation has completed synchronously.</summary>
    private const int CompletedSynchronouslyState = 1;

    /// <summary>Indicates that the operation has completed asynchronously.</summary>
    private const int CompletedAsynchronouslyState = 2;
    #endregion

    #region Private instance variables
    /// <summary>The callback that is called when the operation completes.</summary>
    private readonly AsyncCallback asyncCallback;

    /// <summary>A user-defined object that qualifies or contains information about an asynchronous operation.</summary>
    private readonly object asyncState;

    /// <summary>The current state of the operation.</summary>
    private int currentState;

    /// <summary>Notifies any waiting thread that an event has occurred. This field may or may not be used depending on the usage of this class.</summary>
    private ManualResetEvent asyncWaitHandle;

    /// <summary>The pending exception (if any) that was thrown by the executed method.</summary>
    private Exception pendingException;

    /// <summary>The actual result that was returned by the asynchronous method.</summary>
    private T operationResult;
    #endregion

    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="AsyncResult{T}"/> class.</summary>
    /// <param name="asyncCallback">The callback that is executed when the operation completes. Leave <see langword="null"/> if not used.</param>
    /// <param name="state">A user-defined object that qualifies or contains information about an asynchronous operation. Leave 
    /// <see langword="null"/> if not used.</param>
    public AsyncResult(AsyncCallback asyncCallback, object state) {
      this.asyncCallback = asyncCallback;
      this.asyncState = state;
      this.currentState = AsyncResult<T>.PendingState;
    }
    #endregion

    #region IAsyncResult Properties
    /// <summary>Gets a value indicating whether the asynchronous operation has completed.</summary>
    public bool IsCompleted {
      get {
        /* Get the current state. Due to the asynchronous nature, Thread.VolatileRead is used. */
        return Thread.VolatileRead(ref this.currentState) != AsyncResult<T>.PendingState;
      }
    }

    /// <summary>Gets a user-defined object that qualifies or contains information about an asynchronous operation.</summary>
    public object AsyncState {
      get { return this.asyncState; }
    }

    /// <summary>Gets a value indicating whether the asynchronous operation completed synchronously.</summary>
    public bool CompletedSynchronously {
      get {
        /* Get the current state. Due to the asynchronous nature, Thread.VolatileRead is used. */
        return Thread.VolatileRead(ref this.currentState) == AsyncResult<T>.CompletedSynchronouslyState;
      }
    }

    /// <summary>Gets a <see cref="WaitHandle"/> that is used to wait for an asynchronous operation to complete.</summary>
    public WaitHandle AsyncWaitHandle {
      get {
        if(this.asyncWaitHandle == null) {
          bool done = this.IsCompleted;
          ManualResetEvent mre = new ManualResetEvent(done);
          if(Interlocked.CompareExchange(ref this.asyncWaitHandle, mre, null) != null) {
            /* Another thread created this object's event. Dispose the event that was just created. */
            mre.Close();
          }
          else if(!done && this.IsCompleted) {
            /* If the operation wasn't done when the event was created but now it is done, set the event. */
            this.asyncWaitHandle.Set();
          }
        }

        return this.asyncWaitHandle;
      }
    }
    #endregion

    #region Methods
    /// <summary>Sets the status of the asynchronous call to completed.</summary>
    /// <param name="result">The result that returned by the asynchronous operation.</param>
    /// <param name="exception">The <see cref="Exception"/> that was thrown by the executed method. If no pending Exception was thrown, pass a <see langword="null"/> 
    /// reference.</param>
    /// <param name="completedSynchronously"><see langword="true"/> if the asynchronous operation completed synchronously; otherwise, <see langword="false"/>.</param>
    /// <remarks>If the synchronous completion of the call is detected in the <see cref="AsyncCallback"/> delegate, it is probable that the thread that initiated the 
    /// asynchronous operation is the current thread.<br/>
    /// <br/>
    /// <b>Notes to Implementers:</b><br/>
    /// Most implementers of the <see cref="IAsyncResult"/> interface will not use this property and should return <see langword="false"/>.</remarks>
    public void SetAsCompleted(T result, Exception exception, bool completedSynchronously) {
      /* Set the pending exception if any */
      this.pendingException = exception;

      int newState = -1;
      if(completedSynchronously) {
        newState = AsyncResult<T>.CompletedSynchronouslyState;
      }
      else {
        newState = AsyncResult<T>.CompletedAsynchronouslyState;
      }

      int previousState = Interlocked.Exchange(ref this.currentState, newState);
      if(previousState != AsyncResult<T>.PendingState) {
        throw new InvalidOperationException("You can set a result only once");
      }

      this.operationResult = result;

      /* If someone subscribed to the event, set it */
      if(this.AsyncWaitHandle != null) {
        this.asyncWaitHandle.Set();
      }

      /* If someone subscribed to the callback, call it */
      if(this.asyncCallback != null) {
        this.asyncCallback(this);
      }
    }

    /// <summary>Ends the invocation by waiting for the wait handle to finish.</summary>
    /// <returns>The return value of the asynchronous operation.</returns>
    public T EndInvoke() {
      if(!this.IsCompleted) {
        /* Block untill the wait handle is finished */
        this.AsyncWaitHandle.WaitOne();
        /* Close and de-reference the waithandle */
        this.asyncWaitHandle.Close();
        this.asyncWaitHandle = null;
      }

      /* If an exception is pending, throw it now */
      if(this.pendingException != null) {
        throw this.pendingException;
      }
      else {
        return this.operationResult;
      }
    }
    #endregion
  }
}