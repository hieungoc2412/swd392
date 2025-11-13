using CandidateManagement.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CandidateManagement.Data.Repositories;

// ------------------------------------------------------------
// ICandidateRepository: Interface đặc thù cho Candidate
// ------------------------------------------------------------
public interface ICandidateRepository : IRepository<Candidate>
{
    Task<IEnumerable<Candidate>> GetRecentCandidatesAsync(int count);
}

