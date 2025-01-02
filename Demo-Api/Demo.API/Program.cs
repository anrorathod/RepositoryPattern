using Microsoft.OpenApi.Models;
using Demo.Data.Contracts;
using Demo.Data.Repositories;
using Demo.Service;
using Demo.Service.Contracts;
using System.Text.Json.Serialization;
using AutoMapper; 
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens; 
using System.Text;
using Demo.Data;
using Demo.Service.Services;
using Demo.API.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors();

// Add services to the container.
builder.Services.Configure<Microsoft.AspNetCore.Http.Features.FormOptions>(x =>
{
    x.ValueLengthLimit = int.MaxValue;
    x.MultipartBodyLengthLimit = int.MaxValue; // In case of multipart
});


builder.Services.AddControllers().AddNewtonsoftJson().AddXmlDataContractSerializerFormatters(); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo()
        {
            Title = "Demo API",
            Version = "V1",
            Description = "This API is design for Demo Project." +
            " Developed in .net Core 8.0",
            TermsOfService = new System.Uri("https://DemoApp.com"),
            Contact = new OpenApiContact()
            {
                Name = "DemoApp",
                Email = "DemoApps@gmail.com",
                Url = new System.Uri("http://www.DemoApp.com")
            },
            License = new OpenApiLicense
            {
                Name = "Use for Demo Project",
                Url = new System.Uri("https://DemoApp.com"),
            }
        });
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                new string[] { }
            }
        });
    }
);

builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
builder.Services.AddScoped<IServiceWrapper, ServiceWrapper>();

builder.Services.AddProblemDetails();
builder.Services.AddApiVersioning();

builder.Services.AddDbContext<DemoDbContext>(options =>
{ 
    options.UseSqlServer(builder.Configuration.GetValue<string>("DemoCon"));
});

//// Register our TokenService dependency
builder.Services.AddScoped<TokenService, TokenService>();

// Support string to enum conversions
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// These will eventually be moved to a secrets file, but for alpha development appsettings is fine
var validIssuer = builder.Configuration.GetValue<string>("JwtTokenSettings:ValidIssuer");
var validAudience = builder.Configuration.GetValue<string>("JwtTokenSettings:ValidAudience");
var symmetricSecurityKey = builder.Configuration.GetValue<string>("JwtTokenSettings:SymmetricSecurityKey");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.IncludeErrorDetails = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ClockSkew = TimeSpan.Zero,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = validIssuer,
        ValidAudience = validAudience,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(symmetricSecurityKey)
        ),
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(builder => builder
               .AllowAnyHeader()
               .AllowAnyMethod()
               .SetIsOriginAllowed((host) => true)
               .AllowCredentials()
           );
app.UseAuthorization();

app.MapControllers();

app.Run();
