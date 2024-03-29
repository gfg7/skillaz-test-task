using Microsoft.AspNetCore.Authentication.Cookies;
using ShortLink.Api.Configuration;
using ShortLink.Api.Identity;
using ShortLink.Api.Support;
using ShortLink.Database;
using ShortLink.Services;
using ShortLink.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRouting(x => x.LowercaseUrls = true);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, SignInService>();
builder.Services.AddScoped<IAuthService, SignInService>();

builder.Services.AddScoped<CancellationTokenProvider>();
builder.Services.AddScoped<DbFactory>(x => new DbFactory(EnvService.GetVariable("CONNECTION_STRING")));
builder.Services.AddScoped<IShortLinkRepository, ShortLinkRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ShortLinkService>();
builder.Services.AddScoped<IShortLinkGenerator, ShortLinkGenerator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandlerExtension();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
