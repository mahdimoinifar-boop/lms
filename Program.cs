using System.Text;
using LmsProject.Mapping;
using LmsProject.Data;
using Microsoft.OpenApi.Models;
using LmsProject.Models.Entities;
using LmsProject.Repositories;
using LmsProject.Repositories.Interfaces;
using LmsProject.Services;
using LmsProject.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// ==================== Database ====================

builder.Services.AddDbContext<LmsDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);
builder.Services.AddScoped<ICategoryService, CategoryService>();
// ==================== AutoMapper ====================
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});
// ==================== Repositories ====================

builder.Services.AddScoped(
    typeof(IRepository<>),
    typeof(Repository<>));

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ICourseRepository, CourseRepository>();

builder.Services.AddScoped<ILessonRepository, LessonRepository>();

builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
// ==================== Services ====================
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ICourseService, CourseService>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
// ==================== Password Hasher ====================

builder.Services.AddScoped<
    IPasswordHasher<User>,
    PasswordHasher<User>
>();

// ==================== JWT Authentication ====================

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,

        IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),

        ValidateIssuer = true,

        ValidIssuer =
            builder.Configuration["Jwt:Issuer"],

        ValidateAudience = true,

        ValidAudience =
            builder.Configuration["Jwt:Audience"],

        ValidateLifetime = true,

        ClockSkew = TimeSpan.Zero
    };
});

// ==================== Controllers ====================

builder.Services.AddControllers();

// ==================== Swagger ====================

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Enter: Bearer {your JWT token}"
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




//



builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>();

builder.Services.AddScoped<IAssignmentService, AssignmentService>();

// ==================== Build App ====================

var app = builder.Build();

// ==================== HTTP Pipeline ====================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();