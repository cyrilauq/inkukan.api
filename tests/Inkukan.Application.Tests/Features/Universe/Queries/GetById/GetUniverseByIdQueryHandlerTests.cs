using AutoMapper;
using FluentAssertions;
using Inkukan.Application.Features.Universe.Queries.GetById;
using Inkukan.Application.Mappers;
using Inkukan.Domain.Exceptions;
using Inkukan.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using UniverseEntity = Inkukan.Domain.Entities.Universe;

namespace Inkukan.Application.Tests.Features.Universe.Queries.GetById;

[TestClass]
public class GetUniverseByIdQueryHandlerTests
{
    private ILoggerFactory _loggerFactory = null!;
    private Mock<IBaseRepository<UniverseEntity>> _universeRepository = null!;
    private GetUniverseByIdQueryHandler _handler = null!;

    [TestInitialize]
    public void Setup()
    {
        _loggerFactory = LoggerFactory.Create(cfg => cfg.AddConsole());
        _universeRepository = new Mock<IBaseRepository<UniverseEntity>>();
        var mapper = new MapperConfiguration(cfg => cfg.AddProfile<UniverseProfile>(), _loggerFactory).CreateMapper();
        _handler = new GetUniverseByIdQueryHandler(_universeRepository.Object, mapper);
    }

    [TestMethod]
    public async Task When_UniverseExists_Then_ReturnsMappedDto()
    {
        Guid id = Guid.NewGuid();
        _universeRepository.Setup(repository => repository.GetByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new UniverseEntity { Id = id, Name = "Multiverse", Code = "multiverse" });

        var result = await _handler.Handle(new GetUniverseByIdQuery { Id = id }, CancellationToken.None);

        result.Id.Should().Be(id);
        result.Name.Should().Be("Multiverse");
        result.Code.Should().Be("multiverse");
    }

    [TestMethod]
    public async Task When_UniverseDoesNotExist_Then_ThrowsEntityNotFoundException()
    {
        Guid id = Guid.NewGuid();
        _universeRepository.Setup(repository => repository.GetByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((UniverseEntity?)null);

        Func<Task> result = async () => await _handler.Handle(new GetUniverseByIdQuery { Id = id }, CancellationToken.None);

        await result.Should().ThrowAsync<EntityNotFoundException>();
    }
}
