using AutoMapper;
using FluentValidation;
using Inkukan.Application.Dtos;
using Inkukan.Application.Features.Abstractions;
using Inkukan.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Inkukan.Application.Features.MangaPeople.Commands.Update
{
    public class UpdateMangaPeopleCommandHandler(IMangaPeopleRepository mangaPeopleRepository, IValidator<UpdateMangaPeopleCommand> validator, IMapper mapper) 
        : BaseUpdateCommandHandler<UpdateMangaPeopleCommand, MangaPeopleDto, Domain.Entities.MangaPeople>(mangaPeopleRepository, validator, mapper)
    {
        public override async Task<bool> AlreadyExistsAsync(UpdateMangaPeopleCommand request, CancellationToken cancellationToken = default)
        {
            Domain.Entities.MangaPeople? mangaPeople = await mangaPeopleRepository.GetQuery()
                .Where(mp =>
                    mp.Firstname.ToLower().Equals(request.Firstname.ToLower()) &&
                    mp.Lastname.ToLower().Equals(request.Lastname.ToLower()) &&
                    mp.Id != request.Id
                )
                .FirstOrDefaultAsync(cancellationToken);
            return mangaPeople != null;
        }

        public override Task<Domain.Entities.MangaPeople?> GetByIdAsync(UpdateMangaPeopleCommand request, CancellationToken cancellationToken = default)
            => mangaPeopleRepository.GetByIdAsync(request.Id, cancellationToken);
    }
}
