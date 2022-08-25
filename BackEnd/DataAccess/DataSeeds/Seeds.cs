using Core.Entities;
using Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.DataSeeds;

public class Seeds
{
    private readonly IPasswordHasher<User> _passwordHasher;

    public Seeds()
    {
        _passwordHasher = new PasswordHasher<User>();
    }

    public IEnumerable<User> GetUsers()
    {
        const string password = "HelloWorld123";
        var result = new List<User>()
        {
            new User()
            {
                Id = 1,
                Username = "Rick Sanchez",
                IsActive = true
            },
            new User()
            {
                Id = 2,
                Username = "Morty Smith",
                IsActive = true
            },
            new User()
            {
                Id = 3,
                Username = "Jerry Smith",
                IsActive = true
            },
            new User()
            {
                Id = 4,
                Username = "Walter White",
                IsActive = true
            },
            new User()
            {
                Id = 5,
                Username = "Jesse Pinkman",
                IsActive = true
            },
            new User()
            {
                Id = 6,
                Username = "Saul Goodman",
                IsActive = true
            }
        };
        
        result.ForEach(u => u.PasswordHash = _passwordHasher.HashPassword(u, password));
        return result;
    }

    public IEnumerable<Chatroom> GetChatrooms() => new List<Chatroom>()
    {
        new Chatroom() // Rick & Morty
        {
            Id = 1,
            Type = ChatType.Private
        },
        new Chatroom() // Morty & Jerry
        {
            Id = 2,
            Type = ChatType.Private
        },
        new Chatroom() // Walter & Jesse
        {
            Id = 3,
            Type = ChatType.Private
        },
        new Chatroom() // Walter & Saul
        {
            Id = 4,
            Type = ChatType.Private
        },
        new Chatroom() // Jesse & Saul
        {
            Id = 5,
            Type = ChatType.Private
        },
        new Chatroom() // Rick and Morty characters
        {
            Id = 6,
            Name = "Rick and Morty group",
            Type = ChatType.Group
        },
        new Chatroom() // Breaking Bad character
        {
            Id = 7,
            Name = "Braking Bad group",
            Type = ChatType.Group
        }
    };

    public IEnumerable<UserChatroom> GetUserChatrooms() => new List<UserChatroom>()
    {
        // Rick & Morty private
        new UserChatroom() {ChatroomId = 1, UserId = 1},
        new UserChatroom() {ChatroomId = 1, UserId = 2},
        // Morty & Jerry private
        new UserChatroom() {ChatroomId = 2, UserId = 2},
        new UserChatroom() {ChatroomId = 2, UserId = 3},
        // Walter & Jesse private
        new UserChatroom() {ChatroomId = 3, UserId = 4},
        new UserChatroom() {ChatroomId = 3, UserId = 5},
        // Walter & Saul private
        new UserChatroom() {ChatroomId = 4, UserId = 4},
        new UserChatroom() {ChatroomId = 4, UserId = 6},
        // Jesse & Saul private
        new UserChatroom() {ChatroomId = 5, UserId = 5},
        new UserChatroom() {ChatroomId = 5, UserId = 6},
        // Rick & Morty group
        new UserChatroom() {ChatroomId = 6, UserId = 1},
        new UserChatroom() {ChatroomId = 6, UserId = 2},
        new UserChatroom() {ChatroomId = 6, UserId = 3},
        // Breaking Bad group
        new UserChatroom() {ChatroomId = 7, UserId = 4},
        new UserChatroom() {ChatroomId = 7, UserId = 5},
        new UserChatroom() {ChatroomId = 7, UserId = 6}
    };
}