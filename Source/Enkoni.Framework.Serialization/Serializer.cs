using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Enkoni.Framework.Serialization {
  /// <summary>Provides a base class for a type that is capable of (de)serializing data to and from a specific format.</summary>
  /// <typeparam name="T">Type of the object that has to be serialized.</typeparam>
  public abstract class Serializer<T> where T : new() {
    #region Constructor

    /// <summary>Initializes a new instance of the <see cref="Serializer{T}"/> class.</summary>
    protected Serializer() {
      this.DefaultEncoding = Encoding.UTF8;
    }

    /// <summary>Initializes a new instance of the <see cref="Serializer{T}"/> class.</summary>
    /// <param name="transformer">The transformer that must be used during the (de)serialization.</param>
    protected Serializer(Transformer<T> transformer)
      : this() {
      this.Transformer = transformer;
    }

    #endregion

    #region Properties

    /// <summary>Gets or sets the default encoding that will be used when no encoding is specified.</summary>
    public Encoding DefaultEncoding { get; protected set; }

    /// <summary>Gets or sets the transformer that transforms single objects into a specific format.</summary>
    protected Transformer<T> Transformer { get; set; }

    #endregion

    #region Public asynchronous methods

    /// <summary>Begins to serialize the objects to the specified file using a default encoding.</summary>
    /// <param name="objects">The collection of objects that must be serialized.</param>
    /// <param name="filePath">The name of the output file.</param>
    /// <param name="callback">The method to be called when the asynchronous operation is completed.</param>
    /// <param name="state">A user-provided object that distinguishes this particular asynchronous request from other requests.</param>
    /// <returns>An <see cref="IAsyncResult"/> that references the asynchronous operation.</returns>
    public IAsyncResult BeginSerialize(IEnumerable<T> objects, string filePath, AsyncCallback callback, object state) {
      return this.BeginSerialize(objects, filePath, this.DefaultEncoding, callback, state);
    }

    /// <summary>Begins to serialize the objects to the specified file using the specified encoding.</summary>
    /// <param name="objects">The collection of objects that must be serialized.</param>
    /// <param name="filePath">The name of the output file.</param>
    /// <param name="encoding">The encoding that must be used to serialize the data.</param>
    /// <param name="callback">The method to be called when the asynchronous operation is completed.</param>
    /// <param name="state">A user-provided object that distinguishes this particular asynchronous request from other requests.</param>
    /// <returns>An <see cref="IAsyncResult"/> that references the asynchronous operation.</returns>
    public IAsyncResult BeginSerialize(IEnumerable<T> objects, string filePath, Encoding encoding, AsyncCallback callback, object state) {
      AsyncResult<int> result = new AsyncResult<int>(callback, state);
      SerializePropertyContainer container =
        new SerializeToFilePropertyContainer { Objects = objects, FilePath = filePath, Encoding = encoding, AsyncResult = result };

      ThreadPool.QueueUserWorkItem(this.SerializeToFileHelper, container);
      return result;
    }

    /// <summary>Begins to serialize the objects to the specified stream using a default encoding.</summary>
    /// <param name="objects">The collection of objects that must be serialized.</param>
    /// <param name="stream">The stream to which the objects must be serialized.</param>
    /// <param name="callback">The method to be called when the asynchronous operation is completed.</param>
    /// <param name="state">A user-provided object that distinguishes this particular asynchronous request from other requests.</param>
    /// <returns>An <see cref="IAsyncResult"/> that references the asynchronous operation.</returns>
    public IAsyncResult BeginSerialize(IEnumerable<T> objects, Stream stream, AsyncCallback callback, object state) {
      return this.BeginSerialize(objects, stream, this.DefaultEncoding, callback, state);
    }

    /// <summary>Begins to serialize the objects to the specified stream using the specified encoding.</summary>
    /// <param name="objects">The collection of objects that must be serialized.</param>
    /// <param name="stream">The stream to which the objects must be serialized.</param>
    /// <param name="encoding">The encoding that must be used to serialize the data.</param>
    /// <param name="callback">The method to be called when the asynchronous operation is completed.</param>
    /// <param name="state">A user-provided object that distinguishes this particular asynchronous request from other requests.</param>
    /// <returns>An <see cref="IAsyncResult"/> that references the asynchronous operation.</returns>
    public IAsyncResult BeginSerialize(IEnumerable<T> objects, Stream stream, Encoding encoding, AsyncCallback callback, object state) {
      AsyncResult<int> result = new AsyncResult<int>(callback, state);
      SerializePropertyContainer container =
        new SerializeToStreamPropertyContainer { Objects = objects, Stream = stream, Encoding = encoding, AsyncResult = result };

      ThreadPool.QueueUserWorkItem(this.SerializeToStreamHelper, container);
      return result;
    }

    /// <summary>Waits for the pending asynchronous serialize operation to complete. If an exception was thrown during the execution of the
    /// asynchronous operation, it is rethrown when invoking this method.</summary>
    /// <param name="asyncResult">The reference to the pending asynchronous request to wait for.</param>
    /// <returns>The number of bytes that have been written to the file or stream.</returns>
    public int EndSerialize(IAsyncResult asyncResult) {
      if(asyncResult == null) {
        throw new ArgumentNullException("asyncResult");
      }

      AsyncResult<int> result = asyncResult as AsyncResult<int>;
      if(result == null) {
        throw new ArgumentException("The specified IAsyncResult was not of the expected type AsyncResult<int>", "asyncResult");
      }

      int operationresult = result.EndInvoke();
      return operationresult;
    }

    /// <summary>Begins to deserialize the objects from the specified file using a default encoding.</summary>
    /// <param name="filePath">The name of the input file.</param>
    /// <param name="callback">The method to be called when the asynchronous operation is completed.</param>
    /// <param name="state">A user-provided object that distinguishes this particular asynchronous request from other requests.</param>
    /// <returns>An <see cref="IAsyncResult"/> that references the asynchronous operation.</returns>
    public IAsyncResult BeginDeserialize(string filePath, AsyncCallback callback, object state) {
      return this.BeginDeserialize(filePath, this.DefaultEncoding, callback, state);
    }

    /// <summary>Begins to deserialize the objects from the specified file using the specified encoding.</summary>
    /// <param name="filePath">The name of the input file.</param>
    /// <param name="encoding">The encoding that must be used to deserialize the data.</param>
    /// <param name="callback">The method to be called when the asynchronous operation is completed.</param>
    /// <param name="state">A user-provided object that distinguishes this particular asynchronous request from other requests.</param>
    /// <returns>An <see cref="IAsyncResult"/> that references the asynchronous operation.</returns>
    public IAsyncResult BeginDeserialize(string filePath, Encoding encoding, AsyncCallback callback, object state) {
      AsyncResult<ICollection<T>> result = new AsyncResult<ICollection<T>>(callback, state);
      DeserializePropertyContainer container =
        new DeserializeFromFilePropertyContainer { FilePath = filePath, Encoding = encoding, AsyncResult = result };

      ThreadPool.QueueUserWorkItem(this.DeserializeToFileHelper, container);
      return result;
    }

    /// <summary>Begins to deserialize the objects from the specified stream using a default encoding.</summary>
    /// <param name="stream">The stream to which the objects must be serialized.</param>
    /// <param name="callback">The method to be called when the asynchronous operation is completed.</param>
    /// <param name="state">A user-provided object that distinguishes this particular asynchronous request from other requests.</param>
    /// <returns>An <see cref="IAsyncResult"/> that references the asynchronous operation.</returns>
    public IAsyncResult BeginDeserialize(Stream stream, AsyncCallback callback, object state) {
      return this.BeginDeserialize(stream, this.DefaultEncoding, callback, state);
    }

    /// <summary>Begins to deserialize the objects from the specified stream using the specified encoding.</summary>
    /// <param name="stream">The stream to which the objects must be serialized.</param>
    /// <param name="encoding">The encoding that must be used to serialize the data.</param>
    /// <param name="callback">The method to be called when the asynchronous operation is completed.</param>
    /// <param name="state">A user-provided object that distinguishes this particular asynchronous request from other requests.</param>
    /// <returns>An <see cref="IAsyncResult"/> that references the asynchronous operation.</returns>
    public IAsyncResult BeginDeserialize(Stream stream, Encoding encoding, AsyncCallback callback, object state) {
      AsyncResult<ICollection<T>> result = new AsyncResult<ICollection<T>>(callback, state);
      DeserializePropertyContainer container =
        new DeserializeFromStreamPropertyContainer { Stream = stream, Encoding = encoding, AsyncResult = result };

      ThreadPool.QueueUserWorkItem(this.DeserializeToStreamHelper, container);
      return result;
    }

    /// <summary>Waits for the pending asynchronous deserialize operation to complete. If an exception was thrown during the execution of the
    /// asynchronous operation, it is rethrown when invoking this method.</summary>
    /// <param name="asyncResult">The reference to the pending asynchronous request to wait for.</param>
    /// <returns>The items that were deserialized from the file or stream.</returns>
    public ICollection<T> EndDeserialize(IAsyncResult asyncResult) {
      if(asyncResult == null) {
        throw new ArgumentNullException("asyncResult");
      }

      AsyncResult<ICollection<T>> result = asyncResult as AsyncResult<ICollection<T>>;
      if(result == null) {
        throw new ArgumentException("The specified IAsyncResult was not of the expected type AsyncResult<ICollection<T>>", "asyncResult");
      }

      ICollection<T> operationresult = result.EndInvoke();
      return operationresult;
    }

    #endregion

    #region Public synchronous methods

    /// <summary>Serializes a list of objects to a file using a default encoding.</summary>
    /// <param name="objects">The collection of objects that must be serialized.</param>
    /// <param name="filePath">The name of the output file.</param>
    /// <returns>The number of bytes that have been written to the file.</returns>
    /// <exception cref="ArgumentNullException">The parameter is null.</exception>
    /// <exception cref="ArgumentException">The file path is empty or contains illegal characters.</exception>
    public int Serialize(IEnumerable<T> objects, string filePath) {
      return this.Serialize(objects, filePath, this.DefaultEncoding);
    }

    /// <summary>Serializes a list of objects to a file.</summary>
    /// <param name="objects">The collection of objects that must be serialized.</param>
    /// <param name="filePath">The name of the output file.</param>
    /// <param name="encoding">The encoding that must be used to serialize the data.</param>
    /// <returns>The number of bytes that have been written to the file.</returns>
    /// <exception cref="ArgumentNullException">The parameter is null.</exception>
    /// <exception cref="ArgumentException">The file path is empty or contains illegal characters.</exception>
    public int Serialize(IEnumerable<T> objects, string filePath, Encoding encoding) {
      /* Validate the parameters */
      Guard.ArgumentIsNotNull(objects, nameof(objects), "The parameter cannot be null.");
      Guard.ArgumentIsNotNull(filePath, nameof(filePath), "The parameter cannot be null.");

      Guard.ArgumentIsNotNullOrEmpty(filePath.Trim(), nameof(filePath), "The file path cannot be null or empty.");
      Guard.ArgumentIsValidPath(filePath, nameof(filePath), "The file path contains illegal characters.");

      int byteCount = -1;
      using(StreamWriter fileWriter = new StreamWriter(filePath, false, encoding)) {
        /* Write an empty string to the stream. This enforces the output of the encoding-bytes for the fileheader */
        fileWriter.Write(string.Empty);
        fileWriter.Flush();

        byteCount = this.Serialize(objects, fileWriter.BaseStream, encoding);
      }

      return byteCount;
    }

    /// <summary>Serializes a list of objects to a stream using a default encoding.</summary>
    /// <param name="objects">The collection of objects that must be serialized.</param>
    /// <param name="stream">The stream to which the objects must be serialized.</param>
    /// <returns>The number of bytes that have been written to the stream.</returns>
    /// <exception cref="ArgumentNullException">The parameter is null.</exception>
    /// <exception cref="ArgumentException">The stream is read-only.</exception>
    public int Serialize(IEnumerable<T> objects, Stream stream) {
      return this.Serialize(objects, stream, this.DefaultEncoding);
    }

    /// <summary>Serializes a list of objects to a stream.</summary>
    /// <param name="objects">The collection of objects that must be serialized.</param>
    /// <param name="stream">The stream to which the objects must be serialized.</param>
    /// <param name="encoding">The encoding that must be used to serialize the data.</param>
    /// <returns>The number of bytes that have been written to the stream.</returns>
    /// <exception cref="ArgumentNullException">The parameter is null.</exception>
    /// <exception cref="ArgumentException">The stream is read-only.</exception>
    public int Serialize(IEnumerable<T> objects, Stream stream, Encoding encoding) {
      /* Validate the parameters */
      Guard.ArgumentIsNotNull(objects, nameof(objects), "The parameter cannot be null.");
      Guard.ArgumentIsNotNull(encoding, nameof(encoding), "The parameter cannot be null.");
      Guard.ArgumentIsNotNull(stream, nameof(stream), "The parameter cannot be null.");

      if(!stream.CanWrite) {
        throw new ArgumentException("Specified stream is not writable.", "stream");
      }

      return this.Serialize(objects, encoding, stream);
    }

    /// <summary>Deserializes a CSV to a list of objects using a default encoding of UTF-8.</summary>
    /// <param name="filePath">File path to csv file.</param>
    /// <returns>List with objects.</returns>
    public ICollection<T> Deserialize(string filePath) {
      return this.Deserialize(filePath, this.DefaultEncoding);
    }

    /// <summary>Deserializes a CSV to a list of objects.</summary>
    /// <param name="filePath">File path to csv file.</param>
    /// <param name="encoding">The encoding that must be used to deserialize the data.</param>
    /// <returns>List with objects.</returns>
    public ICollection<T> Deserialize(string filePath, Encoding encoding) {
            /* Validate the parameters */
      Guard.ArgumentIsNotNullOrEmpty(filePath?.Trim(), nameof(filePath), "The parameter cannot be null or empty");
      Guard.ArgumentIsNotNull(encoding, nameof(encoding), "The parameter cannot be null");
      Guard.ArgumentIsValidPath(filePath, nameof(filePath), "The file path contains illegal characters");

      /* Deserialize the data */
      ICollection<T> result = null;
      using(StreamReader fileReader = new StreamReader(filePath, encoding)) {
        result = this.Deserialize(fileReader.BaseStream, encoding);
      }

      return result;
    }

    /// <summary>Deserializes CSV data to a list of objects using a default encoding of UTF-8.</summary>
    /// <param name="stream">The stream that contains the csv data.</param>
    /// <returns>List with objects.</returns>
    public ICollection<T> Deserialize(Stream stream) {
      return this.Deserialize(stream, this.DefaultEncoding);
    }

    /// <summary>Deserializes a CSV to a list of objects.</summary>
    /// <param name="stream">The stream that contains the csv data.</param>
    /// <param name="encoding">The encoding that must be used to deserialize the data.</param>
    /// <returns>List with objects.</returns>
    public ICollection<T> Deserialize(Stream stream, Encoding encoding) {
      /* Validate the parameters */
      Guard.ArgumentIsNotNull(encoding, nameof(encoding), "The parameter cannot be null");
      Guard.ArgumentIsNotNull(stream, nameof(stream), "The parameter cannot be null");

      if(!stream.CanRead) {
        throw new ArgumentException("The specified stream is not readable", nameof(stream));
      }

      /* Deserialize the data */
      using(StreamReader reader = new StreamReader(stream, encoding)) {
        return this.Deserialize(reader);
      }
    }

    #endregion

    #region Protected methods

    /// <summary>Serializes a collection of items by transforming each item using the <see cref="Transformer"/> property and writing the
    /// transformed item to the <paramref name="stream"/>. Each item will be separated using the new line character(s) of the current environment.
    /// </summary>
    /// <param name="objects">The objects that must be serialized.</param>
    /// <param name="encoding">The encoding that must be used.</param>
    /// <param name="stream">The stream to which the serialized items must be sent.</param>
    /// <returns>The number of bytes that have been written to the stream.</returns>
    protected virtual int Serialize(IEnumerable<T> objects, Encoding encoding, Stream stream) {
      if(objects.Count() == 0) {
        return 0;
      }

      byte[] newLine = encoding.GetBytes(Environment.NewLine);
      IEnumerator<T> enumerator = objects.GetEnumerator();
      int writeCount = 0;

      bool proceed = enumerator.MoveNext();
      while(proceed) {
        byte[] bytes = this.Transformer.ToBytes(enumerator.Current, encoding);
        stream.Write(bytes, 0, bytes.Length);
        writeCount += bytes.Length;
        proceed = enumerator.MoveNext();
        if(proceed) {
          stream.Write(newLine, 0, newLine.Length);
          writeCount += newLine.Length;
        }
      }

      return writeCount;
    }

    /// <summary>Deserializes a collection of objects using the data that is accessible through the specified stream reader.</summary>
    /// <param name="reader">The object that gives access to the underlying stream.</param>
    /// <returns>The collection of deserialized objects.</returns>
    protected virtual ICollection<T> Deserialize(StreamReader reader) {
      Guard.ArgumentIsNotNull(reader, nameof(reader));

      ICollection<T> objList = new List<T>();

      while(!reader.EndOfStream) {
        string line = reader.ReadLine();
        T obj = this.Transformer.FromString(line);
        objList.Add(obj);
      }

      return objList;
    }

    #endregion

    #region Private methods

    /// <summary>Executes the <see cref="Serialize(IEnumerable{T},string,Encoding)"/> method in a separate thread. This is used to support
    /// asynchronous operations.</summary>
    /// <param name="propertyContainer">The object that holds properties that are required for the asynchronous operation.</param>
    private void SerializeToFileHelper(object propertyContainer) {
      Guard.ArgumentIsOfType<SerializeToFilePropertyContainer>(propertyContainer, nameof(propertyContainer), "The specified object was not of the expected type SerializeToFilePropertyContainer");
      SerializeToFilePropertyContainer container = propertyContainer as SerializeToFilePropertyContainer;

      try {
        int result = this.Serialize(container.Objects, container.FilePath, container.Encoding);
        container.AsyncResult.SetAsCompleted(result, null, false);
      }
      catch(Exception ex) {
        /* We deliberately catch every exception. It is up to the caller of EndSerialize() to do a more fine-grained exception handling */
        container.AsyncResult.SetAsCompleted(-1, ex, false);
      }
    }

    /// <summary>Executes the <see cref="Serialize(IEnumerable{T},Stream,Encoding)"/> method in a separate thread. This is used to support
    /// asynchronous operations.</summary>
    /// <param name="propertyContainer">The object that holds properties that are required for the asynchronous operation.</param>
    private void SerializeToStreamHelper(object propertyContainer) {
      Guard.ArgumentIsOfType<SerializeToStreamPropertyContainer>(propertyContainer, nameof(propertyContainer), "The specified object was not of the expected type SerializeToStreamPropertyContainer");
      SerializeToStreamPropertyContainer container = propertyContainer as SerializeToStreamPropertyContainer;

      try {
        int result = this.Serialize(container.Objects, container.Stream, container.Encoding);
        container.AsyncResult.SetAsCompleted(result, null, false);
      }
      catch(Exception ex) {
        /* We deliberately catch every exception. It is up to the caller of EndSerialize() to do a more fine-grained exception handling */
        container.AsyncResult.SetAsCompleted(-1, ex, false);
      }
    }

    /// <summary>Executes the <see cref="Deserialize(string,Encoding)"/> method in a separate thread. This is used to support asynchronous
    /// operations.</summary>
    /// <param name="propertyContainer">The object that holds properties that are required for the asynchronous operation.</param>
    private void DeserializeToFileHelper(object propertyContainer) {
      Guard.ArgumentIsOfType<DeserializeFromFilePropertyContainer>(propertyContainer, nameof(propertyContainer), "The specified object was not of the expected type DeserializeFromFilePropertyContainer");
      DeserializeFromFilePropertyContainer container = propertyContainer as DeserializeFromFilePropertyContainer;

      try {
        ICollection<T> result = this.Deserialize(container.FilePath, container.Encoding);
        container.AsyncResult.SetAsCompleted(result, null, false);
      }
      catch(Exception ex) {
        /* We deliberately catch every exception. It is up to the caller of EndSerialize() to do a more fine-grained exception handling */
        container.AsyncResult.SetAsCompleted(null, ex, false);
      }
    }

    /// <summary>Executes the <see cref="Deserialize(Stream,Encoding)"/> method in a separate thread. This is used to support asynchronous
    /// operations.</summary>
    /// <param name="propertyContainer">The object that holds properties that are required for the asynchronous operation.</param>
    private void DeserializeToStreamHelper(object propertyContainer) {
      Guard.ArgumentIsOfType<DeserializeFromStreamPropertyContainer>(propertyContainer, nameof(propertyContainer), "The specified object was not of the expected type DeserializeFromStreamPropertyContainer");
      DeserializeFromStreamPropertyContainer container = propertyContainer as DeserializeFromStreamPropertyContainer;

      try {
        ICollection<T> result = this.Deserialize(container.Stream, container.Encoding);
        container.AsyncResult.SetAsCompleted(result, null, false);
      }
      catch(Exception ex) {
        /* We deliberately catch every exception. It is up to the caller of EndSerialize() to do a more fine-grained exception handling */
        container.AsyncResult.SetAsCompleted(null, ex, false);
      }
    }

    #endregion

    #region Private classes

    /// <summary>Defines a base container that holds the properties that are required during the serialization of objects.</summary>
    private class SerializePropertyContainer {
      /// <summary>Gets or sets the objects that must be serialized.</summary>
      internal IEnumerable<T> Objects { get; set; }

      /// <summary>Gets or sets the encoding that must be used.</summary>
      internal Encoding Encoding { get; set; }

      /// <summary>Gets or sets the asynchronous result object.</summary>
      internal AsyncResult<int> AsyncResult { get; set; }
    }

    /// <summary>Defines a base container that holds the properties that are required during the deserialization of objects.</summary>
    private class DeserializePropertyContainer {
      /// <summary>Gets or sets the encoding that must be used.</summary>
      internal Encoding Encoding { get; set; }

      /// <summary>Gets or sets the asynchronous result object.</summary>
      internal AsyncResult<ICollection<T>> AsyncResult { get; set; }
    }

    /// <summary>Defines a container that holds the properties that are required during the serialization of objects to a file.</summary>
    private class SerializeToFilePropertyContainer : SerializePropertyContainer {
      /// <summary>Gets or sets the path to the file that must be used.</summary>
      internal string FilePath { get; set; }
    }

    /// <summary>Defines a container that holds the properties that are required during the deserialization of objects from a file.</summary>
    private class DeserializeFromFilePropertyContainer : DeserializePropertyContainer {
      /// <summary>Gets or sets the path to the file that must be used.</summary>
      internal string FilePath { get; set; }
    }

    /// <summary>Defines a container that holds the properties that are required during the serialization of objects to a stream.</summary>
    private class SerializeToStreamPropertyContainer : SerializePropertyContainer {
      /// <summary>Gets or sets the stream that must be used.</summary>
      internal Stream Stream { get; set; }
    }

    /// <summary>Defines a container that holds the properties that are required during the deserialization of objects from a stream.</summary>
    private class DeserializeFromStreamPropertyContainer : DeserializePropertyContainer {
      /// <summary>Gets or sets the stream that must be used.</summary>
      internal Stream Stream { get; set; }
    }
    #endregion
  }
}
