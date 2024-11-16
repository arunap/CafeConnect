using CafeConnect.Application.Features.Cafe.Commands;
using CafeConnect.Domain.Exceptions;
using CafeConnect.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Moq;

namespace CafeConnect.Tests
{
    public class UpdateCafeCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldUpdateCafeDetails_WhenValidRequestIsProvided()
        {
            // Arrange
            var mockCafeRepository = new Mock<ICafeRepository>();
            var mockImageUploadRepository = new Mock<IImageUploadRepository>();
            var handler = new UpdateCafeCommandHandler(mockCafeRepository.Object, mockImageUploadRepository.Object);

            var command = new UpdateCafeCommand
            {
                Id = Guid.NewGuid(),
                Name = "Updated Cafe",
                Description = "Updated Description",
                Location = "Updated Location"
            };

            var existingCafe = new Domain.Entities.Cafe
            {
                Id = command.Id,
                Name = "Old Cafe",
                Description = "Old Description",
                Location = "Old Location"
            };

            mockCafeRepository
                .Setup(repo => repo.GetByIdAsync(command.Id))
                .ReturnsAsync(existingCafe);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockCafeRepository.Verify(repo => repo.GetByIdAsync(command.Id), Times.Once);
            mockCafeRepository.Verify(repo => repo.UpdateAsync(It.Is<Domain.Entities.Cafe>(c =>
                c.Id == command.Id &&
                c.Name == command.Name &&
                c.Description == command.Description &&
                c.Location == command.Location)), Times.Once);
            mockImageUploadRepository.Verify(repo => repo.UploadFileAsync(It.IsAny<IFormFile>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldThrowItemNotFoundException_WhenCafeDoesNotExist()
        {
            // Arrange
            var mockCafeRepository = new Mock<ICafeRepository>();
            var mockImageUploadRepository = new Mock<IImageUploadRepository>();
            var handler = new UpdateCafeCommandHandler(mockCafeRepository.Object, mockImageUploadRepository.Object);

            var command = new UpdateCafeCommand
            {
                Id = Guid.NewGuid(),
                Name = "Non-existent Cafe"
            };

            mockCafeRepository
                .Setup(repo => repo.GetByIdAsync(command.Id))
                .ReturnsAsync((Domain.Entities.Cafe)null);

            // Act & Assert
            await Assert.ThrowsAsync<ItemNotFoundException>(() => handler.Handle(command, CancellationToken.None));

            mockCafeRepository.Verify(repo => repo.GetByIdAsync(command.Id), Times.Once);
            mockCafeRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Entities.Cafe>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldUploadNewLogo_WhenLogoIsDifferent()
        {
            // Arrange
            var mockCafeRepository = new Mock<ICafeRepository>();
            var mockImageUploadRepository = new Mock<IImageUploadRepository>();
            var handler = new UpdateCafeCommandHandler(mockCafeRepository.Object, mockImageUploadRepository.Object);

            var command = new UpdateCafeCommand
            {
                Id = Guid.NewGuid(),
                Name = "Updated Cafe",
                Description = "Updated Description",
                Location = "Updated Location",
                Logo = Mock.Of<IFormFile>(f => f.FileName == "new_logo.png")
            };

            var existingCafe = new Domain.Entities.Cafe
            {
                Id = command.Id,
                Name = "Old Cafe",
                Description = "Old Description",
                Location = "Old Location",
                LogoId = Guid.NewGuid()
            };

            var existingLogo = Mock.Of<Domain.Entities.FileInfo>(img => img.FileName == "old_logo.png");

            var newLogoId = Guid.NewGuid();

            mockCafeRepository
                .Setup(repo => repo.GetByIdAsync(command.Id))
                .ReturnsAsync(existingCafe);

            mockImageUploadRepository
                .Setup(repo => repo.GetByIdAsync(existingCafe.LogoId.Value))
                .ReturnsAsync(existingLogo);

            mockImageUploadRepository
                .Setup(repo => repo.UploadFileAsync(It.IsAny<IFormFile>()))
                .ReturnsAsync(newLogoId);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockImageUploadRepository.Verify(repo => repo.UploadFileAsync(command.Logo), Times.Once);
            mockCafeRepository.Verify(repo => repo.UpdateAsync(It.Is<Domain.Entities.Cafe>(c =>
                c.LogoId == newLogoId &&
                c.Name == command.Name &&
                c.Description == command.Description &&
                c.Location == command.Location)), Times.Once);
        }
    }
}