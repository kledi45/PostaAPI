using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

public class UploadDocumentHelper
{
    private readonly string _storagePath;
    private readonly IWebHostEnvironment _environment;

    public UploadDocumentHelper(IWebHostEnvironment environment, string subFolder = "uploads")
    {
        _environment = environment;
        _storagePath = Path.Combine(_environment.WebRootPath, subFolder);
    }

    public async Task<string> UploadDocumentAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("File is not selected or is empty.");

        var allowedExtensions = new[] { ".pdf", ".docx", ".xlsx", ".jpg", ".png", ".jpeg" };
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!allowedExtensions.Contains(extension))
            throw new InvalidDataException("Unsupported file type.");

        var fileName = $"{Guid.NewGuid()}{extension}";
        var filePath = Path.Combine(_storagePath, fileName);

        if (!Directory.Exists(_storagePath))
            Directory.CreateDirectory(_storagePath);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Return the relative path
        return filePath.Replace(_environment.WebRootPath, "").Replace("\\", "/");
    }
}
