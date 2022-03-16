namespace Fading.Api.Tests;

public partial class SqliteApiTests : IDisposable
{
  private readonly DbConnection _connection;
  private readonly DbContextOptions<MessageContext> _contextOptions;

  public SqliteApiTests()
  {
    _connection = new SqliteConnection("Filename=:memory:");
    _connection.Open();

    _contextOptions = new DbContextOptionsBuilder<MessageContext>()
        .UseSqlite(_connection)
        .Options;

    // Create the schema and seed some data
    using var context = new MessageContext(_contextOptions);
    context.Database.EnsureCreated();

    var msgReq1 = new MessageCreationRequest {Title = "Message One", Message = "This is the content on message one"};
    var msgReq2 = new MessageCreationRequest {Title = "Message Two", Message = "This is the content on message two"};
    var msgReq3 = new MessageCreationRequest {Title = "Message Three", Message = "This is the content on message three"};
    var msgReq4 = new MessageCreationRequest {Title = "Message Four", Message = "This is the content on message four"};
    var msgReq5 = new MessageCreationRequest {Title = "Message Five", Message = "This is the content on message five"};
    var msgReq6 = new MessageCreationRequest {Title = "Message Six", Message = "This is the content on message six"};
    var msgReq7 = new MessageCreationRequest {Title = "Message Seven", Message = "This is the content on message seven"};
    var msgReq8 = new MessageCreationRequest {Title = "Message Eight", Message = "This is the content on message eight"};
    var msgReq9 = new MessageCreationRequest {Title = "Message Nine", Message = "This is the content on message nine"};
    var msgReq10 = new MessageCreationRequest {Title = "Message Ten", Message = "This is the content on message ten"};
    var msgReq11 = new MessageCreationRequest {Title = "Message Eleven", Message = "This is the content on message eleven"};
    var msgReq12 = new MessageCreationRequest {Title = "Message Twelve", Message = "This is the content on message twelve"};
    
    context.AddRange(
      msgReq1.ToTemporaryMessage(),
      msgReq2.ToTemporaryMessage(),
      msgReq3.ToTemporaryMessage(),
      msgReq4.ToTemporaryMessage(),
      msgReq5.ToTemporaryMessage(),
      msgReq6.ToTemporaryMessage(),
      msgReq7.ToTemporaryMessage(),
      msgReq8.ToTemporaryMessage(),
      msgReq9.ToTemporaryMessage(),
      msgReq10.ToTemporaryMessage(),
      msgReq11.ToTemporaryMessage(),
      msgReq12.ToTemporaryMessage()
    );
    context.SaveChanges();
  }

  MessageContext CreateContext() => new MessageContext(_contextOptions);
  public void Dispose() => _connection.Dispose();

  [Fact]
  public async void Should_Get_all_Messages()
  {
    // Arrange
    using var context = CreateContext();
    var controller = new MessageController(context);

    // Act
    var messagesTask = await controller.GetAllMessages();
    var result = messagesTask.Value;

    // Assert
    result!.Count().Should().Be(12);
  }
}
