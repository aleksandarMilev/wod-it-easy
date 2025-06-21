namespace WodItEasy.Common.Infrastructure.Configuration
{
    using Domain.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            const string SerializedDataPropertyName = "serializedData";

            builder
                .HasKey(m => m.Id);

            builder
                .Property<string>(SerializedDataPropertyName)
                .IsRequired()
                .HasField(SerializedDataPropertyName);

            builder
                .Property(m => m.Type)
                .IsRequired()
                .HasConversion(
                    t => t.AssemblyQualifiedName,
                    t => Type.GetType(t!)!);

            builder
                .Ignore(m => m.Data);
        }
    }
}
