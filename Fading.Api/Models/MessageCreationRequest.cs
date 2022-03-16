namespace Fading.Api.Models;
public class MessageCreationRequest
{
  public string Title { get; set; }
  public string Message { get; set; }

  public TemporaryMessage ToTemporaryMessage() => new TemporaryMessage(Title, Message);
}