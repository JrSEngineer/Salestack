﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Salestack.Data.Context;

#nullable disable

namespace Salestack.Data.Migrations
{
    [DbContext(typeof(SalestackDbContext))]
    partial class SalestackDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Salestack.Entities.Company.SalestackCompany", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CompanyCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("DirectorId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Cnpj")
                        .IsUnique();

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.ToTable("Company");
                });

            modelBuilder.Entity("Salestack.Entities.Customers.CustomerAddress", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<double>("Lat")
                        .HasColumnType("double precision");

                    b.Property<double>("Long")
                        .HasColumnType("double precision");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("Salestack.Entities.Customers.SalestackCustomer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("SalestackCompanyId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("SalestackCompanyId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Salestack.Entities.Joins.SalestackBudgetProduct", b =>
                {
                    b.Property<Guid>("BudgetId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<Guid?>("SalestackBudgetId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("numeric");

                    b.HasKey("BudgetId", "ProductId");

                    b.HasIndex("ProductId");

                    b.HasIndex("SalestackBudgetId");

                    b.ToTable("BudgetProduct");
                });

            modelBuilder.Entity("Salestack.Entities.Joins.SalestackBudgetService", b =>
                {
                    b.Property<Guid>("BudgetId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uuid");

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<Guid?>("SalestackBudgetId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("numeric");

                    b.HasKey("BudgetId", "ServiceId");

                    b.HasIndex("SalestackBudgetId");

                    b.HasIndex("ServiceId");

                    b.ToTable("BudgetService");
                });

            modelBuilder.Entity("Salestack.Entities.SaleTargets.SalestackProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("ProductImage")
                        .HasColumnType("text");

                    b.Property<Guid?>("SalestackCompanyId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("SalestackOrderId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SalestackCompanyId");

                    b.HasIndex("SalestackOrderId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Salestack.Entities.SaleTargets.SalestackService", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<Guid?>("SalestackCompanyId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("SalestackOrderId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SalestackCompanyId");

                    b.HasIndex("SalestackOrderId");

                    b.ToTable("Service");
                });

            modelBuilder.Entity("Salestack.Entities.Sales.SalestackBudget", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("SalestackCustomerId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<decimal>("TotalBudgetPrice")
                        .HasColumnType("numeric");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SalestackCustomerId");

                    b.ToTable("Budget");
                });

            modelBuilder.Entity("Salestack.Entities.Sales.SalestackOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BudgetId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<decimal>("TotalOrderPrice")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Salestack.Entities.Teams.SalestackTeam", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("DirectorId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ManagerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("DirectorId");

                    b.HasIndex("ManagerId");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("Salestack.Entities.Users.Authentication", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<int>("Occupation")
                        .HasColumnType("integer");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Email");

                    b.ToTable("Authentication");
                });

            modelBuilder.Entity("Salestack.Entities.Users.SalestackDirector", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AuthenticationEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<int>("Hierarchy")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Occupation")
                        .HasColumnType("integer");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AuthenticationEmail");

                    b.HasIndex("CompanyId")
                        .IsUnique();

                    b.ToTable("Director");
                });

            modelBuilder.Entity("Salestack.Entities.Users.SalestackManager", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AuthenticationEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<int>("Hierarchy")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Occupation")
                        .HasColumnType("integer");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("VerificationCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AuthenticationEmail");

                    b.HasIndex("CompanyId");

                    b.ToTable("Manager");
                });

            modelBuilder.Entity("Salestack.Entities.Users.SalestackSeller", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AuthenticationEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<int>("Hierarchy")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Occupation")
                        .HasColumnType("integer");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TeamId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AuthenticationEmail");

                    b.HasIndex("CompanyId");

                    b.HasIndex("TeamId");

                    b.ToTable("Seller");
                });

            modelBuilder.Entity("Salestack.Entities.Customers.SalestackCustomer", b =>
                {
                    b.HasOne("Salestack.Entities.Customers.CustomerAddress", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Salestack.Entities.Company.SalestackCompany", null)
                        .WithMany("Customers")
                        .HasForeignKey("SalestackCompanyId");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Salestack.Entities.Joins.SalestackBudgetProduct", b =>
                {
                    b.HasOne("Salestack.Entities.SaleTargets.SalestackProduct", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Salestack.Entities.Sales.SalestackBudget", null)
                        .WithMany("Products")
                        .HasForeignKey("SalestackBudgetId");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Salestack.Entities.Joins.SalestackBudgetService", b =>
                {
                    b.HasOne("Salestack.Entities.Sales.SalestackBudget", null)
                        .WithMany("Services")
                        .HasForeignKey("SalestackBudgetId");

                    b.HasOne("Salestack.Entities.SaleTargets.SalestackService", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Service");
                });

            modelBuilder.Entity("Salestack.Entities.SaleTargets.SalestackProduct", b =>
                {
                    b.HasOne("Salestack.Entities.Company.SalestackCompany", null)
                        .WithMany("Products")
                        .HasForeignKey("SalestackCompanyId");

                    b.HasOne("Salestack.Entities.Sales.SalestackOrder", null)
                        .WithMany("Products")
                        .HasForeignKey("SalestackOrderId");
                });

            modelBuilder.Entity("Salestack.Entities.SaleTargets.SalestackService", b =>
                {
                    b.HasOne("Salestack.Entities.Company.SalestackCompany", null)
                        .WithMany("Services")
                        .HasForeignKey("SalestackCompanyId");

                    b.HasOne("Salestack.Entities.Sales.SalestackOrder", null)
                        .WithMany("Services")
                        .HasForeignKey("SalestackOrderId");
                });

            modelBuilder.Entity("Salestack.Entities.Sales.SalestackBudget", b =>
                {
                    b.HasOne("Salestack.Entities.Customers.SalestackCustomer", null)
                        .WithMany("Budgets")
                        .HasForeignKey("SalestackCustomerId");
                });

            modelBuilder.Entity("Salestack.Entities.Teams.SalestackTeam", b =>
                {
                    b.HasOne("Salestack.Entities.Company.SalestackCompany", "Company")
                        .WithMany("Teams")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Salestack.Entities.Users.SalestackDirector", "Director")
                        .WithMany("Teams")
                        .HasForeignKey("DirectorId");

                    b.HasOne("Salestack.Entities.Users.SalestackManager", "Manager")
                        .WithMany("Teams")
                        .HasForeignKey("ManagerId");

                    b.Navigation("Company");

                    b.Navigation("Director");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("Salestack.Entities.Users.SalestackDirector", b =>
                {
                    b.HasOne("Salestack.Entities.Users.Authentication", "Authentication")
                        .WithMany()
                        .HasForeignKey("AuthenticationEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Salestack.Entities.Company.SalestackCompany", "Company")
                        .WithOne("Director")
                        .HasForeignKey("Salestack.Entities.Users.SalestackDirector", "CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Authentication");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Salestack.Entities.Users.SalestackManager", b =>
                {
                    b.HasOne("Salestack.Entities.Users.Authentication", "Authentication")
                        .WithMany()
                        .HasForeignKey("AuthenticationEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Salestack.Entities.Company.SalestackCompany", "Company")
                        .WithMany("Managers")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Authentication");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Salestack.Entities.Users.SalestackSeller", b =>
                {
                    b.HasOne("Salestack.Entities.Users.Authentication", "Authentication")
                        .WithMany()
                        .HasForeignKey("AuthenticationEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Salestack.Entities.Company.SalestackCompany", "Company")
                        .WithMany("Sellers")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Salestack.Entities.Teams.SalestackTeam", "Team")
                        .WithMany("Sellers")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Authentication");

                    b.Navigation("Company");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Salestack.Entities.Company.SalestackCompany", b =>
                {
                    b.Navigation("Customers");

                    b.Navigation("Director")
                        .IsRequired();

                    b.Navigation("Managers");

                    b.Navigation("Products");

                    b.Navigation("Sellers");

                    b.Navigation("Services");

                    b.Navigation("Teams");
                });

            modelBuilder.Entity("Salestack.Entities.Customers.SalestackCustomer", b =>
                {
                    b.Navigation("Budgets");
                });

            modelBuilder.Entity("Salestack.Entities.Sales.SalestackBudget", b =>
                {
                    b.Navigation("Products");

                    b.Navigation("Services");
                });

            modelBuilder.Entity("Salestack.Entities.Sales.SalestackOrder", b =>
                {
                    b.Navigation("Products");

                    b.Navigation("Services");
                });

            modelBuilder.Entity("Salestack.Entities.Teams.SalestackTeam", b =>
                {
                    b.Navigation("Sellers");
                });

            modelBuilder.Entity("Salestack.Entities.Users.SalestackDirector", b =>
                {
                    b.Navigation("Teams");
                });

            modelBuilder.Entity("Salestack.Entities.Users.SalestackManager", b =>
                {
                    b.Navigation("Teams");
                });
#pragma warning restore 612, 618
        }
    }
}
