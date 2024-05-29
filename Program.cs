// Importing necessary namespaces from different parts of the application and external libraries
using Backend_localinezationBackend.Services;
using localinezationBackend.Services;
using localinezationBackend.Services.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

// Create a new builder for the web application using the provided arguments from the command line or environment
var builder = WebApplication.CreateBuilder(args);

// Add services to the container. Services are typically scoped to the request lifecycle meaning they are created per each HTTP request.
// builder.Services.AddScoped<BlogService>(); // Example service that's currently commented out
builder.Services.AddScoped<UserService>(); // Registers UserService for dependency injection
// builder.Services.AddScoped<PasswordService>(); // Registers PasswordService for dependency injection
builder.Services.AddScoped<MediaService>(); // Registers MediaService for dependency injection

// Retrieve the connection string from the configuration file, typically appsettings.json or environment-specific configuration
var connectionString = builder.Configuration.GetConnectionString("MyBlogString");

// Configure Entity Framework Core to use SQL Server as the database provider
// DataContext is the custom DbContext used for interacting with the database
builder.Services.AddDbContext<DataContext>(Options =>
    Options.UseSqlServer(connectionString)); // Uses the SQL Server database provider with the provided connection string

// Configure Cross-Origin Resource Sharing (CORS) to allow requests from specified origins
builder.Services.AddCors(options => options.AddPolicy("BlogPolicy", builder =>
{
    builder.WithOrigins("http://localhost:3000", // Allows requests from the frontend development server
                        "http://localhost:3001", // Allows requests from another potential frontend or service
                        "http://localhost:3002", // Another potential frontend or development service
                        "https://localinezation-front-one.vercel.app", // Production frontend server
                        "https://localinezation.vercel.app", // Another production frontend server
                        "https://localinezation-front.vercel.app") // Another variation of the production frontend
    .AllowAnyHeader() // Allows all headers in CORS requests
    .AllowAnyMethod(); // Allows all methods in CORS requests
}));

// Add support for controllers, enabling MVC or Web API controllers to handle requests
builder.Services.AddControllers();
// Add Swagger generation tool to help build interactive API documentation
builder.Services.AddSwaggerGen();

// Add console logging to capture and display logs in the console
builder.Logging.AddConsole();

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline, which is the sequence of actions an HTTP request goes through
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enables Swagger in development environments
    app.UseSwaggerUI(); // Enables Swagger UI in development environments
}

app.UseCors("BlogPolicy"); // Use the configured CORS policy named "BlogPolicy"

app.UseAuthorization(); // Adds authorization middleware to the pipeline to secure the app

app.MapControllers(); // Maps routes to the controllers

app.Run(); // Runs the application, listening for incoming HTTP requests
