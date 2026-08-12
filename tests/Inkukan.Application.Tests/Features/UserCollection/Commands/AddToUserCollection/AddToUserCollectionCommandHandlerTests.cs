using AutoMapper;
using AwesomeAssertions;
using FakeItEasy;
using Inkukan.Application.Dtos.User;
using Inkukan.Application.Features.UserCollection.Commands.AddToUserCollection;
using Inkukan.Application.Mappers;
using Inkukan.Domain.Entities;
using Inkukan.Domain.Exceptions;
using Inkukan.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Inkukan.Application.Tests.Features.UserCollection.Commands.AddToUserCollection;

[TestClass]
public class AddToUserCollectionCommandHandlerTests
{
    private ILoggerFactory _loggerFactory = null!;
    private IBaseRepository<UserListItem> _userListRepoMock = null!;
    private IBaseRepository<User> _userRepoMock = null!;
    private IBaseRepository<SerieVolume> _serieVolumeRepoMock = null!;
    private IMapper _mapper = null!;
    private AddToUserCollectionCommandValidator _validator = null!;
    private AddToUserCollectionCommandHandler _handler = null!;

    [TestInitialize]
    public void Setup()
    {
        _loggerFactory = LoggerFactory.Create(cfg => cfg.AddConsole());
        _userListRepoMock = A.Fake<IBaseRepository < UserListItem>>();
        _userRepoMock = A.Fake<IBaseRepository<User>>();
        _serieVolumeRepoMock = A.Fake<IBaseRepository<SerieVolume>>();
        _mapper = new MapperConfiguration(cfg => cfg.AddProfile<UserListItemMapper>(), _loggerFactory)
            .CreateMapper();
        _validator = new AddToUserCollectionCommandValidator();
        _handler = new AddToUserCollectionCommandHandler(_userListRepoMock, _userRepoMock, _serieVolumeRepoMock, _validator, _mapper);
    }

    [TestMethod]
    public async Task When_GuidDoesNotCorrespondToExistingUser_Then_ThrowException()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        A.CallTo(() => _userRepoMock.GetByIdAsync(userId, CancellationToken.None))
            .Returns((User?)null);

        // Act
        Func<Task<UserListItemDto>> action = async () => await _handler.Handle(new() { UserId = userId, ListType = UserListType.Collection, SerieVolumeId = Guid.NewGuid() }, CancellationToken.None);

        // Assert
        await action
            .Should()
            .ThrowAsync<EntityNotFoundException>();
    }

    [TestMethod]
    public async Task When_GuidDoesNotCorrespondToExistingVolume_Then_ThrowException()
    {
        // Arrange
        Guid volumeId = Guid.NewGuid();
        A.CallTo(() => _serieVolumeRepoMock.GetByIdAsync(volumeId, CancellationToken.None))
            .Returns((SerieVolume?)null);

        // Act
        Func<Task<UserListItemDto>> action = async () => await _handler.Handle(new() { UserId = Guid.NewGuid(), ListType = UserListType.Collection, SerieVolumeId = volumeId }, CancellationToken.None);

        // Assert
        await action
            .Should()
            .ThrowAsync<EntityNotFoundException>();
    }

    [TestMethod]
    [DynamicData(nameof(Get_When_CommandIsNotValid_Then_ThrowsException_Data), DynamicDataSourceType.Method)]
    public async Task When_CommandIsNotValid_Then_ThrowsException(AddToUserCollectionCommand command)
    {
        // Act
        Func<Task<UserListItemDto>> action = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await action
            .Should()
            .ThrowAsync<EntityValidationException>();
    }

    public static IEnumerable<object[]> Get_When_CommandIsNotValid_Then_ThrowsException_Data()
    {
        yield return [ new AddToUserCollectionCommand() { UserId = Guid.Empty, ListType = UserListType.Collection, SerieVolumeId = Guid.NewGuid() } ];
        yield return [ new AddToUserCollectionCommand() { UserId = Guid.NewGuid(), ListType = UserListType.Collection, SerieVolumeId = Guid.Empty } ];
    }
}
