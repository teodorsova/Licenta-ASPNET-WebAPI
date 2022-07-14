using auth.Data;
using auth.Helpers;
using Microsoft.EntityFrameworkCore;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Azure.Services.AppAuthentication;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((context, config) => {
    var buildConfiguration = config.Build();
    

    string kvURL = buildConfiguration["KeyVaultConfig:KVUrl"];
    string tenantId = buildConfiguration["KeyVaultConfig:TenantId"];
    string clientId = buildConfiguration["KeyVaultConfig:ClientId"];
    string clientSecret = buildConfiguration["KeyVaultConfig:ClientSecret"];

    var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);

    var client = new SecretClient(new Uri(kvURL), credential);
    config.AddAzureKeyVault(client, new AzureKeyVaultConfigurationOptions());
});

builder.Services.AddCors(options => {
    options.AddPolicy(name: "MyPolicy",
    builder => 
    {
        builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
    });
});

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseNpgsql(builder.Configuration["PostgreSQLConnectionString"]);
    
});

builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<ISubscriptionRepo, SubcriptionRepo>();
builder.Services.AddScoped<IOrderRepo, OrderRepo>();
builder.Services.AddScoped<IFurnitureRepo, FurnitureRepo>();
builder.Services.AddScoped<IJwtService, JwtService>();
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


app.UseCors("MyPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
