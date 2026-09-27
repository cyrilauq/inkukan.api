using AutoMapper;
using FluentAssertions;
using Inkukan.Application.Features.Universe.Commands.Create;
using Inkukan.Application.Dtos;
using Inkukan.Application.Mappers;
using Inkukan.Domain.Entities;
using Inkukan.Domain.Exceptions;
using Inkukan.Domain.Repositories;
using Microsoft.Extensions.Logging;
using UniverseEntity = Inkukan.Domain.Entities.Universe;
using MockQueryable;
using Moq;

namespace Inkukan.Application.Tests.Features.Universe.Create;

[TestClass]
public class CreateUniverseCommandHandlerTests
{
    private ILoggerFactory _loggerFactory = null!;
    private Mock<IBaseRepository<UniverseEntity>> _universeRepository = null!;
    private IMapper _mapper = null!;
    private CreateUniverseValidator _validator = null!;
    private CreateUniverseCommandHandler _handler = null!;

    [TestInitialize]
    public void Setup()
    {
        _loggerFactory = LoggerFactory.Create(cfg => cfg.AddConsole());
        _universeRepository = new Mock<IBaseRepository<UniverseEntity>>();
        _universeRepository.Setup(repository => repository.GetQuery())
            .Returns(new List<UniverseEntity>().BuildMock());
        _mapper = new MapperConfiguration(cfg => cfg.AddProfile<UniverseProfile>(), _loggerFactory).CreateMapper();
        _validator = new CreateUniverseValidator();
        _handler = new CreateUniverseCommandHandler(_universeRepository.Object, _validator, _mapper);
    }

    [DataRow("")]
    [DataRow(null)]
    [DataTestMethod]
    public async Task When_NameIsNotValid_Then_ThrowsValidationException(string name)
    {
        CreateUniverseCommand command = new() { Name = name, Code = "test" };

        Func<Task> result = async () => await _handler.Handle(command, CancellationToken.None);

        await result.Should().ThrowAsync<EntityValidationException>();
    }

    [DataRow("")]
    [DataRow(null)]
    [DataTestMethod]
    public async Task When_CodeIsNotValid_Then_ThrowsValidationException(string code)
    {
        CreateUniverseCommand command = new() { Name = "Test", Code = code };

        Func<Task> result = async () => await _handler.Handle(command, CancellationToken.None);

        await result.Should().ThrowAsync<EntityValidationException>();
    }

    [TestMethod]
    public async Task When_CodeIsAlreadyTaken_Then_ThrowsConflictException()
    {
        _universeRepository.Setup(repository => repository.GetQuery())
            .Returns(new List<UniverseEntity> { new() { Name = "Existing", Code = "test" } }.BuildMock());
        CreateUniverseCommand command = new() { Name = "New", Code = "TEST" };

        Func<Task> result = async () => await _handler.Handle(command, CancellationToken.None);

        await result.Should().ThrowAsync<ConflictException>();
    }

    [TestMethod]
    public async Task When_CommandIsValid_Then_CreatesUniverse()
    {
        _universeRepository.Setup(repository => repository.GetQuery())
            .Returns(new List<UniverseEntity>().BuildMock());
        _universeRepository.Setup(repository => repository.UpdateAsync(It.IsAny<UniverseEntity>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((UniverseEntity universe, CancellationToken _) => universe);
        CreateUniverseCommand command = new() { Name = "Test Universe", Code = "test_universe" };

        UniverseDto result = await _handler.Handle(command, CancellationToken.None);

        result.Name.Should().Be(command.Name);
        result.Code.Should().Be(command.Code);
        _universeRepository.Verify(repository => repository.UpdateAsync(It.IsAny<UniverseEntity>(), CancellationToken.None), Times.Once);
    }
}
