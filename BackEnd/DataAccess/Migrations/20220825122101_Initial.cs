using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chatrooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chatrooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserChtatrooms",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ChatroomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChtatrooms", x => new { x.UserId, x.ChatroomId });
                    table.ForeignKey(
                        name: "FK_UserChtatrooms_Chatrooms_ChatroomId",
                        column: x => x.ChatroomId,
                        principalTable: "Chatrooms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserChtatrooms_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: true),
                    SentAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsEdited = table.Column<bool>(type: "bit", nullable: false),
                    IsDeletedForSender = table.Column<bool>(type: "bit", nullable: false),
                    IsDeletedForEveryone = table.Column<bool>(type: "bit", nullable: false),
                    RepliedMessageId = table.Column<int>(type: "int", nullable: true),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    ChatroomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Messages_RepliedMessageId",
                        column: x => x.RepliedMessageId,
                        principalTable: "Messages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Messages_UserChtatrooms_ChatroomId_SenderId",
                        columns: x => new { x.ChatroomId, x.SenderId },
                        principalTable: "UserChtatrooms",
                        principalColumns: new[] { "UserId", "ChatroomId" });
                });

            migrationBuilder.InsertData(
                table: "Chatrooms",
                columns: new[] { "Id", "Name", "Type" },
                values: new object[,]
                {
                    { 1, null, 0 },
                    { 2, null, 0 },
                    { 3, null, 0 },
                    { 4, null, 0 },
                    { 5, null, 0 },
                    { 6, "Rick and Morty group", 1 },
                    { 7, "Braking Bad group", 1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "IsActive", "PasswordHash", "Username" },
                values: new object[,]
                {
                    { 1, true, "AQAAAAEAACcQAAAAEEKgEY9POY3wYUKMQNkypFFtPuR7LePDw0PvLQMe2L3ZMEt4QaL6XPTuUsAOFtjlIQ==", "Rick Sanchez" },
                    { 2, true, "AQAAAAEAACcQAAAAENl6gcU0usQADYG8wNtF/eaN+VWDQsHE0wjrxlsz0Dk4OTJq+z3MmAV8Neg4ENO8Ng==", "Morty Smith" },
                    { 3, true, "AQAAAAEAACcQAAAAEDkUuZ+EL11qvW8yXp9Su78otx+XV5mQeiItxG/cmwM0j2Fjj2Q/cLV1FVrF1VF6Rw==", "Jerry Smith" },
                    { 4, true, "AQAAAAEAACcQAAAAEPyqGa96o35hzkVZp695Tw7THbbHqXOb8O03CXy2VSVCIF9wlJvXC2Szq8I9Wa1qLg==", "Walter White" },
                    { 5, true, "AQAAAAEAACcQAAAAEApHyhGY2e7DO6BulwKHw+9StQKfVMX3K2fVLxiTe/EtcJyLPsitjCpEEMkZv7xVCg==", "Jesse Pinkman" },
                    { 6, true, "AQAAAAEAACcQAAAAEFmcSP4756peXtybrp0ob1IBxMeBnnZy/t2ZxIf+xHAmk7DpfohjkmlDkvRPd9yVzw==", "Saul Goodman" }
                });

            migrationBuilder.InsertData(
                table: "UserChtatrooms",
                columns: new[] { "ChatroomId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 6, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 6, 2 },
                    { 2, 3 },
                    { 6, 3 },
                    { 3, 4 },
                    { 4, 4 },
                    { 7, 4 },
                    { 3, 5 },
                    { 5, 5 },
                    { 7, 5 },
                    { 4, 6 },
                    { 5, 6 },
                    { 7, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatroomId_SenderId",
                table: "Messages",
                columns: new[] { "ChatroomId", "SenderId" });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_RepliedMessageId",
                table: "Messages",
                column: "RepliedMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChtatrooms_ChatroomId",
                table: "UserChtatrooms",
                column: "ChatroomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "UserChtatrooms");

            migrationBuilder.DropTable(
                name: "Chatrooms");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
