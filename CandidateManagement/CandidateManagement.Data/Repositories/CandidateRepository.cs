using CandidateManagement.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CandidateManagement.Data.Repositories;

// ------------------------------------------------------------
// CandidateRepository: Repository cho Candidate
// ------------------------------------------------------------
public class CandidateRepository : RepositoryBase<Candidate>, ICandidateRepository
{
    public CandidateRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Candidate>> GetRecentCandidatesAsync(int count)
    {
        return await DbSet.AsNoTracking()
            .OrderByDescending(c => c.CreatedAt)
            .Take(count)
            .ToListAsync();
    }
}

