using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel.Unity;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Rhino.Mocks;

namespace Enkoni.Framework.Logging.Tests {
  /// <summary>Tests he functionality of the <see cref="LogManager"/> class.</summary>
  [TestClass]
  public class LogManagerTest {
    /// <summary>Tests the functionality of the <see cref="LogManager.CreateLogger()"/> method.</summary>
    [TestMethod]
    public void CreateLogger_WithConfiguredLogWriter_LogEntryIsForwardedToConfiguredLogWriter() {
      /* Create a mocked logwriter */
      LogWriter mockedLogWriter = MockRepository.GenerateMock<LogWriter>();

      /* Build up the EnterpriseLibraryContainer using Unity for the IoC */
      IUnityContainer container = new UnityContainer();
      container.RegisterInstance<LogWriter>("logwriter", mockedLogWriter); /* Any name will do */

      UnityContainerConfigurator configurator = new UnityContainerConfigurator(container);
      IConfigurationSource configSource = new DictionaryConfigurationSource();
      EnterpriseLibraryContainer.ConfigureContainer(configurator, configSource);
      IServiceLocator locator = new UnityServiceLocator(container);
      EnterpriseLibraryContainer.Current = locator;

      /* Call the test subject */
      Logger result = LogManager.CreateLogger();
      
      /* Make sure the returned logger is not null... */
      Assert.IsNotNull(result);

      /* ...and that the returned logger is working with the configured LogWriter */
      LogEntry entry = new LogEntry("message", "category", 1, 1000, System.Diagnostics.TraceEventType.Information, "title", null);
      result.Log(entry);
      mockedLogWriter.AssertWasCalled(lw => lw.Write(Arg.Is(entry)));
    }

    /// <summary>Tests the functionality of the <see cref="LogManager.CreateLogger()"/> method when no <see cref="LogWriter"/> is configured.</summary>
    [TestMethod]
    public void CreateLogger_NoConfiguredLogWriter_NoExceptionIsThrown() {
      /* Call the test subject */
      Logger result = LogManager.CreateLogger();

      /* Make sure the returned logger is not null... */
      Assert.IsNotNull(result);

      /* ...and that the returned logger is working with the configured LogWriter */
      LogEntry entry = new LogEntry("message", "category", 1, 1000, System.Diagnostics.TraceEventType.Information, "title", null);
      result.Log(entry);
      
      /* No additional assertions. If no exception is thrown at this point, the test is complete */
    }
  }
}
