using System.Data.Entity;
using TrinityAttenTrace.Model;

namespace TrinityAttenTrace.Data
{
    public class AttenTraceContext : DbContext
    {
        public AttenTraceContext() : base("default")
        {

        }


        public DbSet<AdviserModel> Advisers { get; set; }
        public DbSet<AnnouncementModel> Announcements { get; set; }
        public DbSet<SectionModel> Sections { get; set; }
        public DbSet<SentMessagesModel> SentMessages { get; set; }
        public DbSet<StudentInModel> StudentIns { get; set; }
        public DbSet<StudentOutModel> StudentOuts { get; set; }
        public DbSet<StudentSectionModel> StudentsSection { get; set; }
        public DbSet<StudentsModel> Students { get; set; }
        public DbSet<UserModel> Users { get; set; }

    }
}
