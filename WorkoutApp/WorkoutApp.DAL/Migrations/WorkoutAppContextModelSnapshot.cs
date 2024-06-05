﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WorkoutApp.DAL.Context;

#nullable disable

namespace WorkoutApp.DAL.Migrations
{
    [DbContext(typeof(WorkoutAppContext))]
    partial class WorkoutAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.6");

            modelBuilder.Entity("WorkoutApp.DAL.Entities.AppSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<int?>("DistanceUnit")
                        .HasColumnType("INTEGER")
                        .HasColumnName("distance_unit");

                    b.Property<string>("FirstDayOfWeek")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("Sunday")
                        .HasColumnName("first_day_of_week");

                    b.Property<string>("HeightUnit")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("Inch")
                        .HasColumnName("height_unit");

                    b.Property<bool?>("KeepScreenOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false)
                        .HasColumnName("keep_screen_on");

                    b.Property<bool?>("LockScreenOrientation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false)
                        .HasColumnName("lock_screen_orientation");

                    b.Property<string>("OneRepMaxStrategy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("Brzycki")
                        .HasColumnName("one_rep_max_strategy");

                    b.Property<bool?>("ShowAssistanceExercises")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(true)
                        .HasColumnName("show_assistance_exercises");

                    b.Property<bool?>("ShowRepsToBeat1Rm")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(true)
                        .HasColumnName("show_reps_to_beat_1rm");

                    b.Property<bool?>("ShowRepsToBeatPr")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(true)
                        .HasColumnName("show_reps_to_beat_pr");

                    b.Property<string>("SupinationStrategy")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("None")
                        .HasColumnName("supination_strategy");

                    b.Property<string>("Theme")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("System")
                        .HasColumnName("theme");

                    b.Property<string>("WeightUnit")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("Pound")
                        .HasColumnName("weight_unit");

                    b.HasKey("Id");

                    b.ToTable("app_settings", (string)null);
                });

            modelBuilder.Entity("WorkoutApp.DAL.Entities.Barbell", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<decimal>("MassKg")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMERIC")
                        .HasColumnName("weight_kg")
                        .HasDefaultValueSql("0");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Name" }, "IX_barbells_name")
                        .IsUnique();

                    b.ToTable("barbells", (string)null);
                });

            modelBuilder.Entity("WorkoutApp.DAL.Entities.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<int?>("Barbell")
                        .HasColumnType("INTEGER")
                        .HasColumnName("barbell");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("exercises", (string)null);
                });

            modelBuilder.Entity("WorkoutApp.DAL.Entities.Plate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<decimal>("WeightKg")
                        .HasColumnType("NUMERIC")
                        .HasColumnName("weight_kg");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "WeightKg" }, "IX_plates_weight_kg")
                        .IsUnique();

                    b.ToTable("plates", (string)null);
                });

            modelBuilder.Entity("WorkoutApp.DAL.Entities.Set", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("SetGroupId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SetGroupId");

                    b.ToTable("Set");
                });

            modelBuilder.Entity("WorkoutApp.DAL.Entities.SetGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ExerciseId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("WorkoutId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("SetGroup");
                });

            modelBuilder.Entity("WorkoutApp.DAL.Entities.Workout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<string>("Notes")
                        .HasColumnType("TEXT")
                        .HasColumnName("notes");

                    b.Property<string>("StartedAt")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("started_at");

                    b.Property<string>("StoppedAt")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("stopped_at");

                    b.HasKey("Id");

                    b.ToTable("workouts", (string)null);
                });

            modelBuilder.Entity("WorkoutApp.DAL.Entities.Set", b =>
                {
                    b.HasOne("WorkoutApp.DAL.Entities.SetGroup", null)
                        .WithMany("Sets")
                        .HasForeignKey("SetGroupId");
                });

            modelBuilder.Entity("WorkoutApp.DAL.Entities.SetGroup", b =>
                {
                    b.HasOne("WorkoutApp.DAL.Entities.Exercise", "Exercise")
                        .WithMany()
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WorkoutApp.DAL.Entities.Workout", "Workout")
                        .WithMany("SetGroups")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise");

                    b.Navigation("Workout");
                });

            modelBuilder.Entity("WorkoutApp.DAL.Entities.SetGroup", b =>
                {
                    b.Navigation("Sets");
                });

            modelBuilder.Entity("WorkoutApp.DAL.Entities.Workout", b =>
                {
                    b.Navigation("SetGroups");
                });
#pragma warning restore 612, 618
        }
    }
}
