using AutoMapper;
using AutoMapper.QueryableExtensions;
using Inkukan.Application.Dtos;
using Inkukan.Application.Features.UserCollection.Queries.GetUserCollectionByName;
using Inkukan.Application.Mediator.Abstractions;
using Inkukan.Domain.Entities;
using Inkukan.Domain.Exceptions;
using Inkukan.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Inkukan.Application.Features.UserCollection.Queries.GetUserCollectionVolumesByType;

public class GetUserCollectionVolumesByTypeQueryHandler(IUserRepository userRepository, ISerieVolumeRepository serieVolumeRepository, IMapper mapper, ILogger<GetUserCollectionVolumesByTypeQueryHandler> logger) 
    : IRequestHandler<GetUserCollectionVolumesByTypeQuery, PaginatedDto<SerieVolumeDto>>
{

    public async Task<PaginatedDto<SerieVolumeDto>> Handle(GetUserCollectionVolumesByTypeQuery request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user == null)
            throw new EntityNotFoundException($"No user found for the id: [{request.UserId}]");
        IQueryable<Domain.Entities.SerieVolume> query = await serieVolumeRepository.GetByUserAndListAsync(request.UserId, request.CollectionName, cancellationToken);

        int listSize = await query.CountAsync(cancellationToken);
        List<SerieVolumeDto> volumes = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ProjectTo<SerieVolumeDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        return new PaginatedDto<SerieVolumeDto>()
        {
            Items = volumes,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = listSize
        };
    }
}
