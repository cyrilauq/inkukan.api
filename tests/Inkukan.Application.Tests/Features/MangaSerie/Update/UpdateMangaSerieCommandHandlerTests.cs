using AutoMapper;
using FluentAssertions;
using Inkukan.Application.Features.MangaSerie.Command.Update;
using Inkukan.Application.Mappers;
using Inkukan.Domain.Exceptions;
using Inkukan.Domain.Repositories;
using Microsoft.Extensions.Logging;
using MockQueryable;
using Moq;
using MangaSerieEntity = Inkukan.Domain.Entities.MangaSerie;
using UniverseEntity = Inkukan.Domain.Entities.Universe;

namespace Inkukan.Application.Tests.Features.MangaSerie.Update;

[TestClass]
public class UpdateMangaSerieCommandHandlerTests
{
    private ILoggerFactory _loggerFactory = null!;
    private Mock<IMangaSerieRepository> _mangaSerieRepoMock = null!;
    private Mock<IBaseRepository<UniverseEntity>> _universeRepoMock = null!;
    private UpdateMangaSerieCommandHandler _handler = null!;
    private MangaSerieEntity _serie = null!;

    [TestInitialize]
    public void Setup()
    {
        _loggerFactory = LoggerFactory.Create(cfg => cfg.AddConsole());
        _serie = new() { Id = Guid.NewGuid(), TitleVF = "Old", TitleVO = "Old", VOParutionCountry = "Japan", Synopsis = "Old", UniverseId = Guid.NewGuid() };

        _mangaSerieRepoMock = new Mock<IMangaSerieRepository>();
        _mangaSerieRepoMock.Setup(repository => repository.GetQuery())
            .Returns(new List<MangaSerieEntity> { _serie }.BuildMock());
        _mangaSerieRepoMock.Setup(repository => repository.UpdateAsync(It.IsAny<MangaSerieEntity>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((MangaSerieEntity entity, CancellationToken _) => entity);

        _universeRepoMock = new Mock<IBaseRepository<UniverseEntity>>();

        IMapper mapper = new MapperConfiguration(cfg => cfg.AddProfile<MangaSerieProfile>(), _loggerFactory)
            .CreateMapper();
        _handler = new UpdateMangaSerieCommandHandler(_mangaSerieRepoMock.Object, _universeRepoMock.Object, new UpdateMangaSerieValidator(), mapper);
    }

    [TestMethod]
    public async Task When_UniverseExists_Then_UpdatesSerie()
    {
        // Arrange
        Guid universeId = Guid.NewGuid();
        _universeRepoMock.Setup(repository => repository.GetByIdAsync(universeId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new UniverseEntity { Id = universeId, Name = "Universe", Code = "universe" });

        // Act
        await _handler.Handle(CreateCommand(universeId), CancellationToken.None);

        // Assert
        _mangaSerieRepoMock.Verify(repository => repository.UpdateAsync(It.Is<MangaSerieEntity>(serie => serie.UniverseId == universeId), It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task When_UniverseDoesNotExist_Then_ThrowsEntityNotFoundException()
    {
        // Arrange
        Guid universeId = Guid.NewGuid();
        _universeRepoMock.Setup(repository => repository.GetByIdAsync(universeId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((UniverseEntity?)null);

        // Act
        Func<Task> result = async () => await _handler.Handle(CreateCommand(universeId), CancellationToken.None);

        // Assert
        await result.Should()
            .ThrowAsync<EntityNotFoundException>();
        _mangaSerieRepoMock.Verify(repository => repository.UpdateAsync(It.IsAny<MangaSerieEntity>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    private UpdateMangaSerieCommand CreateCommand(Guid universeId) => new()
    {
        Id = _serie.Id,
        TitleVF = "Test",
        TitleVO = "Test",
        Synopsis = "Test",
        TotalVolumes = 1,
        VOParutionCountry = "Japan",
        UniverseId = universeId
    };
}
