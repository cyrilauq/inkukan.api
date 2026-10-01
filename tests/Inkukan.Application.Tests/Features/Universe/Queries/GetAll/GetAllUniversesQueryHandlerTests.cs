using AutoMapper;
using FluentAssertions;
using Inkukan.Application.Features.Universe.Queries.GetAll;
using Inkukan.Application.Mappers;
using Inkukan.Domain.Repositories;
using UniverseEntity = Inkukan.Domain.Entities.Universe;
using Microsoft.Extensions.Logging;
using MockQueryable;
using Moq;

namespace Inkukan.Application.Tests.Features.Universe.Queries.GetAll;

[TestClass]
public class GetAllUniversesQueryHandlerTests
{
    private ILoggerFactory _loggerFactory = null!;
    private Mock<IBaseRepository<UniverseEntity>> _universeRepository = null!;
    private IMapper _mapper = null!;
    private GetAllUniversesQueryHandler _handler = null!;

    [TestInitialize]
    public void Setup()
    {
        _loggerFactory = LoggerFactory.Create(cfg => cfg.AddConsole());
        _universeRepository = new Mock<IBaseRepository<UniverseEntity>>();
        _universeRepository.Setup(repository => repository.GetQuery())
            .Returns(new List<UniverseEntity>().BuildMock());
        _mapper = new MapperConfiguration(cfg => cfg.AddProfile<UniverseProfile>(), _loggerFactory).CreateMapper();
        _handler = new GetAllUniversesQueryHandler(_universeRepository.Object, _mapper);
    }

    [TestMethod]
    public async Task When_QueryIsValid_Then_ReturnsPaginatedUniverses()
    {
        List<UniverseEntity> universes =
        [
            new() { Name = "Universe One", Code = "universe-one" },
            new() { Name = "Universe Two", Code = "universe-two" },
            new() { Name = "Universe Three", Code = "universe-three" }
        ];
        _universeRepository.Setup(repository => repository.GetQuery())
            .Returns(universes.BuildMock());
        GetAllUniversesQuery query = new() { PageNumber = 1, PageSize = 1 };

        var result = await _handler.Handle(query, CancellationToken.None);

        result.PageNumber.Should().Be(1);
        result.PageSize.Should().Be(1);
        result.TotalCount.Should().Be(3);
        result.Items.Should().ContainSingle()
            .Which.Should().BeEquivalentTo(new { Name = "Universe Two", Code = "universe-two" });
    }
}
