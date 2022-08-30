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
                    table.PrimaryKey("PK_UserChtatrooms", x => new { x.ChatroomId, x.UserId });
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
                        principalColumns: new[] { "ChatroomId", "UserId" });
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
                    { 1, true, "AQAAAAEAACcQAAAAEEgANxKNg2hy8oRXUngzt5P9L6vUxxdUt3buOSq5Hc0vUgSJKpNgMR3HftgMpLi2/w==", "Rick Sanchez" },
                    { 2, true, "AQAAAAEAACcQAAAAEP5Ojugck10+AbXTZQxeyLUuFSy8Y5kM2gYeVhsNPLQC7A9TQoOAFY8E9Ge7Fj/rVw==", "Morty Smith" },
                    { 3, true, "AQAAAAEAACcQAAAAEKZdTehwvBrssXgaE5LDrgDELH48cMDE5v8KgTSV8spacVPnxxmLPkT88U+mRTiGWg==", "Jerry Smith" },
                    { 4, true, "AQAAAAEAACcQAAAAEPRREPLCu1pII4tq95H5lnBl8ioHqeg2CRc9gMr8ITfsfGOEbb0VnrZvaGad6KJMfw==", "Walter White" },
                    { 5, true, "AQAAAAEAACcQAAAAEJXi4SgxNPpG2fZjiNkjPOP4lpoeKeqEEXKLXVwnFxI7trsqCOpSGgJNR7i+sYferA==", "Jesse Pinkman" },
                    { 6, true, "AQAAAAEAACcQAAAAEKzsKNG2MVOfKfRSe8b06gADF9ttDfZ1Xkl/RNTNYz1P/GdnvMrgyI+qbCeYRQMG9A==", "Saul Goodman" }
                });

            migrationBuilder.InsertData(
                table: "UserChtatrooms",
                columns: new[] { "ChatroomId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 2, 3 },
                    { 3, 4 },
                    { 3, 5 },
                    { 4, 4 },
                    { 4, 6 },
                    { 5, 5 },
                    { 5, 6 },
                    { 6, 1 },
                    { 6, 2 },
                    { 6, 3 },
                    { 7, 4 },
                    { 7, 5 },
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
                name: "IX_UserChtatrooms_UserId",
                table: "UserChtatrooms",
                column: "UserId");
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
