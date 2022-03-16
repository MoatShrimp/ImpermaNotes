namespace Fading.Api.Models;
public class TemporaryMessage
{
  public static Random Random = new Random();
  public TemporaryMessage(string title, string message)
  {
    var currentTime = DateTime.Now;
    var usedTime = new DateTime(currentTime.Ticks - (currentTime.Ticks % TimeSpan.TicksPerSecond));
    Title = title;
    Message = message;
    BirthTime = usedTime;
    DeathTime = usedTime.AddSeconds(Random.Next(15, 180));
  }

  public int Id { get; set; }
  public string Title { get; set; }
  public string Message { get; set; }
  public DateTime BirthTime { get; set; }
  public DateTime DeathTime { get; set; }
}