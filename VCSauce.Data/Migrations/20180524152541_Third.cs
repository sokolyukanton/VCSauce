using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VCSauce.Data.Migrations
{
    public partial class Third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Versions_VersionId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Versions_Repositories_RepositoryId",
                table: "Versions");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Versions_VersionId",
                table: "Files",
                column: "VersionId",
                principalTable: "Versions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Versions_Repositories_RepositoryId",
                table: "Versions",
                column: "RepositoryId",
                principalTable: "Repositories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Versions_VersionId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Versions_Repositories_RepositoryId",
                table: "Versions");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Versions_VersionId",
                table: "Files",
                column: "VersionId",
                principalTable: "Versions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Versions_Repositories_RepositoryId",
                table: "Versions",
                column: "RepositoryId",
                principalTable: "Repositories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
