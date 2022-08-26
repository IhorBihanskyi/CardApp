using Atm.Api.Services;
using Atm.Api.Interfaces;

public static class ServicesCollectionExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IBankService, BankService>();
        services.AddSingleton<IAtmService, AtmEventService>(sp
                => new AtmEventService(
                        new AtmService(sp.GetRequiredService<IBankService>()),
                        sp.GetRequiredService<IAtmEventBroker>()));
        services.AddSingleton<IAtmEventBroker, AtmEventBroker>();
    }
}   