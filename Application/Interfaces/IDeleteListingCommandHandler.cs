using RealEstateListingApi.Core.Commands;

namespace RealEstateListingApi.Application.Interfaces;

public interface IDeleteListingCommandHandler
{
    Task Handle(DeleteListingCommand command);
}