using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using PortfolioBackend.Data;

#nullable disable

namespace PortfolioBackend.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20260826172000_AddExploringSettings")]
public sealed class AddExploringSettings : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>("AvailabilityText", "ProfileSettings", "text", nullable: false, defaultValue: "");
        migrationBuilder.AddColumn<bool>("ExploringEnabled", "ProfileSettings", "boolean", nullable: false, defaultValue: true);
        migrationBuilder.AddColumn<string>("ExploringItems", "ProfileSettings", "text", nullable: false, defaultValue: "");
        migrationBuilder.AddColumn<string>("ExploringStatus", "ProfileSettings", "text", nullable: false, defaultValue: "Learning");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn("AvailabilityText", "ProfileSettings");
        migrationBuilder.DropColumn("ExploringEnabled", "ProfileSettings");
        migrationBuilder.DropColumn("ExploringItems", "ProfileSettings");
        migrationBuilder.DropColumn("ExploringStatus", "ProfileSettings");
    }
}
