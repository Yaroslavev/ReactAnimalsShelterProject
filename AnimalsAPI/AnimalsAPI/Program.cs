using AnimalsAPI;
using Core.IServices;
using Core.MapperProfiles;
using Core.Services;
using Data;
using Data.Enteties;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

var builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("LocalDb")!;

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AnimalsDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddAutoMapper(typeof(AppProfile));

builder.Services.AddIdentity<User, IdentityRole>(options => 
    options.SignIn.RequireConfirmedAccount = false)
    .AddDefaultTokenProviders()
    .AddSignInManager()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AnimalsDbContext>();

builder.Services.AddScoped<IAccountsService, AccountsService>();
builder.Services.AddScoped<IFilesService, FilesService>();

builder.Services.AddCors(options =>
    options.AddPolicy("AllowLocalhost", policy => {
        policy.WithOrigins("http://localhost:5173", "https://localhost:5173", "https://localhost:3000", "http://localhost:3000")
              .AllowAnyHeader()
              .AllowCredentials()
              .AllowAnyMethod();
    }));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseCors("AllowLocalhost");

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AnimalsDbContext>();
    DbInitializer.Initialize(context);
}

app.Run();
