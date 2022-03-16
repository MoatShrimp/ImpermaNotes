using Fading.Api.Models;
using Fading.Api.Data;

namespace Fading.Api.Controllers
{
  [ApiController, Route("api/Messages")]
  public class MessageController : ControllerBase
  {
    private readonly MessageContext _context;
    public MessageController(MessageContext context)
    => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TemporaryMessage>>> GetAllMessages() 
    {
      _context.Messages.RemoveRange(_context.Messages.Where(msg => msg.DeathTime < DateTime.Now.AddHours(1)));
      await _context.SaveChangesAsync();
      return await _context.Messages.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TemporaryMessage>> GetMessage(int id)
    {
      var message = await _context.Messages.FindAsync(id);
      if (message is null) return NotFound($"No message with the id: '{id}' was found!");
      return message;
    }

    [HttpPost]
    public async Task<ActionResult<TemporaryMessage>> NewMessage([Required] MessageCreationRequest message)
    {
      if (message is null || message.Title is null)
      {
        return BadRequest("You need a message title!");
      }
      var tempMessage = message.ToTemporaryMessage();
      await _context.Messages.AddAsync(tempMessage);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetMessage", new { id = tempMessage.Id }, tempMessage);
    }

    private bool MessageExists(int id) => _context.Messages.Any(e => e.Id == id);
  }
}
