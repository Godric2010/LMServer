namespace LMServer.Services;

public class FileUploadService
{
    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly string _uploadDirectory;

    public FileUploadService()
    {
        var documentsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LMServer");
        _uploadDirectory = Path.Combine(documentsPath, "uploads");
        Directory.CreateDirectory(_uploadDirectory);
    }

    public async Task<(bool success, string msg, string? filePath)> SaveFileAsync(IFormFile? file)
    {
        if (file is null || file.Length == 0)
        {
            return (false, "No file uploaded or file is empty", null);
        }
        
        var allowedExtensions = new[] { ".blend", ".zip" };
        var fileExtension= Path.GetExtension(file.FileName).ToLower();
        if (!allowedExtensions.Contains(fileExtension))
        {
            return (false, $"File type {fileExtension} is not supported", null);
        }
        
        var filePath = Path.Combine(_uploadDirectory, file.FileName);
        
        await using var fileStream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(fileStream);
        
        return (true, "File uploaded successfully", filePath);
    }
}