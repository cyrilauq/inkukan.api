using AutoMapper;
using FluentAssertions;
using InkShelf.Application.Features.Editor.Create;
using InkShelf.Application.Mappers;
using InkShelf.Domain.Exceptions;
using InkShelf.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace Inshelf.Application.Tests.Features.Editor.Create
{
    [TestClass]
    public class CreateEditorCommandHandlerTests
    {
        private ILoggerFactory _loggerFactory = null!;
        private Mock<IEditorRepository> _editorRepository = null!;
        private IMapper _mapper = null!;
        private CreateEditorValidator _validator = null!;
        private CreateEditorCommandHandler _handler = null!;

        [TestInitialize]
        public void SetUp()
        {
            _loggerFactory = LoggerFactory.Create(cfg => cfg.AddConsole());
            _editorRepository = new Mock<IEditorRepository>();

            _editorRepository.Setup(er => er.GetByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((InkShelf.Domain.Entities.Editor?)null);

            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<EditorProfile>(), _loggerFactory)
                .CreateMapper();
            _validator = new CreateEditorValidator();
            _handler = new CreateEditorCommandHandler(_editorRepository.Object, _validator, _mapper);
        }

        [TestMethod]
        [DynamicData(nameof(DateTimeValues), DynamicDataSourceType.Method)]
        public async Task When_ConstitutionDateIsNotValid_Then_ThrowValidationException(DateTime constitutionDate)
        {
            // Arrange
            CreateEditorCommand command = new()
            {
                ConstitutionDate = constitutionDate,
                ContactMail = string.Empty,
                Country = "Belgium",
                Description = null,
                Name = "Doki Doki",
                Website = null
            };

            // Act
            Func<Task> result = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await result.Should()
                .ThrowAsync<EntityValidationException>();
        }

        private static IEnumerable<object?[]> DateTimeValues()
        {
            yield return [null];
            yield return [DateTime.MinValue];
        }

        [TestMethod]
        [DynamicData(nameof(InvalidNameValues), DynamicDataSourceType.Method)]
        public async Task When_NameIsNotValid_Then_ThrowValidationException(string name)
        {
            // Arrange
            CreateEditorCommand command = new()
            {
                ConstitutionDate = DateTime.Now,
                ContactMail = string.Empty,
                Country = "Belgium",
                Description = null,
                Name = name,
                Website = null
            };

            // Act
            Func<Task> result = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await result.Should()
                .ThrowAsync<EntityValidationException>();
        }

        private static IEnumerable<object?[]> InvalidNameValues()
        {
            yield return [""];
            yield return [null];
            yield return ["\r\n"];
            yield return ["              "];
        }
    }
}
