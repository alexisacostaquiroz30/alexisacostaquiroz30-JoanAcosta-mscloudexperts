﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PruebaJoanAcosta.Data;

namespace PruebaJoanAcosta.Migrations
{
    [DbContext(typeof(Conexion))]
    [Migration("20231129185312_V1")]
    partial class V1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PruebaJoanAcosta.Models.Deportista", b =>
                {
                    b.Property<int>("IdDeportista")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NombreDeportista")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaisDeportista")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdDeportista");

                    b.ToTable("Deportistas");
                });

            modelBuilder.Entity("PruebaJoanAcosta.Models.DeportistaPeso", b =>
                {
                    b.Property<int>("IdDeporPeso")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Arranque")
                        .HasColumnType("int");

                    b.Property<int>("Envion")
                        .HasColumnType("int");

                    b.Property<int>("IdDeportistaFk")
                        .HasColumnType("int");

                    b.HasKey("IdDeporPeso");

                    b.HasIndex("IdDeportistaFk");

                    b.ToTable("DeportistaPeso");
                });

            modelBuilder.Entity("PruebaJoanAcosta.Models.DeportistaPeso", b =>
                {
                    b.HasOne("PruebaJoanAcosta.Models.Deportista", "Deportista")
                        .WithMany("DeportistaDeportistaPesos")
                        .HasForeignKey("IdDeportistaFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
