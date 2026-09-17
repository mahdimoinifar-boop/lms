
using Microsoft.EntityFrameworkCore;
using LmsProject.Models.Entities;
namespace LmsProject.Data

{
    public class LmsDbContext:DbContext
    {
        public LmsDbContext(DbContextOptions<LmsDbContext> options): base(options) { }


        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories {  get; set; }


        public DbSet<Course> Courses { get; set; }

        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<Enrollment> Enrollments { get; set; }

        public DbSet<Assignment> Assignments { get; set; }

        public DbSet<AssignmentSubmission> AssignmentSubmissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //user

            modelBuilder.Entity<User>()
           . HasIndex(user =>user.Username)
          . IsUnique();

            modelBuilder.Entity<User>()
                . HasIndex(user => user.Email)
                . IsUnique();

            //category

            modelBuilder.Entity<Category>()
            .HasIndex(Category => Category.Name)
            .IsUnique();

            //course -> teacher

            modelBuilder.Entity<Course>()
            .HasOne(course => course.Teacher)
            .WithMany(user => user.CoursesTaught)
            .HasForeignKey(course => course.TeacherId)
           .OnDelete(DeleteBehavior.Restrict);

            // Course->Lesson

            modelBuilder.Entity<Lesson>()
                .HasOne(lesson => lesson.Course)
                .WithMany(course => course.Lessons)
                .HasForeignKey(lesson => lesson.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            //course -> Category

            modelBuilder.Entity<Course>()
                .HasOne(course => course.Category)
                .WithMany(category => category.Courses)
                .HasForeignKey(course => course.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            //Enrollment->Course
            modelBuilder.Entity<Enrollment>()
                .HasOne(enrollment => enrollment.Course)
               . WithMany(course => course.Enrollments)
                .HasForeignKey(enrollment => enrollment.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            //prevent duplicated registration
            modelBuilder.Entity<Enrollment>()
               .HasIndex(enrollment => new
               {
                   enrollment.StudentId,
                   enrollment.CourseId
               })
               .IsUnique();

            // Course -> Assignment
            modelBuilder.Entity<Assignment>()
              .HasOne(assignment => assignment.Course)
              .WithMany(course => course.Assignments)
              .HasForeignKey(assignment => assignment.CourseId)
              .OnDelete(DeleteBehavior.Cascade);

            // AssignmentSubmission -> Assignment
            modelBuilder.Entity<AssignmentSubmission>()
               .HasOne(submission => submission.Assignment)
               .WithMany(assignment => assignment.Submissions)
               .HasForeignKey(submission => submission.AssignmentId)
               .OnDelete(DeleteBehavior.Cascade);


            // AssignmentSubmission -> Student.
            modelBuilder.Entity<AssignmentSubmission>()
    .HasKey(submission => submission.SubmissionId);

            modelBuilder.Entity<AssignmentSubmission>()
                .HasOne(submission => submission.Student)
                .WithMany(user => user.Submissions)
                .HasForeignKey(submission => submission.StudentId)
                .OnDelete(DeleteBehavior.Restrict);


            // for each student ariver on  Submission 

            modelBuilder.Entity<AssignmentSubmission>()
                .HasIndex(submission => new
                {
                    submission.AssignmentId,
                    submission.StudentId
                })
                .IsUnique();


            //price course

            modelBuilder.Entity<Course>()
                .Property(course => course.Price)
                .HasPrecision(18, 2);
        }


    }
}
