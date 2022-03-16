using Fading.Api.Models;

namespace Fading.Api.Data;
public class MessageContext : DbContext
{
  public MessageContext(DbContextOptions<MessageContext> options) : base(options) { }

  public DbSet<TemporaryMessage> Messages { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<TemporaryMessage>(m =>
   {
     m.ToTable("temp_messages");
     m.Property(a => a.Id).HasColumnName("id");
     m.Property(a => a.Title).HasColumnName("title").IsRequired().HasMaxLength(120);
     m.Property(a => a.Message).HasColumnName("artist").HasMaxLength(255);
     m.Property(a => a.BirthTime).HasColumnName("birth_time").HasColumnType("DateTime2");
     m.Property(a => a.DeathTime).HasColumnName("death_time").HasColumnType("DateTime2");
   });

    modelBuilder.Ignore<MessageCreationRequest>();
  }
}
