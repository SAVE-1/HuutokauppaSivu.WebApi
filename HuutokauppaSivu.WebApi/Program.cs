using Huutokauppa_sivu.Server.Data;
using Huutokauppa_sivu.Server.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MvcMovie.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IItem, MagicalItemsService>();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// https://learn.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-8.0
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy
                          .WithOrigins(
                             "http://localhost:8080",
                             "https://localhost:8080",

                             "http://localhost:8080/login",
                             "https://localhost:8080/login",

                             "http://localhost",
                             "https://localhost",

                             "http://localhost:7266/Login",
                             "https://localhost:7266/Login",

                             "http://localhost:7266",
                             "https://localhost:7266",
                             "127.0.0.1"
                           )
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          //.SetIsOriginAllowed(origin => true) // allow any origin 
                          .AllowCredentials();

                      });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddDbContext<MagicalItemsContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<MagicalItemsContext>();


var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<IdentityUser>();

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();


app.MapControllers();

app.Run();
