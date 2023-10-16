using System;
using System.Net;
using System.Text;
using AutoMapper;
using CardStorage.Data;
using CardStorage.Mappings;
using CardStorage.Models.Requests;
using CardStorage.Models.Validators;
using CardStorage.Services.Authentication;
using CardStorage.Services.Authentication.Impl;
using CardStorage.Services.Repository;
using CardStorage.Services.Repository.Impl;
using CardStorage.Utils;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Web;
using SwaggerThemes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

/*#region Configure gRPC

builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 5001, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});

builder.Services.AddGrpc();

#endregion*/

#region Configure Logging

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All | HttpLoggingFields.RequestQuery;
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
    logging.RequestHeaders.Add("Authorization");
    logging.RequestHeaders.Add("X-Real-Ip");
    logging.RequestHeaders.Add("X-Forwarded-For");
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Host.UseNLog(new NLogAspNetCoreOptions{ RemoveLoggerFactoryFilter = true });

#endregion

#region Configure Fluent Validator

builder.Services.AddScoped<IValidator<AuthenticationRequest>, AuthenticationRequestValidator>();

#endregion

#region Configure Auto Mapper

var mapperConfiguration = new MapperConfiguration(config =>
{
    config.AddProfile(new MappingsProfile());
});

var mapper = mapperConfiguration.CreateMapper();

builder.Services.AddSingleton(mapper);

#endregion

#region Configure EntityFramework DbContext

builder.Services.AddDbContext<CardStorageDbContext>(options =>
{
    options.UseMySql(
        builder.Configuration["Settings:Database:ConnectionString"],
        new MySqlServerVersion(new Version(8, 0, 34)));
});

#endregion

#region Configure Repostiories

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountSessionRepository, AccountSessionRepository>();

#endregion

#region Configure Services

builder.Services.AddSingleton<IAuthenticateService, AuthenticateService>();

#endregion

#region Configure JWT Tokens

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    #if (DEBUG)
    {  
        options.RequireHttpsMetadata = false;
    }
    #endif
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(PasswordUtils.SecretKey)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

#endregion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "CardStorageService", Version = "v1.0"});
    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Description = "JWT Auth header using bearer scheme (example: \"Bearer token\")",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerThemes(Theme.UniversalDark);
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpLogging();

app.MapControllers();

/*app.UseEndpoints(builder =>
{
    builder.MapGrpcService<ClientService>();
    builder.MapGrpcService<CardService>();
});*/

app.Run();