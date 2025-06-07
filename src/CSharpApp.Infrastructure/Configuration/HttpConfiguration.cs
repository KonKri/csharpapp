using Microsoft.Extensions.Options;
using Polly;

namespace CSharpApp.Infrastructure.Configuration;

public static class HttpConfiguration
{
    public static IServiceCollection AddHttpConfiguration(this IServiceCollection services)
    {
        // get the service provider so that we can get the http client settings via IOptions.
        using var provider = services.BuildServiceProvider();
        var sett = provider.GetRequiredService<IOptions<HttpClientSettings>>();

        services.AddHttpClient<IProductsService, ProductsService>()

            // set the time that a message handler lives.
            .SetHandlerLifetime(TimeSpan.FromSeconds(sett.Value.LifeTime))

            // using Polly, set the number of retries we want, and the seconds that we are going to wait till the next request in case of a falure.
            .AddTransientHttpErrorPolicy(policy => policy
                .WaitAndRetryAsync(sett.Value.RetryCount, retryAttempt => TimeSpan.FromSeconds(sett.Value.SleepDuration)));

        return services;
    }
}