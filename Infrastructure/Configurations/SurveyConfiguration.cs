using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configurations
{
    public class SurveyConfiguration : IEntityTypeConfiguration<Survey>
    {
        public void Configure(EntityTypeBuilder<Survey> builder)
        {
            builder.ToTable("Survey");

            builder.HasKey(x => x.Id)
                .HasName("survey_id");

            builder.Property(x => x.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.MiddleName)
                .HasColumnName("middle_name")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasColumnName("last_name")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Birthdate)
                .HasColumnName("birthdate")
                .IsRequired();

            builder.Property(x => x.IsFirstAppointment)
                .HasColumnName("is_first_appointment")
                .IsRequired();

            builder.Property(x => x.MoreThen50)
                .HasColumnName("more_then_50")
                .IsRequired();

            builder.Property(x => x.CureType)
                .HasColumnName("cure_type")
                .IsRequired();

            builder.Property(x => x.SuccessTherapy)
                .HasColumnName("success_therapy")
                .IsRequired();
        }
    }
}
