namespace RealEstateListingApi.Core.Commands;

public record DeleteListingCommand
{
    public string Id { get; set; }
}