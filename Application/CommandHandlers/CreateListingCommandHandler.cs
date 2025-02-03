using RealEstateListingApi.Application.Interfaces;
using RealEstateListingApi.Core.Commands;
using RealEstateListingApi.Core.Entities;
using RealEstateListingApi.Core.Interfaces;

namespace RealEstateListingApi.Application.CommandHandlers;

public class CreateListingCommandHandler : ICreateListingCommandHandler
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateListingCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Listing> Handle(CreateListingCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.Title))
            throw new ArgumentException("Title is required.");

        if (command.Price <= 0)
            throw new ArgumentException("Price must be greater than zero.");
        
        var listing = new Listing
        {
            Id = Guid.NewGuid().ToString(),
            Title = command.Title,
            Price = command.Price,
            Description = command.Description
        };

        await _unitOfWork.Listings.InsertAsync(listing);
        await _unitOfWork.SaveChangesAsync();

        return listing;
    }
}