using SignalRPOC.Commons;
using SignalRPOC.Commons.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var settings = builder.Configuration.GetSection("SignalRConfigurations").Get<SignalRConfigurations>();
if (settings is null)
    throw new ArgumentNullException(nameof(settings), "Section \'SignalRConfigurations\' is not in appsettings.json");
builder.Services.RegisterSignalRServices(settings);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapSignalRHubs(settings);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
