using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(m => m.Id);

        builder
            .Property(m => m.Text).HasMaxLength(600);

        builder.HasOne(m => m.UserChatroom)
            .WithMany(s => s.Messages)
            .HasForeignKey(m => new { m.ChatroomId, m.SenderId })
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(m => m.RepliedMessage)
            .WithMany(rm => rm.Replies)
            .HasForeignKey(m => m.RepliedMessageId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Ignore(m => m.SenderName);

        builder.ToTable("Messages");
    }
}