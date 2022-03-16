using Fading.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MessageContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MessageContext")));

builder.Services.AddCors();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000", "https://localhost:3000"));
app.UseHttpsRedirection();
app.MapControllers();
app.Run();


