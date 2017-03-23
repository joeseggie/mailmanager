using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MailManager.Data;

namespace MailManager.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MailManager.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("MailManager.Models.IncomingFile", b =>
                {
                    b.Property<Guid>("IncomingFileId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FileNumber");

                    b.Property<DateTime>("IncomingDate")
                        .HasAnnotation("SqlServer:ColumnType", "date");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("IncomingFileId");

                    b.HasIndex("FileNumber");

                    b.ToTable("IncomingFile");
                });

            modelBuilder.Entity("MailManager.Models.IncomingMail", b =>
                {
                    b.Property<Guid>("IncomingMailId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasAnnotation("SqlServer:ColumnType", "varchar(150)");

                    b.Property<DateTime>("IncomingDate")
                        .HasAnnotation("SqlServer:ColumnType", "date");

                    b.Property<string>("ReferenceNumber");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("IncomingMailId");

                    b.HasIndex("ReferenceNumber");

                    b.ToTable("IncomingMail");
                });

            modelBuilder.Entity("MailManager.Models.OfficeMail", b =>
                {
                    b.Property<string>("ReferenceNumber")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasAnnotation("SqlServer:ColumnType", "varchar(20)");

                    b.Property<string>("From")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasAnnotation("SqlServer:ColumnType", "varchar(50)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasAnnotation("SqlServer:ColumnType", "varchar(100)");

                    b.Property<string>("To")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasAnnotation("SqlServer:ColumnType", "varchar(50)");

                    b.HasKey("ReferenceNumber");

                    b.ToTable("OfficeMail");
                });

            modelBuilder.Entity("MailManager.Models.OutgoingFile", b =>
                {
                    b.Property<Guid>("OutgoingFileId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment");

                    b.Property<string>("FileNumber");

                    b.Property<string>("Officer");

                    b.Property<DateTime>("OutgoingDate");

                    b.Property<byte[]>("RowVersion");

                    b.HasKey("OutgoingFileId");

                    b.HasIndex("FileNumber");

                    b.ToTable("OutgoingFiles");
                });

            modelBuilder.Entity("MailManager.Models.OutgoingMail", b =>
                {
                    b.Property<Guid>("OutgoingMailId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment");

                    b.Property<string>("Officer");

                    b.Property<DateTime>("OutgoingDate");

                    b.Property<string>("ReferenceNumber");

                    b.Property<byte[]>("RowVersion");

                    b.HasKey("OutgoingMailId");

                    b.HasIndex("ReferenceNumber");

                    b.ToTable("OutgoingMails");
                });

            modelBuilder.Entity("MailManager.Models.RecordFile", b =>
                {
                    b.Property<string>("FileNumber")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasAnnotation("SqlServer:ColumnType", "varchar(20)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasAnnotation("SqlServer:ColumnType", "varchar(150)");

                    b.HasKey("FileNumber");

                    b.ToTable("RecordFile");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("MailManager.Models.IncomingFile", b =>
                {
                    b.HasOne("MailManager.Models.RecordFile", "RecordFile")
                        .WithMany("IncomingFiles")
                        .HasForeignKey("FileNumber");
                });

            modelBuilder.Entity("MailManager.Models.IncomingMail", b =>
                {
                    b.HasOne("MailManager.Models.OfficeMail", "OfficeMail")
                        .WithMany("IncomingMails")
                        .HasForeignKey("ReferenceNumber");
                });

            modelBuilder.Entity("MailManager.Models.OutgoingFile", b =>
                {
                    b.HasOne("MailManager.Models.RecordFile", "RecordFile")
                        .WithMany("OutgoingFiles")
                        .HasForeignKey("FileNumber");
                });

            modelBuilder.Entity("MailManager.Models.OutgoingMail", b =>
                {
                    b.HasOne("MailManager.Models.OfficeMail", "OfficeMail")
                        .WithMany("OutgoingMails")
                        .HasForeignKey("ReferenceNumber");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("MailManager.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MailManager.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MailManager.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
