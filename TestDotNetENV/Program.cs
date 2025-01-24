//dotnet run --launch-profile https
//dotnet run --launch-profile http

var builder = WebApplication.CreateBuilder(args);

var env = Environment.GetEnvironmentVariable("ENV");
// Add services to the container.
DotNetEnv.Env.Load($"{env}.env");

var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DBCon");
if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception("Connection string not found. Ensure the .env file is correctly configured and placed in the root directory.");
}
//Add connection string to the applications configuration system
Console.WriteLine(connectionString);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
