using Application.AutoMapperProfile;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistance;
using Persistance.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Injection Start

builder.Services.AddTransient<IDataBaseContext,DataBaseContext>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<IMasterRepository, MasterRepository>();
builder.Services.AddScoped<ISubsidiaryRepository, SubsidiaryRepository>();

builder.Services.AddTransient<IServiceSubsidiary, ServiceSubsidiary>();
builder.Services.AddTransient<IServiceMaster, ServiceMaster>();
builder.Services.AddTransient<IHiracicalReportService, HiracicalReportService>();

//-------------------------------------------------------------------------------------------------------AutoMapper

builder.Services.AddAutoMapper(typeof(MappingProfile));



string connectionString = builder.Configuration["Connection"];

builder.Services.AddEntityFrameworkSqlServer().
    AddDbContext<DataBaseContext>
    (option => option.UseSqlServer(connectionString));

//--------------------------------------------------JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)// Registers the JWT Bearer authentication handler — so your API expects Authorization: Bearer <token> on all requests.
    .AddJwtBearer(options =>
    {
        options.Authority = "https://localhost:5001"; // Tells the API : “Only accept and validate tokens that were issued by this exact IdentityServer https://localhost:5001”
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true, // The aud (audience) claim in a JWT token tells you who the token is intended for,It’s like saying : "This token is meant to be used by DoctorFitAPI, and no one else."
            ValidAudience = "doctorfit_api",//This Is For aud
            ValidateIssuer = true,
            ValidIssuer = "https://localhost:5001",
            RoleClaimType = "role"
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorCors", policy =>
    {
        policy.WithOrigins("https://localhost:5003")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

//-----------------------------------------------------------------------------------



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("BlazorCors");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
