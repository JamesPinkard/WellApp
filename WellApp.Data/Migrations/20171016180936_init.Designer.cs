using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WellApp.Data;

namespace WellApp.Data.Migrations
{
    [DbContext(typeof(GroundwaterContext))]
    [Migration("20171016180936_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("WellApp.Domain.Aquifer", b =>
                {
                    b.Property<int>("AquiferID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AquiferCode");

                    b.Property<string>("AquiferCodeDescriprion");

                    b.Property<string>("AquiferName");

                    b.HasKey("AquiferID");

                    b.ToTable("Aquifers");
                });

            modelBuilder.Entity("WellApp.Domain.Well", b =>
                {
                    b.Property<int>("WellId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AquiferId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("DrillingEndDate");

                    b.Property<int>("GroundSurfaceElevation");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("StateWellNumber");

                    b.Property<int>("WellDepth");

                    b.Property<int>("WellReportTrackingNumber");

                    b.HasKey("WellId");

                    b.HasIndex("AquiferId");

                    b.ToTable("Wells");
                });

            modelBuilder.Entity("WellApp.Domain.Well", b =>
                {
                    b.HasOne("WellApp.Domain.Aquifer", "Aquifer")
                        .WithMany()
                        .HasForeignKey("AquiferId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
