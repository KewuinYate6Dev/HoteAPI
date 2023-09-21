using Microsoft.EntityFrameworkCore;
using HotelWeb.Api.Models;
using HotelWeb.Api.Web.Util;
using Microsoft.OpenApi.Models;
using HotelWeb.Api.Models.Util;
using Google.Protobuf.WellKnownTypes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    { Title = "Hotel API", Version = "v1" });

    //Obtenemos el directorio actual
    var basePath = AppContext.BaseDirectory;
    var assemblyName = System.Reflection.Assembly
                  .GetEntryAssembly()!.GetName().Name;
    var fileName = System.IO.Path
                  .GetFileName(assemblyName + ".xml");
    var xmlPath = Path.Combine(basePath, fileName);
    c.IncludeXmlComments(xmlPath);

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });


});

var connectionMysql = builder.Configuration.GetConnectionString(Constants.ConnectionName);

#region Add appsettingsJson to IOption configuration

var appSettingsSection = builder.Configuration.GetSection("AppSettings");

builder.Services.Configure<AppSettings>(appSettingsSection);

var appSettings = appSettingsSection.Get<AppSettings>();
#endregion

builder.Services.AddJwtBearerHotel(appSettings!.SecrectKey);


builder.Services.AddServicesHotel(connectionMysql!);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hotel API");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
