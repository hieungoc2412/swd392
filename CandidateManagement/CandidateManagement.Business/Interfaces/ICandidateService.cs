using CandidateManagement.Data.Entities;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CandidateManagement.Business.Interfaces;

// ------------------------------------------------------------
// ICandidateService: Chứa nghiệp vụ cho Candidate
// ------------------------------------------------------------
public interface ICandidateService
{
    Task<IEnumerable<Candidate>> GetAllAsync();
    Task<Candidate?> GetByIdAsync(int id);
    Task<IEnumerable<Candidate>> GetRecentAsync(int count);
    Task<Candidate> CreateAsync(Candidate candidate, Stream? cvStream, string? fileName);
    Task UpdateAsync(Candidate candidate, Stream? cvStream, string? fileName);
    Task DeleteAsync(int id);
}

