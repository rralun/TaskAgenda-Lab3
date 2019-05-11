using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskAgenda.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationbuilder.createtable(
            //    name: "tasks",
            //    columns: table => new
            //    {
            //        id = table.column<int>(nullable: false)
            //            .annotation("sqlserver:valuegenerationstrategy", sqlservervaluegenerationstrategy.identitycolumn),
            //        title = table.column<string>(nullable: true),
            //        description = table.column<string>(nullable: true),
            //        datetimeadded = table.column<datetime>(nullable: false),
            //        deadline = table.column<datetime>(nullable: false),
            //        importance = table.column<string>(nullable: true),
            //        status = table.column<string>(nullable: true),
            //        datetimeclosedat = table.column<datetime>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.primarykey("pk_tasks", x => x.id);
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");
        }
    }
}
