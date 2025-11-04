using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using Repository.Entities;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mock
{
    public class Database:DbContext,IContext
    {
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Survey> Surveys { get; set; }

        public void Save()
        {
            SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-SSNMLFD; database=Data++;trusted_connection=true; TrustServerCertificate=True");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // קשר של 1 לרבים - כל משתמש יכול ליצור כמה סקרים
            modelBuilder.Entity<Survey>()
                .HasOne(s => s.Surveyor)
                .WithMany(u => u.OwnSurveys)
                .HasForeignKey(s => s.SurveyorId)
                .OnDelete(DeleteBehavior.NoAction); // משנה ל-NoAction למנוע בעיות

            // קשר של רבים לרבים - משתמשים יכולים לענות על כמה סקרים
            modelBuilder.Entity<Survey>()
                .HasMany(s => s.Respondents)
                .WithMany(u => u.AnsweredSurveys)
                .UsingEntity(j => j.ToTable("UserSurveys"));



            // קשר של תשובות למשתמש (מי ענה על השאלה)
            modelBuilder.Entity<Answer>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Restrict למנוע בעיות

            // קשר של תשובות לשאלה
            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);


         

        }







    }

}
