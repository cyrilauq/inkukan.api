using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Inkukan.Application.Features.MangaPeople.Commands.Create;
using Inkukan.Application.Mappers;
using Inkukan.Domain.Exceptions;
using Inkukan.Domain.Repositories;
using Microsoft.Extensions.Logging;
using MockQueryable;
using Moq;

namespace Inkukan.Application.Tests.Features.MangaPeople.Create
{
    [TestClass]
    public class CreateMangaPeopleCommandHandlerTests
    {
        private ILoggerFactory _loggerFactory;
        private Mock<IMangaPeopleRepository> _mangaPeopleRepoMock;
        private IMapper _mapper;
        private IValidator<CreateMangaPeopleCommand> _validator;
        private CreateMangaPeopleCommandHandler _handler;

        [TestInitialize]
        public void Setup()
        {
            _loggerFactory = LoggerFactory.Create(cfg => cfg.AddConsole());
            _mangaPeopleRepoMock = new Mock<IMangaPeopleRepository>();
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MangaPeopleProfile>(), _loggerFactory)
                .CreateMapper();
            _validator = new CreateMangaPeopleValidator();
            _handler = new CreateMangaPeopleCommandHandler(_mangaPeopleRepoMock.Object, _validator, _mapper);
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow("     ")]
        [DataRow("\t\n")]
        [DataRow(null)]
        public async Task When_FirstNameIsNullOrEmpty_Then_ThrowsException(string firstname)
        {
            // Arrange
            CreateMangaPeopleCommand command = new()
            {
                Firstname = firstname,
                Lastname = "lastname"
            };

            // Act
            Func<Task> result = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await result.Should()
                .ThrowAsync<EntityValidationException>();
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow("     ")]
        [DataRow("\t\n")]
        [DataRow(null)]
        public async Task When_LastNameIsNullOrEmpty_Then_ThrowsException(string lastname)
        {
            // Arrange
            CreateMangaPeopleCommand command = new()
            {
                Lastname = lastname,
                Firstname = "Firstname"
            };

            // Act
            Func<Task> result = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await result.Should()
                .ThrowAsync<EntityValidationException>();
        }

        [TestMethod]
        public async Task When_AuthorWithNameAndFirstnameAlreadyExists_Then_ThrowsException()
        {
            // Arrange
            List<Domain.Entities.MangaPeople> mangaPeople =
            [
                new()
                {
                Lastname = "Lastname",
                Firstname = "Firstname"
                }
            ];
            _mangaPeopleRepoMock.Setup(msrm => msrm.GetQuery())
                .Returns(mangaPeople.BuildMock());
            CreateMangaPeopleCommand command = new()
            {
                Lastname = "Lastname",
                Firstname = "Firstname"
            };

            // Act
            Func<Task> result = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await result.Should()
                .ThrowAsync<ConflictException>();
        }

        [TestMethod]
        public async Task When_AddValidAuthor_Then_CallCreateAsyncMethod()
        {
            // Arrange
            List<Domain.Entities.MangaPeople> mangaPeople = [];
            _mangaPeopleRepoMock.Setup(msrm => msrm.GetQuery())
                .Returns(mangaPeople.BuildMock());
            CreateMangaPeopleCommand command = new()
            {
                Lastname = "Lastname",
                Firstname = "Firstname"
            };

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mangaPeopleRepoMock.Verify(msrm => msrm.CreateAsync(It.IsAny<Domain.Entities.MangaPeople>(), CancellationToken.None), Times.Once);
        }
    }
}
