using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Enkoni.Framework.Entities.Tests {
  /// <summary>A helper class to support the testcases.</summary>
  internal class CustomMappedTestDummyConfiguration : EntityTypeConfiguration<CustomMappedTestDummy> {
    /// <summary>Initializes a new instance of the <see cref="CustomMappedTestDummyConfiguration"/> class.</summary>
    internal CustomMappedTestDummyConfiguration() {
      this.ToTable("CustomMappedTestDummy");
      this.HasKey(td => td.RecordId);

      this.Property(td => td.RecordId).HasColumnName("RecordId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
      this.Property(td => td.TextValue).HasColumnName("TextValue").HasColumnType("nvarchar").HasMaxLength(100).IsRequired();
      this.Property(td => td.NumericValue).HasColumnName("NumericValue").HasColumnType("int").IsRequired();
      this.Property(td => td.BooleanValue).HasColumnName("BooleanValue").HasColumnType("bit").IsRequired();
    }
  }
}
