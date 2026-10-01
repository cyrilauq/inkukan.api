using AutoMapper;
using FluentAssertions;
using Inkukan.Application.Features.Universe.Commands.Update;
using Inkukan.Application.Mappers;
using Inkukan.Domain.Exceptions;
using Inkukan.Domain.Repositories;
using Microsoft.Extensions.Logging;
using MockQueryable;
using Moq;
using UniverseEntity = Inkukan.Domain.Entities.Universe;

namespace Inkukan.Application.Tests.Features.Universe.Update;

[TestClass]
public class UpdateUniverseCommandHandlerTests
{
    private ILoggerFactory _loggerFactory = null!;
    private Mock<IBaseRepository<UniverseEntity>> _universeRepository = null!;
    private IMapper _mapper = null!;
    private UpdateUniverseValidator _validator = null!;
    private UpdateUniverseCommandHandler _handler = null!;
    private List<UniverseEntity> _universes = null!;

    [TestInitialize]
    public void Setup()
    {
        _loggerFactory = LoggerFactory.Create(cfg => cfg.AddConsole());
        _universes = [new UniverseEntity { Id = Guid.NewGuid(), Name = "Original", Code = "original" }];
        _universeRepository = new Mock<IBaseRepository<UniverseEntity>>();
        _universeRepository.Setup(repository => repository.GetQuery())
            .Returns(() => _universes.BuildMock());
        _universeRepository.Setup(repository => repository.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Guid id, CancellationToken _) => _universes.SingleOrDefault(universe => universe.Id == id));
        _universeRepository.Setup(repository => repository.UpdateAsync(It.IsAny<UniverseEntity>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((UniverseEntity universe, CancellationToken _) => universe);
        _mapper = new MapperConfiguration(cfg => cfg.AddProfile<UniverseProfile>(), _loggerFactory).CreateMapper();
        _validator = new UpdateUniverseValidator();
        _handler = new UpdateUniverseCommandHandler(_universeRepository.Object, _validator, _mapper);
    }

    [TestMethod]
    public async Task When_CommandIsValid_Then_UpdatesUniverseAndCode()
    {
        UniverseEntity existingUniverse = _universes[0];
        UpdateUniverseCommand command = new() { Id = existingUniverse.Id, Name = "Updated Universe!" };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Id.Should().Be(existingUniverse.Id);
        result.Name.Should().Be("Updated Universe!");
        result.Code.Should().Be("updated_universe");
        existingUniverse.Code.Should().Be("updated_universe");
        _universeRepository.Verify(repository => repository.UpdateAsync(existingUniverse, CancellationToken.None), Times.Once);
    }

    [TestMethod]
    public async Task When_IdIsEmpty_Then_ThrowsValidationException()
    {
        UpdateUniverseCommand command = new() { Name = "Updated Universe" };

        Func<Task> result = async () => await _handler.Handle(command, CancellationToken.None);

        await result.Should().ThrowAsync<EntityValidationException>();
    }

    [TestMethod]
    public async Task When_NameAlreadyExists_Then_ThrowsConflictException()
    {
        UniverseEntity existingUniverse = _universes[0];
        _universes.Add(new UniverseEntity { Id = Guid.NewGuid(), Name = "Taken", Code = "taken" });
        UpdateUniverseCommand command = new() { Id = existingUniverse.Id, Name = "TAKEN" };

        Func<Task> result = async () => await _handler.Handle(command, CancellationToken.None);

        await result.Should().ThrowAsync<ConflictException>();
    }

    [TestMethod]
    public async Task When_UniverseDoesNotExist_Then_ThrowsValidationException()
    {
        UpdateUniverseCommand command = new() { Id = Guid.NewGuid(), Name = "Updated Universe" };

        Func<Task> result = async () => await _handler.Handle(command, CancellationToken.None);

        await result.Should().ThrowAsync<EntityValidationException>();
    }
}
