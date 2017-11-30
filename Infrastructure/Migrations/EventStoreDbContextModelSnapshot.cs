﻿// <auto-generated />
using Infrastructure.EventStore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(EventStoreDbContext))]
    partial class EventStoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("EventStore")
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Infrastructure.EventStore.Event", b =>
                {
                    b.Property<Guid>("AggregateRootId");

                    b.Property<string>("AggregateName");

                    b.Property<int>("Version");

                    b.Property<string>("EventName");

                    b.Property<DateTime>("OccuredOn");

                    b.Property<string>("Payload");

                    b.HasKey("AggregateRootId", "AggregateName", "Version");

                    b.ToTable("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
