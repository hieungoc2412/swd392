using System.Collections.Generic;
using System.IO;
using CandidateManagement.Business.Interfaces;
using CandidateManagement.Core.Interfaces;
using CandidateManagement.Data.Entities;
using CandidateManagement.Data.Repositories;

namespace CandidateManagement.Business.Services;

// ------------------------------------------------------------
// CandidateService: Lớp nghiệp vụ cho Candidate
// ------------------------------------------------------------
public class CandidateService : ICandidateService
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly IFileStorageService _fileStorageService;

    private const string CvFolder = "uploads/cv";

    public CandidateService(
        ICandidateRepository candidateRepository,
        IFileStorageService fileStorageService)
    {
        _candidateRepository = candidateRepository;
        _fileStorageService = fileStorageService;
    }

    public Task<IEnumerable<Candidate>> GetAllAsync() => _candidateRepository.GetAllAsync();

    public Task<Candidate?> GetByIdAsync(int id) => _candidateRepository.GetByIdAsync(id);

    public Task<IEnumerable<Candidate>> GetRecentAsync(int count) => _candidateRepository.GetRecentCandidatesAsync(count);

    public async Task<Candidate> CreateAsync(Candidate candidate, Stream? cvStream, string? fileName)
    {
        if (cvStream != null && !string.IsNullOrWhiteSpace(fileName))
        {
            candidate.CvUrl = await _fileStorageService.SaveFileAsync(cvStream, fileName, CvFolder);
        }

        await _candidateRepository.AddAsync(candidate);
        await _candidateRepository.SaveChangesAsync();
        return candidate;
    }

    public async Task UpdateAsync(Candidate candidate, Stream? cvStream, string? fileName)
    {
        var existing = await _candidateRepository.GetByIdAsync(candidate.CandidateId);
        if (existing == null)
        {
            throw new KeyNotFoundException("Candidate not found");
        }

        if (cvStream != null && !string.IsNullOrWhiteSpace(fileName))
        {
            await _fileStorageService.DeleteFileAsync(existing.CvUrl);
            existing.CvUrl = await _fileStorageService.SaveFileAsync(cvStream, fileName, CvFolder);
        }

        existing.FullName = candidate.FullName;
        existing.Email = candidate.Email;
        existing.Phone = candidate.Phone;
        existing.Skills = candidate.Skills;
        existing.Experience = candidate.Experience;
        existing.Status = candidate.Status;

        _candidateRepository.Update(existing);
        await _candidateRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var candidate = await _candidateRepository.GetByIdAsync(id);
        if (candidate == null)
        {
            throw new KeyNotFoundException("Candidate not found");
        }

        await _fileStorageService.DeleteFileAsync(candidate.CvUrl);

        _candidateRepository.Remove(candidate);
        await _candidateRepository.SaveChangesAsync();
    }
}

