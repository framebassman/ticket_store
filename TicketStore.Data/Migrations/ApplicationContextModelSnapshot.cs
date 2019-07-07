﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TicketStore.Data;

namespace TicketStore.Data.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("TicketStore.Data.Model.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Artist")
                        .HasColumnName("artist");

                    b.Property<int>("MerchantId")
                        .HasColumnName("merchant_id");

                    b.Property<string>("PosterUrl")
                        .HasColumnName("poster_url");

                    b.Property<string>("PressRelease")
                        .HasColumnName("press_release");

                    b.Property<decimal>("Roubles")
                        .HasColumnName("roubles");

                    b.Property<DateTime>("Time")
                        .HasColumnName("time");

                    b.HasKey("Id");

                    b.HasIndex("MerchantId");

                    b.ToTable("events");
                });

            modelBuilder.Entity("TicketStore.Data.Model.Merchant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Place")
                        .HasColumnName("place");

                    b.Property<string>("YandexMoneyAccount")
                        .HasColumnName("yandex_money_account");

                    b.HasKey("Id");

                    b.ToTable("merchants");
                });

            modelBuilder.Entity("TicketStore.Data.Model.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<decimal>("Amount")
                        .HasColumnName("amount");

                    b.Property<string>("Email")
                        .HasColumnName("email");

                    b.HasKey("Id");

                    b.ToTable("payments");
                });

            modelBuilder.Entity("TicketStore.Data.Model.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("created_at");

                    b.Property<int>("EventId")
                        .HasColumnName("event_id");

                    b.Property<string>("EventName")
                        .HasColumnName("event_name");

                    b.Property<bool>("Expired")
                        .HasColumnName("expired");

                    b.Property<string>("Number")
                        .HasColumnName("number");

                    b.Property<int>("PaymentId")
                        .HasColumnName("payment_id");

                    b.Property<decimal>("Roubles")
                        .HasColumnName("roubles");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("PaymentId");

                    b.ToTable("tickets");
                });

            modelBuilder.Entity("TicketStore.Data.Model.Event", b =>
                {
                    b.HasOne("TicketStore.Data.Model.Merchant", "Merchant")
                        .WithMany("Events")
                        .HasForeignKey("MerchantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TicketStore.Data.Model.Ticket", b =>
                {
                    b.HasOne("TicketStore.Data.Model.Event", "Event")
                        .WithMany("Tickets")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TicketStore.Data.Model.Payment", "Payment")
                        .WithMany("Tickets")
                        .HasForeignKey("PaymentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
