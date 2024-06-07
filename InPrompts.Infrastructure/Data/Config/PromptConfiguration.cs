using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using InPrompts.Core;

namespace InPrompts.Infrastructure;
public class PromptConfiguration : IEntityTypeConfiguration<Prompt>
{
    public void Configure(EntityTypeBuilder<Prompt> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Text)
            .HasMaxLength(DataSchemaConstants.DEFAULT_TEXT_LENGTH)
            .IsRequired();
    }
}