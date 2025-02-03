namespace RealEstateListingApi.Core.Commands;

public record CreateListingCommand
{
    public string Title { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
}