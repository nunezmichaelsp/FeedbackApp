namespace FeedbackApp.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

    /// <inheritdoc />
    public partial class SetupSuperAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "6160a999-3723-4f01-86c5-6b52e66cb7b3", "superadmin@domain.com", true, false, null, "SUPERADMIN@DOMAIN.COM", "SUPERADMIN@DOMAIN.COM", "AQAAAAIAAYagAAAAEAOQqVeVQDz+BD+9KX9TTlohw2dOorrsOgYbTeDiYE5xDuDR9TT5PaXwJDMKj5Y6Nw==", null, false, "2361e5d4910a42dc9848a6c2b502f592", false, "superadmin@domain.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9");
        }
    }
}
