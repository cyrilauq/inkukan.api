using AutoMapper;
using FluentAssertions;
using InkShelf.Application.Features.MangaSerie.Create;
using InkShelf.Application.Mappers;
using InkShelf.Domain.Exceptions;
using InkShelf.Domain.Repositories;
using Microsoft.Extensions.Logging;
using MockQueryable;
using Moq;
using Entities = InkShelf.Domain.Entities;

namespace Inshelf.Application.Tests.Features.MangaSerie.Create
{
    [TestClass]
    public class CreateMangaSerieCommandHandlerTests
    {
        private Microsoft.Extensions.Logging.ILoggerFactory _loggerFactory;
        private Mock<IMangaSerieRepository> _mangaSerieRepoMock;
        private IMapper _mapper;
        private CreateMangaSerieValidator _validator;
        private CreateMangaSerieCommandHandler _handler;

        [TestInitialize]
        public void Setup()
        {
            _loggerFactory = LoggerFactory.Create(cfg => cfg.AddConsole());
            _mangaSerieRepoMock = new Mock<IMangaSerieRepository>();
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MangaSerieProfile>(), _loggerFactory)
                .CreateMapper();
            _validator = new CreateMangaSerieValidator();
            _handler = new CreateMangaSerieCommandHandler(_mangaSerieRepoMock.Object, _validator, _mapper);
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
            };

            // Act
            Func<Task> result = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await result.Should()
                .ThrowAsync<EntityValidationException>();
        }

        [TestMethod]
        public async Task When_TitleIsAlreadyTaken_Then_ThrowsException()
        {
            // Arrange
            List<Entities.MangaSerie> mangas =
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
                TotalVolumes = 1
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
            List<Entities.MangaSerie> mangas = [];
            _mangaSerieRepoMock.Setup(msrm => msrm.GetQuery())
                .Returns(mangas.BuildMock());

            CreateMangaSerieCommand command = new()
            {
                Synopsis = "Test",
                TitleVO = "Test",
                TitleVF = "Test",
                TotalVolumes = 1
            };

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mangaSerieRepoMock.Verify(msrm => msrm.CreateAsync(It.IsAny<Entities.MangaSerie>()), Times.Once);
        }
    }
}
