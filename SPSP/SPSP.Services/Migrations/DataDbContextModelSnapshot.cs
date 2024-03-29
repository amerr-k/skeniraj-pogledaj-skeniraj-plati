﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SPSP.Services.Database;

namespace SPSP.Services.Migrations
{
    [DbContext(typeof(DataDbContext))]
    partial class DataDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "Bosnian_Latin_100_CI_AI")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SPSP.Services.Database.Business", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ContactInfo")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool?>("Valid")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.HasKey("Id");

                    b.ToTable("Business");
                });

            modelBuilder.Entity("SPSP.Services.Database.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool?>("Valid")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Code" }, "UQ__Category__A25C5AA75505BED9")
                        .IsUnique();

                    b.ToTable("Category");
                });

            modelBuilder.Entity("SPSP.Services.Database.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("PenaltyPoints")
                        .HasColumnType("int");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("UserAccountId")
                        .HasColumnType("int");

                    b.Property<bool?>("Valid")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.HasKey("Id");

                    b.HasIndex("UserAccountId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("SPSP.Services.Database.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BusinessId")
                        .HasColumnType("int");

                    b.Property<int>("UserAccountId")
                        .HasColumnType("int");

                    b.Property<bool?>("Valid")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.HasIndex("UserAccountId");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("SPSP.Services.Database.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BusinessId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool?>("Valid")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("Menu");
                });

            modelBuilder.Entity("SPSP.Services.Database.MenuItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MenuId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool?>("Valid")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex(new[] { "MenuId", "Code" }, "UC_Code")
                        .IsUnique();

                    b.ToTable("MenuItem");
                });

            modelBuilder.Entity("SPSP.Services.Database.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("MenuItemId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("PurchaseInvoiceId")
                        .HasColumnType("int");

                    b.Property<int>("QrtableId")
                        .HasColumnType("int")
                        .HasColumnName("QRTableId");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal?>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalAmountWithVat")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("TotalAmountWithVAT");

                    b.Property<bool?>("Valid")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<decimal?>("Vat")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("VAT");

                    b.Property<decimal?>("Vatamount")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("VATAmount");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("QrtableId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("SPSP.Services.Database.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MenuItemId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal?>("Subtotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool?>("Valid")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.HasKey("Id");

                    b.HasIndex("MenuItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("SPSP.Services.Database.PurchaseInvoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("InvoiceNumber")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Note")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalAmountWithVat")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("TotalAmountWithVAT");

                    b.Property<bool?>("Valid")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<decimal?>("Vat")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("VAT");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("PurchaseInvoice");
                });

            modelBuilder.Entity("SPSP.Services.Database.PurchaseInvoiceItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MenuItemId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("PurchaseInvoiceId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<bool?>("Valid")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.HasKey("Id");

                    b.HasIndex("MenuItemId");

                    b.HasIndex("PurchaseInvoiceId");

                    b.ToTable("PurchaseInvoiceItem");
                });

            modelBuilder.Entity("SPSP.Services.Database.QRTable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BusinessId")
                        .HasColumnType("int");

                    b.Property<int?>("Capacity")
                        .HasColumnType("int");

                    b.Property<bool>("IsTaken")
                        .HasColumnType("bit");

                    b.Property<string>("LocationDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QRCode")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("QRCode");

                    b.Property<int?>("TableNumber")
                        .HasColumnType("int");

                    b.Property<bool?>("Valid")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("QRTable");
                });

            modelBuilder.Entity("SPSP.Services.Database.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContactInfo")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("QRTableId")
                        .HasColumnType("int")
                        .HasColumnName("QRTableId");

                    b.Property<string>("SpecialRequest")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool?>("Valid")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("QRTableId");

                    b.ToTable("Reservation");
                });

            modelBuilder.Entity("SPSP.Services.Database.SaleInvoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("Concluded")
                        .HasColumnType("bit");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("InvoiceNumber")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SaleDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("TotalAmountWithVat")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("TotalAmountWithVAT");

                    b.Property<bool?>("Valid")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<decimal?>("Vat")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("VAT");

                    b.Property<decimal?>("Vatamount")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("VATAmount");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("OrderId");

                    b.ToTable("SaleInvoice");
                });

            modelBuilder.Entity("SPSP.Services.Database.SaleInvoiceItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal?>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("MenuItemId")
                        .HasColumnType("int");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("SaleInvoiceId")
                        .HasColumnType("int");

                    b.Property<bool?>("Valid")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.HasKey("Id");

                    b.HasIndex("MenuItemId");

                    b.HasIndex("SaleInvoiceId");

                    b.ToTable("SaleInvoiceItem");
                });

            modelBuilder.Entity("SPSP.Services.Database.UserAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PasswordSalt")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool?>("Registered")
                        .HasColumnType("bit");

                    b.Property<string>("Username")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool?>("Valid")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Username" }, "UQ__UserAcco__536C85E4249C94D1")
                        .IsUnique()
                        .HasFilter("[Username] IS NOT NULL");

                    b.HasIndex(new[] { "Email" }, "UQ__UserAcco__A9D10534C2A79D0E")
                        .IsUnique();

                    b.ToTable("UserAccount");
                });

            modelBuilder.Entity("SPSP.Services.Database.UserAccountUserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("UserAccountId")
                        .HasColumnType("int");

                    b.Property<int>("UserRoleId")
                        .HasColumnType("int");

                    b.Property<bool?>("Valid")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.HasKey("Id");

                    b.HasIndex("UserAccountId");

                    b.HasIndex("UserRoleId");

                    b.ToTable("UserAccountUserRole");
                });

            modelBuilder.Entity("SPSP.Services.Database.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool?>("Valid")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Name" }, "UQ__UserRole__737584F659087FEA")
                        .IsUnique();

                    b.HasIndex(new[] { "Code" }, "UQ__UserRole__A25C5AA780869A0D")
                        .IsUnique();

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("SPSP.Services.Database.Customer", b =>
                {
                    b.HasOne("SPSP.Services.Database.UserAccount", "UserAccount")
                        .WithMany("Customers")
                        .HasForeignKey("UserAccountId")
                        .HasConstraintName("FK_Customer_UserAccountId")
                        .IsRequired();

                    b.Navigation("UserAccount");
                });

            modelBuilder.Entity("SPSP.Services.Database.Employee", b =>
                {
                    b.HasOne("SPSP.Services.Database.Business", "Business")
                        .WithMany("Employees")
                        .HasForeignKey("BusinessId")
                        .HasConstraintName("FK_Employee_BusinessId")
                        .IsRequired();

                    b.HasOne("SPSP.Services.Database.UserAccount", "UserAccount")
                        .WithMany("Employees")
                        .HasForeignKey("UserAccountId")
                        .HasConstraintName("FK_Employee_UserAccountId")
                        .IsRequired();

                    b.Navigation("Business");

                    b.Navigation("UserAccount");
                });

            modelBuilder.Entity("SPSP.Services.Database.Menu", b =>
                {
                    b.HasOne("SPSP.Services.Database.Business", "Business")
                        .WithMany("Menus")
                        .HasForeignKey("BusinessId")
                        .HasConstraintName("FK_Menu_BusinessId")
                        .IsRequired();

                    b.Navigation("Business");
                });

            modelBuilder.Entity("SPSP.Services.Database.MenuItem", b =>
                {
                    b.HasOne("SPSP.Services.Database.Category", "Category")
                        .WithMany("MenuItems")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK_MenuItem_CategoryId");

                    b.HasOne("SPSP.Services.Database.Menu", "Menu")
                        .WithMany("MenuItems")
                        .HasForeignKey("MenuId")
                        .HasConstraintName("FK_MenuItem_MenuId")
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("SPSP.Services.Database.Order", b =>
                {
                    b.HasOne("SPSP.Services.Database.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK_Order_CustomerId");

                    b.HasOne("SPSP.Services.Database.QRTable", "QRTable")
                        .WithMany("Orders")
                        .HasForeignKey("QrtableId")
                        .HasConstraintName("FK_Order_QRTableId")
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("QRTable");
                });

            modelBuilder.Entity("SPSP.Services.Database.OrderItem", b =>
                {
                    b.HasOne("SPSP.Services.Database.MenuItem", "MenuItem")
                        .WithMany("OrderItems")
                        .HasForeignKey("MenuItemId")
                        .HasConstraintName("FK_OrderItem_MenuItemId")
                        .IsRequired();

                    b.HasOne("SPSP.Services.Database.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK_OrderItem_OrderId")
                        .IsRequired();

                    b.Navigation("MenuItem");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("SPSP.Services.Database.PurchaseInvoice", b =>
                {
                    b.HasOne("SPSP.Services.Database.Employee", "Employee")
                        .WithMany("PurchaseInvoices")
                        .HasForeignKey("EmployeeId")
                        .HasConstraintName("FK_PurchaseInvoice_EmployeeId")
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("SPSP.Services.Database.PurchaseInvoiceItem", b =>
                {
                    b.HasOne("SPSP.Services.Database.MenuItem", "MenuItem")
                        .WithMany("PurchaseInvoiceItems")
                        .HasForeignKey("MenuItemId")
                        .HasConstraintName("FK_PurchaseInvoiceItem_MenuItemId")
                        .IsRequired();

                    b.HasOne("SPSP.Services.Database.PurchaseInvoice", "PurchaseInvoice")
                        .WithMany("PurchaseInvoiceItems")
                        .HasForeignKey("PurchaseInvoiceId")
                        .HasConstraintName("FK_PurchaseInvoiceItem_PurchaseInvoiceId")
                        .IsRequired();

                    b.Navigation("MenuItem");

                    b.Navigation("PurchaseInvoice");
                });

            modelBuilder.Entity("SPSP.Services.Database.QRTable", b =>
                {
                    b.HasOne("SPSP.Services.Database.Business", "Business")
                        .WithMany("QRTables")
                        .HasForeignKey("BusinessId")
                        .HasConstraintName("FK_QRTable_BusinessId")
                        .IsRequired();

                    b.Navigation("Business");
                });

            modelBuilder.Entity("SPSP.Services.Database.Reservation", b =>
                {
                    b.HasOne("SPSP.Services.Database.Customer", "Customer")
                        .WithMany("Reservations")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK_Reservation_CustomerId")
                        .IsRequired();

                    b.HasOne("SPSP.Services.Database.QRTable", "QRTable")
                        .WithMany("Reservations")
                        .HasForeignKey("QRTableId")
                        .HasConstraintName("FK_Reservation_QRTableId")
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("QRTable");
                });

            modelBuilder.Entity("SPSP.Services.Database.SaleInvoice", b =>
                {
                    b.HasOne("SPSP.Services.Database.Employee", "Employee")
                        .WithMany("SaleInvoices")
                        .HasForeignKey("EmployeeId")
                        .HasConstraintName("FK_SaleInvoice_EmployeeId")
                        .IsRequired();

                    b.HasOne("SPSP.Services.Database.Order", "Order")
                        .WithMany("SaleInvoices")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK_SaleInvoice_OrderId")
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("SPSP.Services.Database.SaleInvoiceItem", b =>
                {
                    b.HasOne("SPSP.Services.Database.MenuItem", "MenuItem")
                        .WithMany("SaleInvoiceItems")
                        .HasForeignKey("MenuItemId")
                        .HasConstraintName("FK_SaleInvoiceItem_MenuItemId")
                        .IsRequired();

                    b.HasOne("SPSP.Services.Database.SaleInvoice", "SaleInvoice")
                        .WithMany("SaleInvoiceItems")
                        .HasForeignKey("SaleInvoiceId")
                        .HasConstraintName("FK_SaleInvoiceItem_SaleInvoiceId")
                        .IsRequired();

                    b.Navigation("MenuItem");

                    b.Navigation("SaleInvoice");
                });

            modelBuilder.Entity("SPSP.Services.Database.UserAccountUserRole", b =>
                {
                    b.HasOne("SPSP.Services.Database.UserAccount", "UserAccount")
                        .WithMany("UserAccountUserRoles")
                        .HasForeignKey("UserAccountId")
                        .HasConstraintName("FK_UserAccountUserRole_UserAccountId")
                        .IsRequired();

                    b.HasOne("SPSP.Services.Database.UserRole", "UserRole")
                        .WithMany("UserAccountUserRoles")
                        .HasForeignKey("UserRoleId")
                        .HasConstraintName("FK_UserAccountUserRole_UserRoleId")
                        .IsRequired();

                    b.Navigation("UserAccount");

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("SPSP.Services.Database.Business", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("Menus");

                    b.Navigation("QRTables");
                });

            modelBuilder.Entity("SPSP.Services.Database.Category", b =>
                {
                    b.Navigation("MenuItems");
                });

            modelBuilder.Entity("SPSP.Services.Database.Customer", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("SPSP.Services.Database.Employee", b =>
                {
                    b.Navigation("PurchaseInvoices");

                    b.Navigation("SaleInvoices");
                });

            modelBuilder.Entity("SPSP.Services.Database.Menu", b =>
                {
                    b.Navigation("MenuItems");
                });

            modelBuilder.Entity("SPSP.Services.Database.MenuItem", b =>
                {
                    b.Navigation("OrderItems");

                    b.Navigation("PurchaseInvoiceItems");

                    b.Navigation("SaleInvoiceItems");
                });

            modelBuilder.Entity("SPSP.Services.Database.Order", b =>
                {
                    b.Navigation("OrderItems");

                    b.Navigation("SaleInvoices");
                });

            modelBuilder.Entity("SPSP.Services.Database.PurchaseInvoice", b =>
                {
                    b.Navigation("PurchaseInvoiceItems");
                });

            modelBuilder.Entity("SPSP.Services.Database.QRTable", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("SPSP.Services.Database.SaleInvoice", b =>
                {
                    b.Navigation("SaleInvoiceItems");
                });

            modelBuilder.Entity("SPSP.Services.Database.UserAccount", b =>
                {
                    b.Navigation("Customers");

                    b.Navigation("Employees");

                    b.Navigation("UserAccountUserRoles");
                });

            modelBuilder.Entity("SPSP.Services.Database.UserRole", b =>
                {
                    b.Navigation("UserAccountUserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
