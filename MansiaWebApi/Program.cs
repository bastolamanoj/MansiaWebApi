
using DataProvider.Interfaces;
using DataProvider.Interfaces.Chat;
using DataProvider.Interfaces.Core;
using DataProvider.Interfaces.Users;
using MansiaWebApi.Configuration;
using MansiaWebApi.Hubs;
using MansiaWebApi.Infrastructure;
using MansiaWebApi.Middleware;
using Services.Repository;
using Services.Repository.Chat;
using Services.Repository.Core;
using Services.Repository.Users;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddPresentation()
                .AddDbConfigAndIdentity(builder.Configuration)
                .AddAuthenticationAndAuthorization(builder.Configuration)
                .AddSwaggerGen();

builder.Services.AddSignalR();

builder.Services.AddLogging();

//register global exception handler
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IChatHubConnectionRepository, ChatHubConnectionRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddCors(options =>
    options.AddPolicy("ChatApp", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    })

);

var app = builder.Build();
app.UseCors("ChatApp");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
//Register Exception handling middleware
//app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseExceptionHandler();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/ChatHub");

app.Run();
