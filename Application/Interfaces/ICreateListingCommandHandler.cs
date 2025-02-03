using RealEstateListingApi.Core.Commands;
using RealEstateListingApi.Core.Entities;

namespace RealEstateListingApi.Application.Interfaces;

public interface ICreateListingCommandHandler
{
    Task<Listing> Handle(CreateListingCommand command);
}