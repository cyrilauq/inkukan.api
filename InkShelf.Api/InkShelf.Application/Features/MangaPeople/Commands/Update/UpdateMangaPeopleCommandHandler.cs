using AutoMapper;
using FluentValidation;
using InkShelf.Application.Dtos;
using InkShelf.Application.Features.Abstractions;
using InkShelf.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InkShelf.Application.Features.MangaPeople.Commands.Update
{
    public class UpdateMangaPeopleCommandHandler(IMangaPeopleRepository mangaPeopleRepository, IValidator<UpdateMangaPeopleCommand> validator, IMapper mapper) 
        : BaseUpdateCommandHandler<UpdateMangaPeopleCommand, MangaPeopleDto, Domain.Entities.MangaPeople>(mangaPeopleRepository, validator, mapper)
    {
        public override async Task<bool> AlreadyExistsAsync(UpdateMangaPeopleCommand request)
        {
            Domain.Entities.MangaPeople? mangaPeople = await mangaPeopleRepository.GetQuery()
                .Where(mp =>
                    mp.Firstname.ToLower().Equals(request.Firstname.ToLower()) &&
                    mp.Lastname.ToLower().Equals(request.Lastname.ToLower()) &&
                    mp.Id != request.Id
                )
                .FirstOrDefaultAsync();
            return mangaPeople != null;
        }

        public override Task<Domain.Entities.MangaPeople?> GetByIdAsync(UpdateMangaPeopleCommand request)
            => mangaPeopleRepository.GetByIdAsync(request.Id);
    }
}
