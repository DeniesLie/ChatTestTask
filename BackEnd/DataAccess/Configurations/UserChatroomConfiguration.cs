using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class UserChatroomConfiguration : IEntityTypeConfiguration<UserChatroom>
{
    public void Configure(EntityTypeBuilder<UserChatroom> builder)
    {
        builder.HasKey(userchatRoom => new { userchatRoom.ChatroomId, userchatRoom.UserId });

        builder.HasOne(userChatRoom => userChatRoom.User)
            .WithMany(u => u.UserChatrooms)
            .HasForeignKey(userChatRoom => userChatRoom.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(userChatRoom => userChatRoom.Chatroom)
            .WithMany(cr => cr.UserChatrooms)
            .HasForeignKey(userChatRoom => userChatRoom.ChatroomId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.ToTable("UserChtatrooms");
    }
}