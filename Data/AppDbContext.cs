namespace Crud.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        // Define DbSets for your entities here
        // public DbSet<YourEntity> YourEntities { get; set; }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<CourseInstructor> CourseInstructors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure your entity mappings here if needed

            modelBuilder.Entity<Student>()
                    .HasOne(s => s.Course)
                    .WithMany(c => c.Students)
                    .HasForeignKey(s => s.CourseId);

            modelBuilder.Entity<CourseInstructor>()
                    .HasKey(ci => new { ci.InstructorId, ci.CourseId }); // Composite key


            modelBuilder.Entity<Course>()
                        .HasMany(c => c.CourseInstructor)
                        .WithOne(ci => ci.Course)
                        .HasForeignKey(ci => ci.CourseId);


            modelBuilder.Entity<Instructor>()
                        .HasMany(i => i.CourseInstructor)
                        .WithOne(ci => ci.Instructor)
                        .HasForeignKey(ci => ci.InstructorId);

                modelBuilder.Entity<Instructor>().HasData(
                 new Instructor { Id = 1, Name = "Eng/ Mostafa Elnahas", Specialization = "Full Stack" },
                 new Instructor { Id = 2, Name = "Eng/ Mahmoud Mohamed", Specialization = "Full Stack" },
                 new Instructor { Id = 3, Name = "Eng/ Mohamed El helaly", Specialization = "Full Stack" }
    );

        }
    }
}
