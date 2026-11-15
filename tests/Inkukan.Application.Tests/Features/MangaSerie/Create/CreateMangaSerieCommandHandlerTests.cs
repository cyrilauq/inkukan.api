using AutoMapper;
using FluentAssertions;
using Inkukan.Application.Features.MangaSerie.Command.Create;
using Inkukan.Application.Mappers;
using Inkukan.Domain.Exceptions;
using Inkukan.Domain.Repositories;
using Microsoft.Extensions.Logging;
using MockQueryable;
using Moq;

namespace Inkukan.Application.Tests.Features.MangaSerie.Create;

[TestClass]
public class CreateMangaSerieCommandHandlerTests
{
    private ILoggerFactory _loggerFactory = null!;
    private Mock<IMangaSerieRepository> _mangaSerieRepoMock = null!;
    private Mock<IBaseRepository<Domain.Entities.Universe>> _universeRepoMock = null!;
    private IMapper _mapper = null!;
    private CreateMangaSerieValidator _validator = null!;
    private CreateMangaSerieCommandHandler _handler = null!;

    [TestInitialize]
    public void Setup()
    {
        _loggerFactory = LoggerFactory.Create(cfg => cfg.AddConsole());
        _mangaSerieRepoMock = new Mock<IMangaSerieRepository>();
        _universeRepoMock = new Mock<IBaseRepository<Domain.Entities.Universe>>();
        _universeRepoMock.Setup(repository => repository.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Guid id, CancellationToken _) => new Domain.Entities.Universe { Id = id, Name = "Universe", Code = "universe" });
        _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MangaSerieProfile>(), _loggerFactory)
            .CreateMapper();
        _validator = new CreateMangaSerieValidator();
        _handler = new CreateMangaSerieCommandHandler(_mangaSerieRepoMock.Object, _universeRepoMock.Object, _validator, _mapper);
    }

    [DataRow("")]
    [DataRow(null)]
    [DataTestMethod]
    public async Task When_TitleIsNotValid_Then_ThrowsException(string title)
    {
        // Arrange
        CreateMangaSerieCommand command = new CreateMangaSerieCommand()
        {
            Synopsis = "Test",
            TitleVO = "Test",
            TitleVF = title,
            TotalVolumes = 1,
            UniverseId = Guid.NewGuid(),
        };

        // Act
        Func<Task> result = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await result.Should()
            .ThrowAsync<EntityValidationException>();
    }

    [TestMethod]
    public async Task When_UniverseIdIsMissing_Then_ThrowsValidationException()
    {
        CreateMangaSerieCommand command = new()
        {
            Synopsis = "Test",
            TitleVO = "Test",
            TitleVF = "Test",
            TotalVolumes = 1,
            VOParutionCountry = "Japan"
        };

        Func<Task> result = async () => await _handler.Handle(command, CancellationToken.None);

        await result.Should()
            .ThrowAsync<EntityValidationException>();
    }

    [TestMethod]
    public async Task When_TitleIsAlreadyTaken_Then_ThrowsException()
    {
        // Arrange
        List<Domain.Entities.MangaSerie> mangas =
        [
            new()
            {
                TitleVO = "Test",
                TitleVF = "Test",
            }
        ];
        _mangaSerieRepoMock.Setup(msrm => msrm.GetQuery())
            .Returns(mangas.BuildMock());

        CreateMangaSerieCommand command = new()
        {
            Synopsis = "Test",
            TitleVO = "Test",
            TitleVF = "Test",
            TotalVolumes = 1,
            VOParutionCountry = "Japan",
            UniverseId = Guid.NewGuid()
        };

        // Act
        Func<Task> result = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await result.Should()
            .ThrowAsync<ConflictException>();
    }

    [TestMethod]
    public async Task When_TitleIsNotTaken_Then_CallRepository_CreateAsyncMethod()
    {
        // Arrange
        List<Domain.Entities.MangaSerie> mangas = [];
        _mangaSerieRepoMock.Setup(msrm => msrm.GetQuery())
            .Returns(mangas.BuildMock());

        CreateMangaSerieCommand command = new()
        {
            Synopsis = "Test",
            TitleVO = "Test",
            TitleVF = "Test",
            TotalVolumes = 1,
            VOParutionCountry = "Japan",
            UniverseId = Guid.NewGuid()
        };

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mangaSerieRepoMock.Verify(msrm => msrm.CreateAsync(It.IsAny<Domain.Entities.MangaSerie>(), CancellationToken.None), Times.Once);
    }

    [TestMethod]
    public async Task When_UniverseDoesNotExist_Then_ThrowsEntityNotFoundException()
    {
        // Arrange
        Guid universeId = Guid.NewGuid();
        _mangaSerieRepoMock.Setup(msrm => msrm.GetQuery())
            .Returns(new List<Domain.Entities.MangaSerie>().BuildMock());
        _universeRepoMock.Setup(repository => repository.GetByIdAsync(universeId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Domain.Entities.Universe?)null);

        CreateMangaSerieCommand command = new()
        {
            Synopsis = "Test",
            TitleVO = "Test",
            TitleVF = "Test",
            TotalVolumes = 1,
            VOParutionCountry = "Japan",
            UniverseId = universeId
        };

        // Act
        Func<Task> result = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await result.Should()
            .ThrowAsync<EntityNotFoundException>();
        _mangaSerieRepoMock.Verify(msrm => msrm.CreateAsync(It.IsAny<Domain.Entities.MangaSerie>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
