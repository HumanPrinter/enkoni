using AutoMapper;

namespace Enkoni.Framework.Entities.Tests {
  internal class AutoMapperConfiguration {
    #region Public static methods
    /// <summary>Initializes the mapping configuration.</summary>
    public static void Initialize() {
      Mapper.Initialize(mappingConfiguration => {
        ConfigureCustomMappedTestDummyMapping(mappingConfiguration);
      });

      Mapper.AssertConfigurationIsValid();
    }
    #endregion

    #region Private static methods
    /// <summary>Configures the mapping from <see cref="CustomMappedTestDummy"/> to <see cref="CustomMappedTestDummy"/>.</summary>
    /// <param name="mappingConfiguration">The object that holds the AutoMapper configuration.</param>
    private static void ConfigureCustomMappedTestDummyMapping(IConfiguration mappingConfiguration) {
      mappingConfiguration.CreateMap<CustomMappedTestDummy, CustomMappedTestDummy>()
        .ForMember(dest => dest.TextValue, source => source.MapFrom(opt => opt.TextValue + "_mapped"));
    }
    #endregion
  }
}
