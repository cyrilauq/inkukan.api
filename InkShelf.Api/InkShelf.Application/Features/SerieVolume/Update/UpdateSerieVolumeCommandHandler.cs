using AutoMapper;
using FluentValidation;
using InkShelf.Application.Dtos;
using InkShelf.Application.Features.Abstractions;
using InkShelf.Domain.Repositories;

namespace InkShelf.Application.Features.SerieVolume.Update
{
    public class UpdateSerieVolumeCommandHandler(ISerieVolumeRepository volumeRepostory, IValidator<UpdateSerieVolumeCommand> validator, IMapper mapper) 
        : BaseUpdateCommandHandler<UpdateSerieVolumeCommand, SerieVolumeDto, Domain.Entities.SerieVolume>(volumeRepostory, validator, mapper)
    {
    }
}
