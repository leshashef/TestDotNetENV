//dotnet run --launch-profile https
//dotnet run --launch-profile http

using DotNetEnv.Configuration;

var builder = WebApplication.CreateBuilder(args);

var env = Environment.GetEnvironmentVariable("ENV"); // получаем данные из launchSettings.json

DotNetEnv.Env.Load($"{env}.env"); // добавляем в Environment данные из .env 

var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DBCon"); // получаем добавленные данные
if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception("Connection string not found. Ensure the .env file is correctly configured and placed in the root directory.");
}
Console.WriteLine(connectionString);

string getAppsetting = builder.Configuration.GetSection("LogFile").Get<string>(); // получаем данные из appsetting.json
Console.WriteLine(getAppsetting);

var test = Environment.GetEnvironmentVariable("TestData"); 
Console.WriteLine(test);

builder.Configuration.AddDotNetEnv($"{env}.env"); // добавляем в контейнер конфигурации .env 
string trygetenvConf = builder.Configuration.GetSection("TestData").Get<string>(); // после добавления можем его получить 
Console.WriteLine(trygetenvConf);

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
