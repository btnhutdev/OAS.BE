using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Category",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Category = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Category_Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tbl_Cate__3214EC27A3F93AAE", x => x.ID);
                    table.UniqueConstraint("AK_tbl_Category_ID_Category", x => x.ID_Category);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Permission",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Permission = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Permission_Name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tbl_Perm__3214EC2712A75102", x => x.ID);
                    table.UniqueConstraint("AK_tbl_Permission_ID_Permission", x => x.ID_Permission);
                });

            migrationBuilder.CreateTable(
                name: "tbl_User",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_User = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Phone_Number = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    First_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Last_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Gender = table.Column<bool>(type: "bit", nullable: true),
                    ID_Permission = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tbl_User__3214EC27A0DFF69B", x => x.ID);
                    table.UniqueConstraint("AK_tbl_User_ID_User", x => x.ID_User);
                    table.ForeignKey(
                        name: "FK_User_Permission",
                        column: x => x.ID_Permission,
                        principalTable: "tbl_Permission",
                        principalColumn: "ID_Permission");
                });

            migrationBuilder.CreateTable(
                name: "tbl_Product",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Product = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Product_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Init_Price = table.Column<double>(type: "float", nullable: false),
                    Step_Price = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Start_Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    End_Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Is_Approve = table.Column<bool>(type: "bit", nullable: true),
                    Is_Sold = table.Column<bool>(type: "bit", nullable: true),
                    Is_Payment = table.Column<bool>(type: "bit", nullable: true),
                    Category_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    User_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Is_Reject = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tbl_Prod__3214EC27DEA28499", x => x.ID);
                    table.UniqueConstraint("AK_tbl_Product_ID_Product", x => x.ID_Product);
                    table.ForeignKey(
                        name: "FK_Product_Category",
                        column: x => x.Category_ID,
                        principalTable: "tbl_Category",
                        principalColumn: "ID_Category");
                    table.ForeignKey(
                        name: "FK_Product_User",
                        column: x => x.User_ID,
                        principalTable: "tbl_User",
                        principalColumn: "ID_User");
                });

            migrationBuilder.CreateTable(
                name: "tbl_Auction",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Auction = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_Product = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Start_Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    End_Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Price_Current_Max = table.Column<double>(type: "float", nullable: false),
                    Duration = table.Column<double>(type: "float", nullable: false),
                    Is_Start = table.Column<bool>(type: "bit", nullable: true),
                    Is_End = table.Column<bool>(type: "bit", nullable: true),
                    Has_Bid = table.Column<bool>(type: "bit", nullable: true),
                    ID_Winner = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tbl_Auct__3214EC27F52F6AD8", x => x.ID);
                    table.UniqueConstraint("AK_tbl_Auction_ID_Auction", x => x.ID_Auction);
                    table.ForeignKey(
                        name: "FK_Auction_Product",
                        column: x => x.ID_Product,
                        principalTable: "tbl_Product",
                        principalColumn: "ID_Product");
                    table.ForeignKey(
                        name: "FK_Auction_User",
                        column: x => x.ID_Winner,
                        principalTable: "tbl_User",
                        principalColumn: "ID_User");
                });

            migrationBuilder.CreateTable(
                name: "tbl_History_Payment",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_History_Payment = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ID_User = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ID_Product = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Order_Type = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    First_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Last_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ZIP_Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Telephone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Shiping_Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Order_Notes = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Total_Price = table.Column<double>(type: "float", nullable: true),
                    Date_Payment = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tbl_Hist__3214EC2706318AA6", x => x.ID);
                    table.ForeignKey(
                        name: "FK_History_Payment_Product",
                        column: x => x.ID_Product,
                        principalTable: "tbl_Product",
                        principalColumn: "ID_Product");
                    table.ForeignKey(
                        name: "FK_History_Payment_User",
                        column: x => x.ID_User,
                        principalTable: "tbl_User",
                        principalColumn: "ID_User");
                });

            migrationBuilder.CreateTable(
                name: "tbl_Image",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Image = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Image_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    ID_Product = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    S3Uri = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tbl_Imag__3214EC27F261D5D6", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Image_Product",
                        column: x => x.ID_Product,
                        principalTable: "tbl_Product",
                        principalColumn: "ID_Product");
                });

            migrationBuilder.CreateTable(
                name: "tbl_Detail_Auction",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Detail_Auction = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ID_Auction = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ID_Bidder = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Current_Price = table.Column<double>(type: "float", nullable: true),
                    Max_Bid_Price = table.Column<double>(type: "float", nullable: true),
                    Auction_Type = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tbl_Deta__3214EC27CAA99C62", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Detail_Auction_Auction",
                        column: x => x.ID_Auction,
                        principalTable: "tbl_Auction",
                        principalColumn: "ID_Auction");
                    table.ForeignKey(
                        name: "FK_Detail_Auction_Bidder",
                        column: x => x.ID_Bidder,
                        principalTable: "tbl_User",
                        principalColumn: "ID_User");
                });

            migrationBuilder.InsertData(
                table: "tbl_Category",
                columns: new[] { "ID", "Category_Name", "ID_Category" },
                values: new object[,]
                {
                    { 1, "Smart Phone", new Guid("33cedb5b-df86-4ed0-9bbe-df0425d35c13") },
                    { 2, "Laptop", new Guid("0a08457c-1e73-43ca-8670-a45d2a93dfaa") },
                    { 3, "Furniture", new Guid("02526075-462e-46c1-bae8-ba29df7ad366") },
                    { 4, "Motorbike", new Guid("5147f3bc-bb8d-4403-ad35-814b4da7a7b9") }
                });

            migrationBuilder.InsertData(
                table: "tbl_Permission",
                columns: new[] { "ID", "ID_Permission", "Permission_Name" },
                values: new object[,]
                {
                    { 1, new Guid("c05bdda5-10be-4837-8120-cd3e92a1b6b6"), "Auctioneer" },
                    { 2, new Guid("0b747dec-f3c2-4884-ba2c-ab7a1a2e23bb"), "Bidder" },
                    { 3, new Guid("854c6bcd-6650-44a2-8ed7-343bfd1632d5"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "tbl_User",
                columns: new[] { "ID", "Email", "First_Name", "Gender", "ID_Permission", "ID_User", "Last_Name", "Password", "Phone_Number", "Username" },
                values: new object[,]
                {
                    { 1, "admin@gmail.com", "Admin", false, null, new Guid("857b980b-3220-4612-a98e-4377c7fbcd82"), "Nguyen Van", "admin", "0111222333", "admin" },
                    { 2, "buitruongnhutlm@gmail.com", "Huong", false, null, new Guid("79d364c5-6ed3-4f26-9c45-ee147cce734b"), "Nguyen Thi", "123456789", "0222333444", "bid01" },
                    { 3, "buitruongnhatlm@gmail.com", "Huong", false, null, new Guid("f2ba714b-ad81-4508-8d54-09d7df55353e"), "Nguyen Thi", "123456789", "0222333444", "bid01" },
                    { 4, "buitruongnhatlm@gmail.com", "An", true, null, new Guid("c56833cc-f486-4710-927a-414354699d9e"), "Nguyen Van", "123456789", "0123456789", "auc01" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Auction_ID_Product",
                table: "tbl_Auction",
                column: "ID_Product");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Auction_ID_Winner",
                table: "tbl_Auction",
                column: "ID_Winner");

            migrationBuilder.CreateIndex(
                name: "UQ_Auction_ID_Auction",
                table: "tbl_Auction",
                column: "ID_Auction",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Category_ID_Category",
                table: "tbl_Category",
                column: "ID_Category",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Detail_Auction_ID_Auction",
                table: "tbl_Detail_Auction",
                column: "ID_Auction");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Detail_Auction_ID_Bidder",
                table: "tbl_Detail_Auction",
                column: "ID_Bidder");

            migrationBuilder.CreateIndex(
                name: "UQ_Detail_Auction_ID_Detail_Auction",
                table: "tbl_Detail_Auction",
                column: "ID_Detail_Auction",
                unique: true,
                filter: "[ID_Detail_Auction] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_History_Payment_ID_Product",
                table: "tbl_History_Payment",
                column: "ID_Product");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_History_Payment_ID_User",
                table: "tbl_History_Payment",
                column: "ID_User");

            migrationBuilder.CreateIndex(
                name: "UQ_History_Payment_ID_History_Payment",
                table: "tbl_History_Payment",
                column: "ID_History_Payment",
                unique: true,
                filter: "[ID_History_Payment] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Image_ID_Product",
                table: "tbl_Image",
                column: "ID_Product");

            migrationBuilder.CreateIndex(
                name: "UQ_Image_ID_Image",
                table: "tbl_Image",
                column: "ID_Image",
                unique: true,
                filter: "[ID_Image] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ_Permission_ID_Permission",
                table: "tbl_Permission",
                column: "ID_Permission",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Product_Category_ID",
                table: "tbl_Product",
                column: "Category_ID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Product_User_ID",
                table: "tbl_Product",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "UQ_Product_ID_Product",
                table: "tbl_Product",
                column: "ID_Product",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_User_ID_Permission",
                table: "tbl_User",
                column: "ID_Permission");

            migrationBuilder.CreateIndex(
                name: "UQ_User_ID_User",
                table: "tbl_User",
                column: "ID_User",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Detail_Auction");

            migrationBuilder.DropTable(
                name: "tbl_History_Payment");

            migrationBuilder.DropTable(
                name: "tbl_Image");

            migrationBuilder.DropTable(
                name: "tbl_Auction");

            migrationBuilder.DropTable(
                name: "tbl_Product");

            migrationBuilder.DropTable(
                name: "tbl_Category");

            migrationBuilder.DropTable(
                name: "tbl_User");

            migrationBuilder.DropTable(
                name: "tbl_Permission");
        }
    }
}
