using CafeConnect.Application.Features.Cafe.Commands;
using CafeConnect.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Moq;

namespace CafeConnect.Tests
{
    public class CreateCafeCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnGuid_WhenCafeIsSuccessfullyCreated()
        {
            // Arrange
            var mockCafeRepository = new Mock<ICafeRepository>();
            var mockImageUploadRepository = new Mock<IImageUploadRepository>();
            var handler = new CreateCafeCommandHandler(mockCafeRepository.Object, mockImageUploadRepository.Object);

            var command = new CreateCafeCommand
            {
                Name = "Test Cafe",
                Description = "A test cafe description",
                Location = "Test Location"
            };

            var generatedCafeId = Guid.NewGuid();
            mockCafeRepository
                .Setup(repo => repo.InsertAsync(It.IsAny<Domain.Entities.Cafe>()))
                .ReturnsAsync(generatedCafeId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(generatedCafeId, result);
            mockCafeRepository.Verify(repo => repo.InsertAsync(It.IsAny<Domain.Entities.Cafe>()), Times.Once);
            mockImageUploadRepository.Verify(repo => repo.UploadFileAsync(It.IsAny<IFormFile>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldUploadLogo_WhenLogoIsProvided()
        {
            // Arrange
            var mockCafeRepository = new Mock<ICafeRepository>();
            var mockImageUploadRepository = new Mock<IImageUploadRepository>();
            var handler = new CreateCafeCommandHandler(mockCafeRepository.Object, mockImageUploadRepository.Object);

            var command = new CreateCafeCommand
            {
                Name = "Test Cafe",
                Description = "A test cafe description",
                Location = "Test Location",
                Logo = Mock.Of<IFormFile>()
            };

            var imageId = Guid.NewGuid();
            var generatedCafeId = Guid.NewGuid();
            mockImageUploadRepository
                .Setup(repo => repo.UploadFileAsync(It.IsAny<IFormFile>()))
                .ReturnsAsync(imageId);

            mockCafeRepository
                .Setup(repo => repo.InsertAsync(It.IsAny<Domain.Entities.Cafe>()))
                .ReturnsAsync(generatedCafeId);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(generatedCafeId, result);
            mockImageUploadRepository.Verify(repo => repo.UploadFileAsync(It.IsAny<IFormFile>()), Times.Once);
            mockCafeRepository.Verify(repo => repo.InsertAsync(It.Is<Domain.Entities.Cafe>(c => c.LogoId == imageId)), Times.Once);
        }
    }
}