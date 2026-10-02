using FluentAssertions;
using Inkukan.Application.Features.Universe.Commands.Delete;
using Inkukan.Domain.Exceptions;
using Inkukan.Domain.Repositories;
using Moq;
using UniverseEntity = Inkukan.Domain.Entities.Universe;

namespace Inkukan.Application.Tests.Features.Universe.Delete;

[TestClass]
public class DeleteUniverseCommandHandlerTests
{
    private Mock<IBaseRepository<UniverseEntity>> _universeRepository = null!;
    private DeleteUniverseCommandHandler _handler = null!;

    [TestInitialize]
    public void Setup()
    {
        _universeRepository = new Mock<IBaseRepository<UniverseEntity>>();
        _handler = new DeleteUniverseCommandHandler(_universeRepository.Object);
    }

    [TestMethod]
    public async Task When_UniverseExists_Then_DeletesUniverseLogically()
    {
        Guid id = Guid.NewGuid();
        UniverseEntity universe = new() { Id = id, Name = "Multiverse", Code = "multiverse" };
        _universeRepository.Setup(repository => repository.GetByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(universe);
        _universeRepository.Setup(repository => repository.DeleteAsync(universe, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        await _handler.Handle(new DeleteUniverseCommand { Id = id }, CancellationToken.None);

        _universeRepository.Verify(repository => repository.DeleteAsync(universe, CancellationToken.None), Times.Once);
    }

    [TestMethod]
    public async Task When_UniverseDoesNotExist_Then_ThrowsEntityNotFoundException()
    {
        Guid id = Guid.NewGuid();
        _universeRepository.Setup(repository => repository.GetByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((UniverseEntity?)null);

        Func<Task> result = async () => await _handler.Handle(new DeleteUniverseCommand { Id = id }, CancellationToken.None);

        await result.Should().ThrowAsync<EntityNotFoundException>();
        _universeRepository.Verify(repository => repository.DeleteAsync(It.IsAny<UniverseEntity>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
