using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Enkoni.Framework.Entities.Database.Bugs {
  /// <summary>A helper class to support the testcases.</summary>
  internal class TestDummyConfiguration : EntityTypeConfiguration<TestDummy> {
    /// <summary>Initializes a new instance of the <see cref="TestDummyConfiguration"/> class.</summary>
    internal TestDummyConfiguration() {
      this.ToTable("TestDummy");
      this.HasKey(env => env.RecordId);

      this.Property(env => env.RecordId).HasColumnName("RecordId").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
      this.Property(env => env.TextValue).HasColumnName("TextValue").HasColumnType("nvarchar").HasMaxLength(100).IsRequired();
      this.Property(env => env.NumericValue).HasColumnName("NumericValue").HasColumnType("int").IsRequired();
      this.Property(env => env.BooleanValue).HasColumnName("BooleanValue").HasColumnType("bit").IsRequired();
    }
  }
}
