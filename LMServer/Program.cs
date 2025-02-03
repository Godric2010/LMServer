using LMServer.Models;
using LMServer.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<FileUploadService>();

var app = builder.Build();

var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
Directory.CreateDirectory(uploadDirectory);

// Health check
app.MapGet("/", () => "LMServer up and running");

// Placeholder for file upload
app.MapPost("/upload", async (HttpContext context, FileUploadService uploadService) =>
{
    var form = await context.Request.ReadFormAsync();
    var file = form.Files.FirstOrDefault();

    var (success, msg, filePath) = await uploadService.SaveFileAsync(file);
    return success ? Results.Ok(new UploadResult(true, msg, filePath)) : Results.BadRequest(new UploadResult(false, msg));
    
});

// Placeholder for status updates
app.MapGet("/status", () =>
{
    return Results.Json(new { status = "ready", rendering = false });
});

// Placeholder for file download
app.MapGet("/download", () =>
{
    return Results.Ok("File downloaded. (Not implemented yet!)");
});

app.Run();