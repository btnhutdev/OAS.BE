﻿// <auto-generated />
using System;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(SQLServerDbContext))]
    partial class SQLServerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Auction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Duration")
                        .HasColumnType("float");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime")
                        .HasColumnName("End_Date");

                    b.Property<bool?>("HasBid")
                        .HasColumnType("bit")
                        .HasColumnName("Has_Bid");

                    b.Property<Guid?>("IdAuction")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID_Auction");

                    b.Property<Guid?>("IdProduct")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID_Product");

                    b.Property<Guid?>("IdWinner")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID_Winner");

                    b.Property<bool?>("IsEnd")
                        .HasColumnType("bit")
                        .HasColumnName("Is_End");

                    b.Property<bool?>("IsStart")
                        .HasColumnType("bit")
                        .HasColumnName("Is_Start");

                    b.Property<double>("PriceCurrentMax")
                        .HasColumnType("float")
                        .HasColumnName("Price_Current_Max");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime")
                        .HasColumnName("Start_Date");

                    b.HasKey("Id")
                        .HasName("PK__tbl_Auct__3214EC27F52F6AD8");

                    b.HasIndex("IdProduct");

                    b.HasIndex("IdWinner");

                    b.HasIndex(new[] { "IdAuction" }, "UQ_Auction_ID_Auction")
                        .IsUnique();

                    b.ToTable("tbl_Auction", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoryName")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)")
                        .HasColumnName("Category_Name");

                    b.Property<Guid?>("IdCategory")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID_Category");

                    b.HasKey("Id")
                        .HasName("PK__tbl_Cate__3214EC27A3F93AAE");

                    b.HasIndex(new[] { "IdCategory" }, "UQ_Category_ID_Category")
                        .IsUnique();

                    b.ToTable("tbl_Category", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryName = "Smart Phone",
                            IdCategory = new Guid("33cedb5b-df86-4ed0-9bbe-df0425d35c13")
                        },
                        new
                        {
                            Id = 2,
                            CategoryName = "Laptop",
                            IdCategory = new Guid("0a08457c-1e73-43ca-8670-a45d2a93dfaa")
                        },
                        new
                        {
                            Id = 3,
                            CategoryName = "Furniture",
                            IdCategory = new Guid("02526075-462e-46c1-bae8-ba29df7ad366")
                        },
                        new
                        {
                            Id = 4,
                            CategoryName = "Motorbike",
                            IdCategory = new Guid("5147f3bc-bb8d-4403-ad35-814b4da7a7b9")
                        });
                });

            modelBuilder.Entity("Domain.Entities.DetailAuction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AuctionType")
                        .HasColumnType("int")
                        .HasColumnName("Auction_Type");

                    b.Property<double?>("CurrentPrice")
                        .HasColumnType("float")
                        .HasColumnName("Current_Price");

                    b.Property<Guid?>("IdAuction")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID_Auction");

                    b.Property<Guid?>("IdBidder")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID_Bidder");

                    b.Property<Guid?>("IdDetailAuction")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID_Detail_Auction");

                    b.Property<double?>("MaxBidPrice")
                        .HasColumnType("float")
                        .HasColumnName("Max_Bid_Price");

                    b.HasKey("Id")
                        .HasName("PK__tbl_Deta__3214EC27CAA99C62");

                    b.HasIndex("IdAuction");

                    b.HasIndex("IdBidder");

                    b.HasIndex(new[] { "IdDetailAuction" }, "UQ_Detail_Auction_ID_Detail_Auction")
                        .IsUnique()
                        .HasFilter("[ID_Detail_Auction] IS NOT NULL");

                    b.ToTable("tbl_Detail_Auction", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.HistoryPayment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DatePayment")
                        .HasColumnType("datetime")
                        .HasColumnName("Date_Payment");

                    b.Property<string>("Email")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("First_Name");

                    b.Property<Guid?>("IdHistoryPayment")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID_History_Payment");

                    b.Property<Guid?>("IdProduct")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID_Product");

                    b.Property<Guid?>("IdUser")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID_User");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Last_Name");

                    b.Property<string>("OrderNotes")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("Order_Notes");

                    b.Property<string>("OrderType")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)")
                        .HasColumnName("Order_Type");

                    b.Property<string>("ShipingAddress")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("Shiping_Address");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("Telephone")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<double?>("TotalPrice")
                        .HasColumnType("float")
                        .HasColumnName("Total_Price");

                    b.Property<string>("ZipCode")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("ZIP_Code");

                    b.HasKey("Id")
                        .HasName("PK__tbl_Hist__3214EC2706318AA6");

                    b.HasIndex("IdProduct");

                    b.HasIndex("IdUser");

                    b.HasIndex(new[] { "IdHistoryPayment" }, "UQ_History_Payment_ID_History_Payment")
                        .IsUnique()
                        .HasFilter("[ID_History_Payment] IS NOT NULL");

                    b.ToTable("tbl_History_Payment", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("Data")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Extension")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<Guid?>("IdImage")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID_Image");

                    b.Property<Guid?>("IdProduct")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID_Product");

                    b.Property<string>("ImageName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Image_Name");

                    b.Property<string>("S3Uri")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("S3Uri");

                    b.HasKey("Id")
                        .HasName("PK__tbl_Imag__3214EC27F261D5D6");

                    b.HasIndex("IdProduct");

                    b.HasIndex(new[] { "IdImage" }, "UQ_Image_ID_Image")
                        .IsUnique()
                        .HasFilter("[ID_Image] IS NOT NULL");

                    b.ToTable("tbl_Image", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("IdPermission")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID_Permission");

                    b.Property<string>("PermissionName")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("Permission_Name");

                    b.HasKey("Id")
                        .HasName("PK__tbl_Perm__3214EC2712A75102");

                    b.HasIndex(new[] { "IdPermission" }, "UQ_Permission_ID_Permission")
                        .IsUnique();

                    b.ToTable("tbl_Permission", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IdPermission = new Guid("c05bdda5-10be-4837-8120-cd3e92a1b6b6"),
                            PermissionName = "Auctioneer"
                        },
                        new
                        {
                            Id = 2,
                            IdPermission = new Guid("0b747dec-f3c2-4884-ba2c-ab7a1a2e23bb"),
                            PermissionName = "Bidder"
                        },
                        new
                        {
                            Id = 3,
                            IdPermission = new Guid("854c6bcd-6650-44a2-8ed7-343bfd1632d5"),
                            PermissionName = "Admin"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Category_ID");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime")
                        .HasColumnName("End_Date");

                    b.Property<Guid?>("IdProduct")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID_Product");

                    b.Property<double>("InitPrice")
                        .HasColumnType("float")
                        .HasColumnName("Init_Price");

                    b.Property<bool?>("IsApprove")
                        .HasColumnType("bit")
                        .HasColumnName("Is_Approve");

                    b.Property<bool?>("IsPayment")
                        .HasColumnType("bit")
                        .HasColumnName("Is_Payment");

                    b.Property<bool?>("IsReject")
                        .HasColumnType("bit")
                        .HasColumnName("Is_Reject");

                    b.Property<bool?>("IsSold")
                        .HasColumnType("bit")
                        .HasColumnName("Is_Sold");

                    b.Property<string>("ProductName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Product_Name");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime")
                        .HasColumnName("Start_Date");

                    b.Property<double>("StepPrice")
                        .HasColumnType("float")
                        .HasColumnName("Step_Price");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("User_ID");

                    b.HasKey("Id")
                        .HasName("PK__tbl_Prod__3214EC27DEA28499");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.HasIndex(new[] { "IdProduct" }, "UQ_Product_ID_Product")
                        .IsUnique();

                    b.ToTable("tbl_Product", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("First_Name");

                    b.Property<bool?>("Gender")
                        .HasColumnType("bit");

                    b.Property<Guid?>("IdPermission")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID_Permission");

                    b.Property<Guid>("IdUser")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ID_User");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Last_Name");

                    b.Property<string>("Password")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("Phone_Number");

                    b.Property<string>("Username")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id")
                        .HasName("PK__tbl_User__3214EC27A0DFF69B");

                    b.HasIndex("IdPermission");

                    b.HasIndex(new[] { "IdUser" }, "UQ_User_ID_User")
                        .IsUnique();

                    b.ToTable("tbl_User", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin@gmail.com",
                            FirstName = "Admin",
                            Gender = false,
                            IdUser = new Guid("857b980b-3220-4612-a98e-4377c7fbcd82"),
                            LastName = "Nguyen Van",
                            Password = "admin",
                            PhoneNumber = "0111222333",
                            Username = "admin"
                        },
                        new
                        {
                            Id = 2,
                            Email = "buitruongnhutlm@gmail.com",
                            FirstName = "Huong",
                            Gender = false,
                            IdUser = new Guid("79d364c5-6ed3-4f26-9c45-ee147cce734b"),
                            LastName = "Nguyen Thi",
                            Password = "123456789",
                            PhoneNumber = "0222333444",
                            Username = "bid01"
                        },
                        new
                        {
                            Id = 3,
                            Email = "buitruongnhatlm@gmail.com",
                            FirstName = "Huong",
                            Gender = false,
                            IdUser = new Guid("f2ba714b-ad81-4508-8d54-09d7df55353e"),
                            LastName = "Nguyen Thi",
                            Password = "123456789",
                            PhoneNumber = "0222333444",
                            Username = "bid01"
                        },
                        new
                        {
                            Id = 4,
                            Email = "buitruongnhatlm@gmail.com",
                            FirstName = "An",
                            Gender = true,
                            IdUser = new Guid("c56833cc-f486-4710-927a-414354699d9e"),
                            LastName = "Nguyen Van",
                            Password = "123456789",
                            PhoneNumber = "0123456789",
                            Username = "auc01"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Auction", b =>
                {
                    b.HasOne("Domain.Entities.Product", "IdProductNavigation")
                        .WithMany("Auctions")
                        .HasForeignKey("IdProduct")
                        .HasPrincipalKey("IdProduct")
                        .HasConstraintName("FK_Auction_Product");

                    b.HasOne("Domain.Entities.User", "IdWinnerNavigation")
                        .WithMany("Auctions")
                        .HasForeignKey("IdWinner")
                        .HasPrincipalKey("IdUser")
                        .HasConstraintName("FK_Auction_User");

                    b.Navigation("IdProductNavigation");

                    b.Navigation("IdWinnerNavigation");
                });

            modelBuilder.Entity("Domain.Entities.DetailAuction", b =>
                {
                    b.HasOne("Domain.Entities.Auction", "IdAuctionNavigation")
                        .WithMany("DetailAuctions")
                        .HasForeignKey("IdAuction")
                        .HasPrincipalKey("IdAuction")
                        .HasConstraintName("FK_Detail_Auction_Auction");

                    b.HasOne("Domain.Entities.User", "IdBidderNavigation")
                        .WithMany("DetailAuctions")
                        .HasForeignKey("IdBidder")
                        .HasPrincipalKey("IdUser")
                        .HasConstraintName("FK_Detail_Auction_Bidder");

                    b.Navigation("IdAuctionNavigation");

                    b.Navigation("IdBidderNavigation");
                });

            modelBuilder.Entity("Domain.Entities.HistoryPayment", b =>
                {
                    b.HasOne("Domain.Entities.Product", "IdProductNavigation")
                        .WithMany("HistoryPayments")
                        .HasForeignKey("IdProduct")
                        .HasPrincipalKey("IdProduct")
                        .HasConstraintName("FK_History_Payment_Product");

                    b.HasOne("Domain.Entities.User", "IdUserNavigation")
                        .WithMany("HistoryPayments")
                        .HasForeignKey("IdUser")
                        .HasPrincipalKey("IdUser")
                        .HasConstraintName("FK_History_Payment_User");

                    b.Navigation("IdProductNavigation");

                    b.Navigation("IdUserNavigation");
                });

            modelBuilder.Entity("Domain.Entities.Image", b =>
                {
                    b.HasOne("Domain.Entities.Product", "IdProductNavigation")
                        .WithMany("Images")
                        .HasForeignKey("IdProduct")
                        .HasPrincipalKey("IdProduct")
                        .HasConstraintName("FK_Image_Product");

                    b.Navigation("IdProductNavigation");
                });

            modelBuilder.Entity("Domain.Entities.Product", b =>
                {
                    b.HasOne("Domain.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .HasPrincipalKey("IdCategory")
                        .HasConstraintName("FK_Product_Category");

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("Products")
                        .HasForeignKey("UserId")
                        .HasPrincipalKey("IdUser")
                        .HasConstraintName("FK_Product_User");

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.HasOne("Domain.Entities.Permission", "IdPermissionNavigation")
                        .WithMany("Users")
                        .HasForeignKey("IdPermission")
                        .HasPrincipalKey("IdPermission")
                        .HasConstraintName("FK_User_Permission");

                    b.Navigation("IdPermissionNavigation");
                });

            modelBuilder.Entity("Domain.Entities.Auction", b =>
                {
                    b.Navigation("DetailAuctions");
                });

            modelBuilder.Entity("Domain.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Domain.Entities.Permission", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Domain.Entities.Product", b =>
                {
                    b.Navigation("Auctions");

                    b.Navigation("HistoryPayments");

                    b.Navigation("Images");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("Auctions");

                    b.Navigation("DetailAuctions");

                    b.Navigation("HistoryPayments");

                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
