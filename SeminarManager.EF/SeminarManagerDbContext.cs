using System;
using Microsoft.EntityFrameworkCore;
using SeminarManager.Model;

namespace SeminarManager.EF
{
    public class SeminarManagerContext : DbContext
    {
        private string connection_string;
        public DbSet<Person> Persons { get; set; }
        public DbSet<Seminar> Seminars { get; set; }
        public DbSet<Attendee> Attendees { get; set; }

        public SeminarManagerContext()
        {
            connection_string = string.Format("Host={0};Username={1};Password={2};Database={3}",
                Helper.GetFromEnvironmentOrDefault("POSTGRESQL_HOST", "localhost"),
                Helper.GetFromEnvironmentOrDefault("POSTGRESQL_USER", "postgres"),
                Helper.GetFromEnvironmentOrDefault("POSTGRESQL_PASSWORD", "secret"),
                Helper.GetFromEnvironmentOrDefault("POSTGRESQL_DATABASE", "postgres"));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connection_string);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Person>();

            modelBuilder.Entity<Seminar>()
                .HasOne<Person>()
                .WithMany()
                .HasForeignKey(s => s.TeacherID);

            modelBuilder.Entity<Attendee>()
                .HasKey(a => new { a.PersonID, a.SeminarID });

            modelBuilder.Entity<Attendee>()
                .HasOne<Person>()
                .WithMany()
                .HasForeignKey(a => a.PersonID);

            modelBuilder.Entity<Attendee>()
                .HasOne<Seminar>()
                .WithMany()
                .HasForeignKey(a => a.SeminarID);
        }
    } 
}