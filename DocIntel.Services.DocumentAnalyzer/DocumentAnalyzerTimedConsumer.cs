using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DocIntel.Core.Authorization;
using DocIntel.Core.Models;
using DocIntel.Core.Repositories;
using DocIntel.Core.Services;
using DocIntel.Core.Settings;
using DocIntel.Core.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DocIntel.Services.DocumentAnalyzer;

public class DocumentAnalyzerTimedConsumer : DynamicContextConsumer, IHostedService, IDisposable
{
    private int executionCount = 0;
    private readonly ILogger<DocumentAnalyzerTimedConsumer> _logger;
    private Timer? _timer = null;
    private IDocumentRepository _documentRepository;
    private DocumentAnalyzerUtility _documentAnalyzerUtility;

    public DocumentAnalyzerTimedConsumer(ILogger<DocumentAnalyzerTimedConsumer> logger,
        IDocumentRepository documentRepository,
        DocumentAnalyzerUtility documentAnalyzerUtility,
        ApplicationSettings appSettings,
        IServiceProvider serviceProvider,
        AppUserClaimsPrincipalFactory userClaimsPrincipalFactory, UserManager<AppUser> userManager)
        : base(appSettings, serviceProvider, userClaimsPrincipalFactory, userManager)
    {
        _logger = logger;
        _documentRepository = documentRepository;
        _documentAnalyzerUtility = documentAnalyzerUtility;
    }

    public async Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service running.");
        var fromMinutes = TimeSpan.FromMinutes(_appSettings.Schedule.AnalyzerFrequencyCheck);
        _timer = new Timer(async _ => await DoWork(_), null, fromMinutes, fromMinutes);
    }

    private async Task DoWork(object? state)
    {
        var count = Interlocked.Increment(ref executionCount);
        _logger.LogInformation(
            "Timed Hosted Service is working. Count: {Count}", count);
        
        
        using var scope = _serviceProvider.CreateScope();
        using var ambientContext = await GetAmbientContext(scope.ServiceProvider);
            
        while (await _documentRepository.GetAllAsync(ambientContext,
                   _ => _.Where(__ => __.Status == DocumentStatus.Submitted)).CountAsync() > 0)
        {
            var submitted = await _documentRepository.GetAllAsync(ambientContext,
                _ => _.Where(__ => __.Status == DocumentStatus.Submitted))
                .OrderByDescending(__ => __.RegistrationDate).FirstAsync();
            if (!await _documentAnalyzerUtility.Analyze(submitted.DocumentId, ambientContext))
            {
                _logger.LogError("Could not analyze document. Skipping forever.");
                submitted.Status = DocumentStatus.Error;
            }
        }
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service is stopping.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}