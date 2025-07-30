using Microsoft.Extensions.DependencyInjection;
using QLDT.Repository;

public class CleanupSerImpl : BackgroundService
{
    private readonly ILogger<CleanupSerImpl> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly TimeSpan _interval = TimeSpan.FromDays(3);

    public CleanupSerImpl(
        ILogger<CleanupSerImpl> logger,
        IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                try
                {
                    var refreshTokenRepo = scope.ServiceProvider.GetRequiredService<RefreshTokenRepo>();
                    var invalidTokenRepo = scope.ServiceProvider.GetRequiredService<InvalidTokenRepo>();

                    var expiredRefreshTokens = await refreshTokenRepo.GetExpiredAsync(stoppingToken);
                    if (expiredRefreshTokens.Any())
                    {
                        await refreshTokenRepo.DeleteAsync(expiredRefreshTokens, stoppingToken);
                        _logger.LogInformation($"[Cleanup] Deleted {expiredRefreshTokens.Count} expired refresh tokens.");
                    }
                    else
                    {
                        _logger.LogInformation("[Cleanup] No expired refresh tokens found.");
                    }

                    var expiredInvalidTokens = await invalidTokenRepo.GetExpiredAsync(stoppingToken);
                    if (expiredInvalidTokens.Any())
                    {
                        await invalidTokenRepo.DeleteAsync(expiredInvalidTokens, stoppingToken);
                        _logger.LogInformation($"[Cleanup] Deleted {expiredInvalidTokens.Count} expired invalid tokens.");
                    }
                    else
                    {
                        _logger.LogInformation("[Cleanup] No expired invalid tokens found.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "[Cleanup] An error occurred during token cleanup.");
                }
            }

            await Task.Delay(_interval, stoppingToken);
        }
    }
}
