using AzurePractice.RequestSender.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient("receiver", config =>
{
    config.BaseAddress = new Uri(builder.Configuration.GetSection("ReceiverDetails:BaseAddress").Value);
    config.Timeout = new TimeSpan(0, 0, 20);
});
builder.Services.AddScoped<IExternalServices, ExternalServices>();
var app = builder.Build();

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