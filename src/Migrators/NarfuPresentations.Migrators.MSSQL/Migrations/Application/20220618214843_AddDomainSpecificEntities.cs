using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NarfuPresentations.Migrators.MSSQL.Migrations.Application;

public partial class AddDomainSpecificEntities : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Events",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                AccessType = table.Column<int>(type: "int", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                StartsOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Events", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "ImageSlide",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ImageSlide", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Servey",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Servey", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Participant",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Role = table.Column<int>(type: "int", nullable: false),
                EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Participant", x => x.Id);
                table.ForeignKey(
                    name: "FK_Participant_Events_EventId",
                    column: x => x.EventId,
                    principalTable: "Events",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Presentations",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Presentations", x => x.Id);
                table.ForeignKey(
                    name: "FK_Presentations_Events_EventId",
                    column: x => x.EventId,
                    principalTable: "Events",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Question",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Number = table.Column<int>(type: "int", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ServeyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Question", x => x.Id);
                table.ForeignKey(
                    name: "FK_Question_Servey_ServeyId",
                    column: x => x.ServeyId,
                    principalTable: "Servey",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "ServeySlide",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ServeyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ServeySlide", x => x.Id);
                table.ForeignKey(
                    name: "FK_ServeySlide_Servey_ServeyId",
                    column: x => x.ServeyId,
                    principalTable: "Servey",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Answer",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                IsRight = table.Column<bool>(type: "bit", nullable: false),
                QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Answer", x => x.Id);
                table.ForeignKey(
                    name: "FK_Answer_Question_QuestionId",
                    column: x => x.QuestionId,
                    principalTable: "Question",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Slide",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Number = table.Column<int>(type: "int", nullable: false),
                IsServey = table.Column<bool>(type: "bit", nullable: false),
                ImageSlideId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                ServeySlideId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                PresentationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Slide", x => x.Id);
                table.ForeignKey(
                    name: "FK_Slide_ImageSlide_ImageSlideId",
                    column: x => x.ImageSlideId,
                    principalTable: "ImageSlide",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Slide_Presentations_PresentationId",
                    column: x => x.PresentationId,
                    principalTable: "Presentations",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Slide_ServeySlide_ServeySlideId",
                    column: x => x.ServeySlideId,
                    principalTable: "ServeySlide",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_Answer_QuestionId",
            table: "Answer",
            column: "QuestionId");

        migrationBuilder.CreateIndex(
            name: "IX_Participant_EventId",
            table: "Participant",
            column: "EventId");

        migrationBuilder.CreateIndex(
            name: "IX_Presentations_EventId",
            table: "Presentations",
            column: "EventId");

        migrationBuilder.CreateIndex(
            name: "IX_Question_ServeyId",
            table: "Question",
            column: "ServeyId");

        migrationBuilder.CreateIndex(
            name: "IX_ServeySlide_ServeyId",
            table: "ServeySlide",
            column: "ServeyId");

        migrationBuilder.CreateIndex(
            name: "IX_Slide_ImageSlideId",
            table: "Slide",
            column: "ImageSlideId");

        migrationBuilder.CreateIndex(
            name: "IX_Slide_PresentationId",
            table: "Slide",
            column: "PresentationId");

        migrationBuilder.CreateIndex(
            name: "IX_Slide_ServeySlideId",
            table: "Slide",
            column: "ServeySlideId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Answer");

        migrationBuilder.DropTable(
            name: "Participant");

        migrationBuilder.DropTable(
            name: "Slide");

        migrationBuilder.DropTable(
            name: "Question");

        migrationBuilder.DropTable(
            name: "ImageSlide");

        migrationBuilder.DropTable(
            name: "Presentations");

        migrationBuilder.DropTable(
            name: "ServeySlide");

        migrationBuilder.DropTable(
            name: "Events");

        migrationBuilder.DropTable(
            name: "Servey");
    }
}
