var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Health check
app.MapGet("/", () => "LMServer up and running");

// Placeholder for file upload
app.MapGet("/upload", () =>
{
    return Results.Ok("File uploaded. (Not implemented yet!)");
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