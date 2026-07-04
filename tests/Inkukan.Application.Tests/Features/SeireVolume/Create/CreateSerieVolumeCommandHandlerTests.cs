using AutoMapper;
using FluentAssertions;
using Inkukan.Application.Dtos;
using Inkukan.Application.Features.SerieVolume.Commands.Create;
using Inkukan.Application.Mappers;
using Inkukan.Application.Services;
using Inkukan.Application.Services.Implementations;
using Inkukan.Domain.Entities;
using Inkukan.Domain.Exceptions;
using Inkukan.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace Inkukan.Application.Tests.Features.SeireVolume.Create
{
    [TestClass]
    public class CreateSerieVolumeCommandHandlerTests
    {
        private Mock<IHashService> _hashServiceMock = null!;
        private Mock<ISerieVolumeRepository> _repositoryMock = null!;
        private Mock<IFileUploader> _fileUploaderMock = null!;
        private IMapper _mapper = null!;
        private CreateSerieVolumeCommandValidator _validator = null!;
        private CreateSerieVolumeCommandHandler _handler = null!;

        [TestInitialize]
        public void SetUp()
        {
            _hashServiceMock = new();
            _repositoryMock = new Mock<ISerieVolumeRepository>();
            _fileUploaderMock = new Mock<IFileUploader>();

            _hashServiceMock.Setup(hsm => hsm.HashBytesAsync(It.IsAny<byte[]>()))
                .ReturnsAsync("");

            ServiceCollection services = new();

            services.AddTransient<SetImageDtoAction>();
            services.AddSingleton(new VercelBlobOptions() { BlobUrl = "", Token = "" });

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            MapperConfiguration configuration = new(cfg =>
            {
                cfg.ConstructServicesUsing(serviceProvider.GetService);
                cfg.AddProfile<SerieVolumeProfile>();
            }, LoggerFactory.Create(cfg => cfg.AddConsole()));
            _mapper = configuration.CreateMapper();

            _validator = new CreateSerieVolumeCommandValidator();

            _handler = new CreateSerieVolumeCommandHandler(
                _repositoryMock.Object,
                _fileUploaderMock.Object,
                _hashServiceMock.Object,
                _validator,
                _mapper);
        }

        [TestMethod]
        public async Task When_VolumeAlreadyExists_Then_ThrowConflictException()
        {
            // Arrange
            CreateSerieVolumeCommand command = CreateValidCommand();
            _repositoryMock.Setup(r => r.GetBySerieIdAndVolumeNumberAsync(command.MangaSerieId, command.VolumeNumber, CancellationToken.None))
                .ReturnsAsync(new SerieVolume());

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ConflictException>();
        }

        [TestMethod]
        [DynamicData(nameof(GetInvalidSynopsis), DynamicDataSourceType.Method)]
        public async Task When_SynopsisIsInvalid_Then_ThrowEntityValidationException(string synopsis)
        {
            // Arrange
            CreateSerieVolumeCommand command = CreateValidCommand();
            command.Synopsis = synopsis;

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<EntityValidationException>();
        }

        private static IEnumerable<object?[]> GetInvalidSynopsis()
        {
            yield return [null];
            yield return [""];
            yield return ["Too short"]; // < 15 chars
            yield return [new string('a', 256)]; // > 255 chars
        }

        [TestMethod]
        public async Task When_CommandIsValid_Then_CreateAndReturnDto()
        {
            // Arrange
            CreateSerieVolumeCommand command = CreateValidCommand();
            SerieVolume entity = new() { Id = Guid.NewGuid() };

            _repositoryMock.Setup(r => r.GetBySerieIdAndVolumeNumberAsync(It.IsAny<Guid>(), It.IsAny<int>(), CancellationToken.None))
                .ReturnsAsync((SerieVolume?)null);

            _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<SerieVolume>(), CancellationToken.None))
                .ReturnsAsync(entity);

            // Act
            SerieVolumeDto result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<SerieVolume>(), CancellationToken.None), Times.Once);
        }

        [TestMethod]
        public async Task When_ImagesProvided_Then_CallFileUploader()
        {
            // Arrange
            CreateSerieVolumeCommand command = CreateValidCommand();
            using var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write("content");
            stream.Flush();
            stream.Position = 0;
            command.VFCover = new FormFile(stream, 0, stream.Length, "", "test.png");

            _repositoryMock.Setup(r => r.GetBySerieIdAndVolumeNumberAsync(It.IsAny<Guid>(), It.IsAny<int>(), CancellationToken.None))
                .ReturnsAsync((SerieVolume?)null);

            _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<SerieVolume>(), CancellationToken.None))
                .ReturnsAsync(new SerieVolume());

            _fileUploaderMock.Setup(f => f.UploadAsync(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<string>(), CancellationToken.None, It.IsAny<SupportedFileType[]>()))
                .ReturnsAsync(Guid.NewGuid());

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _fileUploaderMock.Verify(f => f.UploadAsync(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<string>(), CancellationToken.None, It.IsAny<SupportedFileType[]>()), Times.AtLeastOnce);
        }

        // Helper pour créer une commande valide par défaut
        private static CreateSerieVolumeCommand CreateValidCommand()
        {
            return new CreateSerieVolumeCommand
            {
                VolumeNumber = 1,
                Synopsis = "Ceci est un synopsis de plus de quinze caractères pour passer la validation.",
                VOParutionDate = DateTime.Now,
                VFParutionDate = DateTime.Now,
                MangaSerieId = Guid.NewGuid(),
                EANCode = "123456789",
                PriceCode = "FR01"
            };
        }
    }
}