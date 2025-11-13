using System;
using System.IO;
using System.Threading.Tasks;
using CandidateManagement.Core.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace CandidateManagement.Core.Services;

// ------------------------------------------------------------
// FileStorageService: Lưu file vào wwwroot/uploads
// ------------------------------------------------------------
public class FileStorageService : IFileStorageService
{
    private readonly string _rootPath;

    public FileStorageService(IWebHostEnvironment environment)
    {
        _rootPath = environment.WebRootPath;
    }

    public async Task<string?> SaveFileAsync(Stream fileStream, string fileName, string folderName)
    {
        if (fileStream == null || fileStream.Length == 0)
        {
            return null;
        }

        var uploadsFolder = Path.Combine(_rootPath, folderName);
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(fileName)}";
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using var file = new FileStream(filePath, FileMode.Create);
        await fileStream.CopyToAsync(file);

        var relativePath = Path.Combine(folderName, uniqueFileName).Replace("\\", "/");
        return $"/{relativePath}";
    }

    public Task DeleteFileAsync(string? filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            return Task.CompletedTask;
        }

        var physicalPath = Path.Combine(_rootPath, filePath.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));

        if (File.Exists(physicalPath))
        {
            File.Delete(physicalPath);
        }

        return Task.CompletedTask;
    }
}

