using Atm.Api.Services;
using Atm.Api.Interfaces;

public static class ServicesCollectionExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IAtmService, AtmService>();
        services.AddSingleton<IBankService, BankService>();
        services.AddSingleton<IAtmEventBroker, AtmEventBroker>();
        services.Decorate<IAtmService, AtmEventService>();
    }
}