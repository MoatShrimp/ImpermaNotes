namespace Fading.Api.Tests;

public partial class SqliteApiTests
{
  [Fact]
  public async void Should_Reject_New_Post_Without_content()
  {
    // Arrange
    using var context = CreateContext();
    var controller = new MessageController(context);
    
    // Act
    var messagesTask = await controller.NewMessage(null);
    var result = messagesTask.Value;

    // Assert
    result!.Should().BeNull();
    messagesTask.Result.Should().BeOfType<BadRequestObjectResult>();
  }
}
