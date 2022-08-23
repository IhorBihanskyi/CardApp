namespace Atm.Api.Models;

public sealed record CardBrandLimit(
    CardBrands CardBrand,
    decimal Amount);
