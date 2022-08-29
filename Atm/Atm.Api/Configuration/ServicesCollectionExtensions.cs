using Atm.Api.Services;
using Atm.Api.Interfaces;
using Atm.Api.Extentions;

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
        services.AddSingleton<AtmLinkGenerator>();
    }
}   