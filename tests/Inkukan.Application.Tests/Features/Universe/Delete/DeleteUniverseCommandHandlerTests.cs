using FluentAssertions;
using Inkukan.Application.Features.Universe.Commands.Delete;
using Inkukan.Domain.Exceptions;
using Inkukan.Domain.Repositories;
using MockQueryable;
using Moq;
using MangaSerieEntity = Inkukan.Domain.Entities.MangaSerie;
using UniverseEntity = Inkukan.Domain.Entities.Universe;

namespace Inkukan.Application.Tests.Features.Universe.Delete;

[TestClass]
public class DeleteUniverseCommandHandlerTests
{
    private Mock<IBaseRepository<UniverseEntity>> _universeRepository = null!;
    private Mock<IBaseRepository<MangaSerieEntity>> _mangaSerieRepository = null!;
    private DeleteUniverseCommandHandler _handler = null!;

    [TestInitialize]
    public void Setup()
    {
        _universeRepository = new Mock<IBaseRepository<UniverseEntity>>();
        _mangaSerieRepository = new Mock<IBaseRepository<MangaSerieEntity>>();
        _mangaSerieRepository.Setup(repository => repository.GetQuery())
            .Returns(new List<MangaSerieEntity>().BuildMock());
        _handler = new DeleteUniverseCommandHandler(_universeRepository.Object, _mangaSerieRepository.Object);
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
    public async Task When_UniverseIsLinkedToSeries_Then_DeletesOnlyItsSeries()
    {
        Guid id = Guid.NewGuid();
        UniverseEntity universe = new() { Id = id, Name = "Multiverse", Code = "multiverse" };
        MangaSerieEntity linkedSerie1 = new() { Id = Guid.NewGuid(), UniverseId = id };
        MangaSerieEntity linkedSerie2 = new() { Id = Guid.NewGuid(), UniverseId = id };
        MangaSerieEntity otherSerie = new() { Id = Guid.NewGuid(), UniverseId = Guid.NewGuid() };
        _universeRepository.Setup(repository => repository.GetByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(universe);
        _mangaSerieRepository.Setup(repository => repository.GetQuery())
            .Returns(new List<MangaSerieEntity> { linkedSerie1, linkedSerie2, otherSerie }.BuildMock());

        await _handler.Handle(new DeleteUniverseCommand { Id = id }, CancellationToken.None);

        _mangaSerieRepository.Verify(repository => repository.DeleteAsync(linkedSerie1, It.IsAny<CancellationToken>()), Times.Once);
        _mangaSerieRepository.Verify(repository => repository.DeleteAsync(linkedSerie2, It.IsAny<CancellationToken>()), Times.Once);
        _mangaSerieRepository.Verify(repository => repository.DeleteAsync(otherSerie, It.IsAny<CancellationToken>()), Times.Never);
        _universeRepository.Verify(repository => repository.DeleteAsync(universe, It.IsAny<CancellationToken>()), Times.Once);
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
        _mangaSerieRepository.Verify(repository => repository.DeleteAsync(It.IsAny<MangaSerieEntity>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
