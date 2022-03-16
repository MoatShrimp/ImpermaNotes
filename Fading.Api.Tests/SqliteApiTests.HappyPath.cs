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

    context.AddRange(
      new TemporaryMessage("Message One", "This is the content on message one")
        {DeathTime = DateTime.Now.AddHours(2)},
      new TemporaryMessage("Message Two", "This is the content on message two")
        {DeathTime = DateTime.Now.AddHours(2)},
      new TemporaryMessage("Message Three", "This is the content on message three")
        {DeathTime = DateTime.Now.AddHours(2)},
      new TemporaryMessage("Message Four", "This is the content on message four")
        {DeathTime = DateTime.Now.AddHours(2)},
      new TemporaryMessage("Message Five", "This is the content on message five")
        {DeathTime = DateTime.Now.AddHours(2)},
      new TemporaryMessage("Message Six", "This is the content on message six")
        {DeathTime = DateTime.Now.AddHours(2)},
      new TemporaryMessage("Message Seven", "This is the content on message seven")
        {DeathTime = DateTime.Now.AddHours(2)},
      new TemporaryMessage("Message Eight", "This is the content on message eight")
        {DeathTime = DateTime.Now.AddHours(2)},
      new TemporaryMessage("Message Nine", "This is the content on message nine")
        {DeathTime = DateTime.Now.AddHours(2)},
      new TemporaryMessage("Message Ten", "This is the content on message ten")
        {DeathTime = DateTime.Now.AddHours(2)}
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

  [Fact]
  public async void Should_Add_One_Included_Message()
  {
    // Arrange
    using var context = CreateContext();
    var controller = new MessageController(context);
    
    // Act
    context.Add(
      new TemporaryMessage("Message Another", "This is the content on message Another")
        {DeathTime = DateTime.Now.AddHours(2)}
    );
    context.SaveChanges();
    var messagesTask = await controller.GetAllMessages();
    var result = messagesTask.Value;

    // Assert
    result!.Count().Should().Be(11);
  }

  }
}
