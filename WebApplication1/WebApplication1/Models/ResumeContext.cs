using System.Data.Entity;

namespace WebApplication1.Models
{
    public class ResumeContext : DbContext
    {
        public DbSet<Ability> Ability { get; set; }
        public DbSet<AdditionalEducation> AdditionalEducation { get; set; }
        public DbSet<CareerHistory> CareerHistory { get; set; }
        public DbSet<DesiredPosition> DesiredPosition { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<FormOfTraining> FormOfTraining { get; set; }
        public DbSet<Industry> Industry { get; set; }
        public DbSet<KnowledgeOfForeignLanguages> KnowledgeOfForeignLanguages { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<Level> Level { get; set; }
        public DbSet<Profile> Profile { get; set; }
        public DbSet<Resume> Resume { get; set; }
        public DbSet<Skill> Skill { get; set; }
        public DbSet<SkillLevel> SkillLevel { get; set; }
        public DbSet<SourceOfInformation> SourceOfInformation { get; set; }
        public DbSet<TheLevelOfLanguageLearning> TheLevelOfLanguageLearning { get; set; }
        public DbSet<ThePost> ThePost { get; set; }
        public DbSet<TheTypeOfTraining> TheTypeOfTraining { get; set; }



    }
}