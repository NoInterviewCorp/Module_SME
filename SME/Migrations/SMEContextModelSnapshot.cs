﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SME.Persistence;

namespace Module_SME.Migrations
{
    [DbContext(typeof(SMEContext))]
    partial class SMEContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SME.Models.Concept", b =>
                {
                    b.Property<string>("ConceptId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("ConceptId");

                    b.ToTable("Concepts");
                });

            modelBuilder.Entity("SME.Models.ConceptQuestion", b =>
                {
                    b.Property<string>("ConceptId");

                    b.Property<string>("QuestionId");

                    b.HasKey("ConceptId", "QuestionId");

                    b.HasIndex("QuestionId");

                    b.ToTable("ConceptQuestions");
                });

            modelBuilder.Entity("SME.Models.ConceptTechnology", b =>
                {
                    b.Property<string>("ConceptId");

                    b.Property<string>("TechnologyId");

                    b.HasKey("ConceptId", "TechnologyId");

                    b.HasIndex("TechnologyId");

                    b.ToTable("ConceptTechnologies");
                });

            modelBuilder.Entity("SME.Models.LearningPlan", b =>
                {
                    b.Property<string>("LearningPlanId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("HasPublished");

                    b.Property<string>("Name");

                    b.Property<string>("TechnologyId");

                    b.Property<string>("UserName");

                    b.HasKey("LearningPlanId");

                    b.HasIndex("TechnologyId");

                    b.ToTable("LearningPlan");
                });

            modelBuilder.Entity("SME.Models.Option", b =>
                {
                    b.Property<string>("OptionId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<bool>("IsCorrect");

                    b.Property<string>("QuestionId");

                    b.Property<string>("QuestionId1");

                    b.HasKey("OptionId");

                    b.HasIndex("QuestionId");

                    b.HasIndex("QuestionId1");

                    b.ToTable("Options");
                });

            modelBuilder.Entity("SME.Models.Question", b =>
                {
                    b.Property<string>("QuestionId")
                        .ValueGeneratedOnAdd();

                    b.Property<byte>("BloomLevel");

                    b.Property<bool>("HasPublished");

                    b.Property<string>("ProblemStatement")
                        .IsRequired();

                    b.Property<string>("ResourceId");

                    b.HasKey("QuestionId");

                    b.HasIndex("ResourceId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("SME.Models.Resource", b =>
                {
                    b.Property<string>("ResourceId")
                        .ValueGeneratedOnAdd();

                    b.Property<byte>("BloomLevel");

                    b.Property<bool>("HasPublished");

                    b.Property<string>("ResourceLink")
                        .IsRequired();

                    b.HasKey("ResourceId");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("SME.Models.ResourceConcept", b =>
                {
                    b.Property<string>("ConceptId");

                    b.Property<string>("ResourceId");

                    b.HasKey("ConceptId", "ResourceId");

                    b.HasIndex("ResourceId");

                    b.ToTable("ResourceConcepts");
                });

            modelBuilder.Entity("SME.Models.ResourceTechnology", b =>
                {
                    b.Property<string>("ResourceId");

                    b.Property<string>("TechnologyId");

                    b.HasKey("ResourceId", "TechnologyId");

                    b.HasIndex("TechnologyId");

                    b.ToTable("ResourceTechnologies");
                });

            modelBuilder.Entity("SME.Models.ResourceTopic", b =>
                {
                    b.Property<string>("ResourceId");

                    b.Property<string>("TopicId");

                    b.HasKey("ResourceId", "TopicId");

                    b.HasIndex("TopicId");

                    b.ToTable("ResourceTopics");
                });

            modelBuilder.Entity("SME.Models.Technology", b =>
                {
                    b.Property<string>("TechnologyId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("TechnologyId");

                    b.ToTable("Technologies");
                });

            modelBuilder.Entity("SME.Models.Topic", b =>
                {
                    b.Property<string>("TopicId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LearningPlanId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("TopicId");

                    b.HasIndex("LearningPlanId");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("SME.Models.ConceptQuestion", b =>
                {
                    b.HasOne("SME.Models.Concept", "Concept")
                        .WithMany("ConceptQuestions")
                        .HasForeignKey("ConceptId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SME.Models.Question", "Question")
                        .WithMany("ConceptQuestions")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SME.Models.ConceptTechnology", b =>
                {
                    b.HasOne("SME.Models.Concept", "Concept")
                        .WithMany("ConceptTechnologies")
                        .HasForeignKey("ConceptId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SME.Models.Technology", "Technology")
                        .WithMany("ConceptTechnologies")
                        .HasForeignKey("TechnologyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SME.Models.LearningPlan", b =>
                {
                    b.HasOne("SME.Models.Technology", "Technology")
                        .WithMany("LearningPlans")
                        .HasForeignKey("TechnologyId");
                });

            modelBuilder.Entity("SME.Models.Option", b =>
                {
                    b.HasOne("SME.Models.Question")
                        .WithMany("Options")
                        .HasForeignKey("QuestionId");

                    b.HasOne("SME.Models.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId1");
                });

            modelBuilder.Entity("SME.Models.Question", b =>
                {
                    b.HasOne("SME.Models.Resource", "Resource")
                        .WithMany("Questions")
                        .HasForeignKey("ResourceId");
                });

            modelBuilder.Entity("SME.Models.ResourceConcept", b =>
                {
                    b.HasOne("SME.Models.Concept", "Concept")
                        .WithMany("ResourceConcepts")
                        .HasForeignKey("ConceptId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SME.Models.Resource", "Resource")
                        .WithMany("ResourceConcepts")
                        .HasForeignKey("ResourceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SME.Models.ResourceTechnology", b =>
                {
                    b.HasOne("SME.Models.Resource", "Resource")
                        .WithMany("ResourceTechnologies")
                        .HasForeignKey("ResourceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SME.Models.Technology", "Technology")
                        .WithMany("ResourceTechnologies")
                        .HasForeignKey("TechnologyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SME.Models.ResourceTopic", b =>
                {
                    b.HasOne("SME.Models.Resource", "Resource")
                        .WithMany("ResourceTopics")
                        .HasForeignKey("ResourceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SME.Models.Topic", "Topic")
                        .WithMany("ResourceTopics")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SME.Models.Topic", b =>
                {
                    b.HasOne("SME.Models.LearningPlan", "LearningPlan")
                        .WithMany("Topics")
                        .HasForeignKey("LearningPlanId");
                });
#pragma warning restore 612, 618
        }
    }
}
