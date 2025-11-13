using CandidateManagement.Business.Interfaces;
using CandidateManagement.Business.Services;
using CandidateManagement.Core.Interfaces;
using CandidateManagement.Core.Services;
using CandidateManagement.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CandidateManagement.Core;

// ------------------------------------------------------------
// DependencyInjection: Đăng ký DI cho toàn hệ thống
// ------------------------------------------------------------
public static class DependencyInjection
{
    public static IServiceCollection AddCandidateManagement(this IServiceCollection services)
    {
        // Layer Data
        services.AddScoped<ICandidateRepository, CandidateRepository>();

        // Layer Core
        services.AddScoped<IFileStorageService, FileStorageService>();

        // Layer Business
        services.AddScoped<ICandidateService, CandidateService>();

        return services;
    }
}

