using LemonEdge.Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IChessPieceFactory, ChessPieceFactory>();

var app = builder.Build();

app.UseCors(cors => cors
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials()
);
// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
