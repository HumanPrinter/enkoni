using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Enkoni.Framework.Entities.Tests {
  /// <summary>A helper class to support the testcases.</summary>
  internal class TestSubDummyConfiguration : EntityTypeConfiguration<TestSubDummy> {
    /// <summary>Initializes a new instance of the <see cref="TestSubDummyConfiguration"/> class.</summary>
    internal TestSubDummyConfiguration() {
      this.ToTable("TestSubDummy");
      this.HasKey(td => td.RecordId);

      this.Property(td => td.RecordId).HasColumnName("RecordId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
      this.Property(td => td.TextValue).HasColumnName("TextValue").HasColumnType("nvarchar").HasMaxLength(100).IsRequired();
    }
  }
}
