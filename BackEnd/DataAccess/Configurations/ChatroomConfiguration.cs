using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class ChatroomConfiguration : IEntityTypeConfiguration<Chatroom>
{
    public void Configure(EntityTypeBuilder<Chatroom> builder)
    {
        builder.HasKey(room => room.Id);

        builder.ToTable("Chatrooms");
    }
}