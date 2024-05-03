using Backend_localinezationBackend.Services;
using localinezationBackend.Services;
using localinezationBackend.Services.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<BlogService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped<MediaService>();  // Register MediaService

var connectionString = builder.Configuration.GetConnectionString("MyBlogString");

builder.Services.AddDbContext<DataContext>(Options =>
    Options.UseSqlServer(connectionString));

builder.Services.AddCors(options => options.AddPolicy("BlogPolicy",
builder =>
{
    builder.WithOrigins("http://localhost:3002", "https://localinezation.vercel.app", "https://localinezation-front.vercel.app")
    .AllowAnyHeader()
    .AllowAnyMethod();
}
));

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("BlogPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
