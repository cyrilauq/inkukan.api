using AutoMapper;
using Inkukan.Application.Dtos;
using Inkukan.Application.Features.UserCollection.Queries.GetUserCollectionVolumesByType;
using Inkukan.Application.Mediator.Abstractions;
using Inkukan.Domain.Entities;
using Inkukan.Domain.Exceptions;
using Inkukan.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Inkukan.Application.Features.UserCollection.Queries.GetUserCollectionSeriesByType;

public class GetUserCollectionSeriesByTypeQueryHandler(IUserRepository userRepository, IMangaSerieRepository mangaSerieRepository, ISerieVolumeRepository serieVolumeRepository, IMapper mapper, ILogger<GetUserCollectionVolumesByTypeQueryHandler> logger)
    : IRequestHandler<GetUserCollectionSeriesByTypeQuery, PaginatedDto<SerieListDto>>
{
    public async Task<PaginatedDto<SerieListDto>> Handle(GetUserCollectionSeriesByTypeQuery request, CancellationToken cancellationToken)
    {
        User user = await userRepository.GetByIdAsync(request.UserId, cancellationToken)
            ?? throw new EntityNotFoundException($"No user found for the id: [{request.UserId}]");
        
        IQueryable<Domain.Entities.SerieVolume> volumeQuery = (await serieVolumeRepository.GetByUserAndListAsync(request.UserId, request.CollectionName, cancellationToken));
        var query = volumeQuery
            .GroupBy(v => v.MangaSerieId)
            .Select(g => new
            {
                SerieId = g.Key,
                TitleVF = g.Min(v => v.MangaSerie.TitleVF),
                VolumeOwnedCount = g.Count()
            })
            .OrderBy(g => g.TitleVF);

        int listSize = await query.CountAsync(cancellationToken);

        var seriesId = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        List<SerieListDto> series = [];
        foreach (var s in seriesId)
        {
            SerieListDto serie = mapper.Map<SerieListDto>(await mangaSerieRepository.GetQuery()
                .Include(ms => ms.Volumes)
                .FirstOrDefaultAsync(ms => ms.Id == s.SerieId, cancellationToken));

            IQueryable<Domain.Entities.SerieVolume> serieVolumeQuery = (await serieVolumeRepository.GetBySerieIdAsync(s.SerieId, cancellationToken))
                .OrderBy(v => v.VolumeNumber);

            int serieVolumeCount = await serieVolumeQuery
                .CountAsync(cancellationToken);

            serie.TotalVolumeCount = serieVolumeCount;
            serie.VolumeOwnedCount = s.VolumeOwnedCount;

            series.Add(serie);
        }

        return new()
        {
            Items = series,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            TotalCount = await query.CountAsync(cancellationToken)
        };
    }
}