using asp.net.core.angular.Persistence;
using Microsoft.EntityFrameworkCore.Migrations;

namespace asp.net.core.angular.Migrations {
    public partial class SeedDatabase : Migration {

        private const string Make1 = "Make 1";
        private const string Make2 = "Make 2";
        private const string Make3 = "Make 3";

        protected override void Up(MigrationBuilder migrationBuilder) {


            migrationBuilder.Sql($"INSERT INTO Makes (Name) VALUES ('{Make1}'), ('{Make2}'), ('{Make3}')");

            migrationBuilder.Sql($"INSERT INTO Models (Name, MakeId) VALUES ('Make1-ModelA', (SELECT id FROM Makes WHERE Name = '{Make1}'))");
            migrationBuilder.Sql($"INSERT INTO Models (Name, MakeId) VALUES ('Make1-ModelB', (SELECT id FROM Makes WHERE Name = '{Make1}'))");
            migrationBuilder.Sql($"INSERT INTO Models (Name, MakeId) VALUES ('Make1-ModelC', (SELECT id FROM Makes WHERE Name = '{Make1}'))");

            migrationBuilder.Sql($"INSERT INTO Models (Name, MakeId) VALUES ('Make2-ModelA', (SELECT id FROM Makes WHERE Name = '{Make2}'))");
            migrationBuilder.Sql($"INSERT INTO Models (Name, MakeId) VALUES ('Make2-ModelB', (SELECT id FROM Makes WHERE Name = '{Make2}'))");
            migrationBuilder.Sql($"INSERT INTO Models (Name, MakeId) VALUES ('Make2-ModelC', (SELECT id FROM Makes WHERE Name = '{Make2}'))");

            migrationBuilder.Sql($"INSERT INTO Models (Name, MakeId) VALUES ('Make3-ModelA', (SELECT id FROM Makes WHERE Name = '{Make3}'))");
            migrationBuilder.Sql($"INSERT INTO Models (Name, MakeId) VALUES ('Make3-ModelB', (SELECT id FROM Makes WHERE Name = '{Make3}'))");
            migrationBuilder.Sql($"INSERT INTO Models (Name, MakeId) VALUES ('Make3-ModelC', (SELECT id FROM Makes WHERE Name = '{Make3}'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.Sql($"DELETE FROM Makes WHERE Name IN ('{Make1}'), ('{Make2}'), ('{Make3}')");
        }
    }
}
