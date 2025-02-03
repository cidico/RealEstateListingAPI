using RealEstateListingApi.Application.Interfaces;
using RealEstateListingApi.Core.Commands;
using RealEstateListingApi.Core.Interfaces;

namespace RealEstateListingApi.Application.CommandHandlers;

public class DeleteListingCommandHandler : IDeleteListingCommandHandler
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteListingCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteListingCommand command)
    {
        var listing = await _unitOfWork.Listings.GetByIdAsync(command.Id);
        if (listing == null)
        {
            throw new KeyNotFoundException("Listing not found.");
        }

        await _unitOfWork.Listings.DeleteAsync(command.Id);
        await _unitOfWork.SaveChangesAsync();
    }
}