using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using UserRetention.DataBase;
using UserRetention.DataBase.DTO;
using UserRetention.Services;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddLogging();
builder.Services.AddHttpLogging(opts =>
opts.LoggingFields = HttpLoggingFields.RequestProperties);
builder.Logging.AddFilter("Microsoft.AspNetCore.HttpLogging.HttpLoggingMiddleware", LogLevel.Information);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddScoped<IUserManagement, UserManagement>();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseHttpLogging();
}

app.UseHttpsRedirection();
app.MapPost("/added", async (RequestUser newUser, IUserManagement userManagement) =>
{
    var result = await userManagement.AddUserAsync(newUser);
    if (result == null)
    {
        return Results.BadRequest("User request cannot be null.");
    }
    if (!result.Success)
    {
        return Results.Problem(result.Message, statusCode: result.StatusCode);
    }
    if (result.Data == null)
    {
        return Results.Problem(result.Message, statusCode: result.StatusCode);
    }
    return Results.Created($"/added/{result.Data.Email}", result.Data);

});

app.MapGet("/get/{email}", async (string email, IUserManagement userManagement) =>
{
    var result = await userManagement.GetUserAsync(email);
    if (result == null)
    {
        return Results.NotFound("User not found.");
    }
    return Results.Ok(result);
});

app.MapPut("/update/{email}", async (string email, RequestUser userRequest, IUserManagement userManagement) =>
{
    var result = await userManagement.UpdateUserAsync(email, userRequest);
    if (result == null)
    {
        return Results.BadRequest("User request cannot be null.");
    }
    if (!result.Success)
    {
        return Results.Problem(result.Message, statusCode: result.StatusCode);
    }
    if (result.Data == null)
    {
        return Results.NotFound("User not found.");
    }
    return Results.Ok(result.Data);
});

app.MapGet("/getall", async (IUserManagement userManagement) =>
{
    var result = await userManagement.GetAllUsersAsync();
    if (result == null || !result.Any())
    {
        return Results.NotFound("No users found.");
    }
    return Results.Ok(result);
});

app.MapDelete("/delete/{email}", async (string email, IUserManagement userManagement) =>
{
    var result = await userManagement.DeleteUserAsync(email);
    if (result == null)
    {
        return Results.NotFound("User not found.");
    }
    if (!result.Success)
    {
        return Results.Problem(result.Message, statusCode: result.StatusCode);
    }
    return Results.Ok(result.Data);
});

app.Run();
