using FluentAssertions;
using Inkukan.Application.Services;
using Inkukan.Application.Services.Implementations;
using Inkukan.Domain.Exceptions;
using Inkukan.Domain.Repositories;
using Moq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Inkukan.Application.Tests.Services.Implementations
{
    [TestClass]
    public class FileUploaderVercelBlobTests
    {
            private Mock<IFileChecker> _fileCheckerMock = null!;
            private Mock<IBlobStorage> _blobStorageMock = null!;
            private FileUploaderVercelBlob _service = null!;

            [TestInitialize]
            public void SetUp()
            {
                _fileCheckerMock = new Mock<IFileChecker>();
                _blobStorageMock = new Mock<IBlobStorage>();;

                _service = new FileUploaderVercelBlob(
                    _fileCheckerMock.Object,
                    _blobStorageMock.Object);
            }

            [TestMethod]
            public async Task When_FileIsNotSupported_Then_ThrowEntityValidationException()
            {
                // Arrange
                var content = new byte[] { 1, 2, 3 };
                _fileCheckerMock.Setup(f => f.FileIsSupportedType(It.IsAny<string>(), content, CancellationToken.None, It.IsAny<SupportedFileType[]>()))
                    .ReturnsAsync(false);

                // Act
                Func<Task> act = async () => await _service.UploadAsync("test.txt", content, "", CancellationToken.None, SupportedFileType.PNG);

                // Assert
                await act.Should().ThrowAsync<EntityValidationException>()
                    .WithMessage("*validating the file*");
            }

            [TestMethod]
            public async Task When_FileIsValidImage_Then_UploadResizedVersionsAndOriginal()
            {
                // Arrange
                var fileName = "test.png";
                var imageContent = CreateValidImageBytes();

                _fileCheckerMock.Setup(f => f.FileIsSupportedType(It.IsAny<string>(), It.IsAny<byte[]>(), CancellationToken.None, It.IsAny<SupportedFileType[]>()))
                    .ReturnsAsync(true);

                // Act
                var result = await _service.UploadAsync(fileName, imageContent, "", CancellationToken.None, SupportedFileType.PNG);

                // Assert
                result.Should().NotBeNull();
                result.Should().NotBe(Guid.Empty);

                _blobStorageMock.Verify(b => b.UploadAsync(It.IsAny<byte[]>(), It.Is<string>(s => s.StartsWith("small/")), CancellationToken.None), Times.Once);
                _blobStorageMock.Verify(b => b.UploadAsync(It.IsAny<byte[]>(), It.Is<string>(s => s.StartsWith("medium/")), CancellationToken.None), Times.Once);
                _blobStorageMock.Verify(b => b.UploadAsync(It.IsAny<byte[]>(), It.Is<string>(s => s.StartsWith("large/")), CancellationToken.None), Times.Once);
                _blobStorageMock.Verify(b => b.UploadAsync(It.IsAny<byte[]>(), It.Is<string>(s => s.StartsWith("original/")), CancellationToken.None), Times.Once);
            }

            /// <summary>
            /// Helper pour générer un byte[] d'une image valide (1x1 pixel) 
            /// pour que ImageSharp ne crash pas au chargement.
            /// </summary>
            private static byte[] CreateValidImageBytes()
            {
            using var image = new Image<Rgba32>(1, 1);
            using var ms = new MemoryStream();
            image.SaveAsPng(ms);
            return ms.ToArray();
        }
        }
}
