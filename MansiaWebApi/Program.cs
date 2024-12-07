
using DataProvider.Interfaces;
using DataProvider.Interfaces.Users;

using MansiaWebApi.Configuration;
using MansiaWebApi.Hubs;

using Services.Repository;
using Services.Repository.Users;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddPresentation()
                .AddDbConfigAndIdentity(builder.Configuration)
                .AddAuthenticationAndAuthorization(builder.Configuration)
                .AddSwaggerGen();

builder.Services.AddSignalR();

builder.Services.AddLogging();

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.AddCors(options =>
    options.AddPolicy("ChatApp", builder =>
    {
        builder.WithOrigins("http://localhost:3000/")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    })

);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/ChatHub");

app.Run();
