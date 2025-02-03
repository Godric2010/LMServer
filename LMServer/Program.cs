using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
Directory.CreateDirectory(uploadDirectory);

// Health check
app.MapGet("/", () => "LMServer up and running");

// Placeholder for file upload
app.MapPost("/upload", async (HttpContext context) =>
{
    var form = await context.Request.ReadFormAsync();
    var file = form.Files.FirstOrDefault();

    if (file == null || file.Length == 0)
    {
        return Results.BadRequest("No file uploaded or file is empty");
    }

    var allowedExtensions = new[] { ".blend" };
    var fileExtension = Path.GetExtension(file.FileName).ToLower();

    if (!allowedExtensions.Contains(fileExtension))
    {
        return Results.BadRequest($"File extension {fileExtension} is not supported.");
    }

    if (file.Length > 50 * 1024 * 1024)
    {
        return Results.BadRequest($"File to big. (Max. 50MB)");
    }
    
    var filePath = Path.Combine(uploadDirectory, file.FileName);
    await using var filestream = new FileStream(filePath, FileMode.Create);
    await file.CopyToAsync(filestream);
    return Results.Ok($"File uploaded: {filePath}");
});

// Placeholder for status updates
app.MapGet("/status", () =>
{
    return Results.Json(new { status = "ready", rendering = false });
});

// Placeholder for file download
app.MapGet("/downlaod", () =>
{
    return Results.Ok("File downloaded. (Not implemented yet!)");
});

app.Run();