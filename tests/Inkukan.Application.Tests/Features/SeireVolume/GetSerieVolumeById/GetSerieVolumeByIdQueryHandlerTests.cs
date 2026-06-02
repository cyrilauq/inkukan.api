using AutoMapper;
using AwesomeAssertions;
using FakeItEasy;
using Inkukan.Application.Dtos;
using Inkukan.Application.Features.SerieVolume.Queries.GetSerieVolumeById;
using Inkukan.Application.Mappers;
using Inkukan.Application.Services.Implementations;
using Inkukan.Domain.Entities;
using Inkukan.Domain.Exceptions;
using Inkukan.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Inkukan.Application.Tests.Features.SeireVolume.GetSerieVolumeById
{
    [TestClass]
    public class GetSerieVolumeByIdQueryHandlerTests
    {
        private IMapper _mapper = null!;
        private ISerieVolumeRepository _serieVolumeRepository = null!;
        private Microsoft.Extensions.Logging.ILoggerFactory _loggerFactory = null!;
        private GetSerieVolumeByIdQueryHandler _handler = null!;

        [TestInitialize]
        public async Task SetUp()
        {
            _loggerFactory = LoggerFactory.Create(cfg => cfg.AddConsole());
            ServiceCollection services = new();

            services.AddTransient<SetImageDtoAction>();
            services.AddSingleton(new VercelBlobOptions() { BlobUrl = "", Token = "" });

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            MapperConfiguration configuration = new(cfg =>
            {
                cfg.ConstructServicesUsing(serviceProvider.GetService);
                cfg.AddProfile<SerieVolumeProfile>();
            }, _loggerFactory);
            _mapper = configuration.CreateMapper();

            _serieVolumeRepository = A.Fake<ISerieVolumeRepository>();

            _handler = new(_serieVolumeRepository, _mapper);
        }

        [TestMethod]
        public async Task When_IdNotRelatedToAnySerieVolume_Then_ThrowsExceptions()
        {
            // Arrange
            Guid volumeId = Guid.NewGuid();
            A.CallTo(() => _serieVolumeRepository.GetByIdAsync(volumeId))
                .Returns((SerieVolume?)null);

            // Act
            Func<Task<SerieVolumeDto>> action = async () => await _handler.Handle(new GetSerieVolumeByIdQuery() { Id = volumeId }, default);

            // Assert
            await action
                .Should()
                .ThrowAsync<EntityNotFoundException>();
        }

        [TestMethod]
        public async Task When_NotRelatedToAnySerieVolume_Then_ReturnsDto()
        {
            // Arrange
            Guid volumeId = Guid.NewGuid();
            A.CallTo(() => _serieVolumeRepository.GetByIdAsync(volumeId))
                .Returns(new Domain.Entities.SerieVolume()
                {
                    Id = volumeId,
                    CreatedAt = DateTime.UtcNow,
                    EANCode = "123456789",
                    MangaSerieId = Guid.NewGuid(),
                    PriceCode = "C",
                    RecommendedAge = 13,
                });

            // Act
            SerieVolumeDto result = await _handler.Handle(new GetSerieVolumeByIdQuery() { Id = volumeId }, default);

            // Assert
            result.EANCode
                .Should()
                .Be("123456789");
        }
    }
}
