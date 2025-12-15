using Kemar.SMS.API.Helper;
using Kemar.SMS.Business.StudentBusiness;
using Kemar.SMS.Business.TeacherBusiness;
using Kemar.SMS.Repository.Context;
using Kemar.SMS.Repository.Repositories.StudentRepo;
using Kemar.SMS.Repository.Repositories.TeacherRepo;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<KemarDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IStudent, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ITeacher, TeacherRepository>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
//builder.Services.AddScoped();

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
