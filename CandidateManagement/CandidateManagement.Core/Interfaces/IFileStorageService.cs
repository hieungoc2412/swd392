namespace CandidateManagement.Core.Interfaces;

// ------------------------------------------------------------
// IFileStorageService: Trừu tượng hoá lưu trữ file
// ------------------------------------------------------------
public interface IFileStorageService
{
    Task<string?> SaveFileAsync(Stream fileStream, string fileName, string folderName);
    Task DeleteFileAsync(string? filePath);
}

