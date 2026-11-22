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
    private Mock<IUniverseRepository> _universeRepository = null!;
    private DeleteUniverseCommandHandler _handler = null!;

    public TestContext TestContext { get; set; } = null!;

    [TestInitialize]
    public void Setup()
    {
        _universeRepository = new Mock<IUniverseRepository>();
        _handler = new DeleteUniverseCommandHandler(_universeRepository.Object);
    }

    [TestMethod]
    public async Task When_UniverseExists_Then_DeletesUniverseWithItsSeries()
    {
        Guid id = Guid.NewGuid();
        UniverseEntity universe = new() { Id = id, Name = "Multiverse", Code = "multiverse" };
        _universeRepository.Setup(repository => repository.GetByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(universe);

        await _handler.Handle(new DeleteUniverseCommand { Id = id }, TestContext.CancellationTokenSource.Token);

        _universeRepository.Verify(repository => repository.DeleteWithSeriesAsync(universe, TestContext.CancellationTokenSource.Token), Times.Once);
        _universeRepository.Verify(repository => repository.DeleteAsync(It.IsAny<UniverseEntity>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [TestMethod]
    public async Task When_UniverseDoesNotExist_Then_ThrowsEntityNotFoundException()
    {
        Guid id = Guid.NewGuid();
        _universeRepository.Setup(repository => repository.GetByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((UniverseEntity?)null);

        Func<Task> result = async () => await _handler.Handle(new DeleteUniverseCommand { Id = id }, TestContext.CancellationTokenSource.Token);

        await result.Should().ThrowAsync<EntityNotFoundException>();
        _universeRepository.Verify(repository => repository.DeleteWithSeriesAsync(It.IsAny<UniverseEntity>(), It.IsAny<CancellationToken>()), Times.Never);
        _universeRepository.Verify(repository => repository.DeleteAsync(It.IsAny<UniverseEntity>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
