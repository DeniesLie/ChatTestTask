using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DataSeeds;

public static class DataSeeder
{
    public static void SeedData(this ModelBuilder modelBuilder)
    {
        var seeds = new Seeds();
        modelBuilder.Entity<User>().HasData(seeds.GetUsers());
        modelBuilder.Entity<Chatroom>().HasData(seeds.GetChatrooms());
        modelBuilder.Entity<UserChatroom>().HasData(seeds.GetUserChatrooms());
    }
}