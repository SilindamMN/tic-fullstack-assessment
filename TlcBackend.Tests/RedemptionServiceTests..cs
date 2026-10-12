
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TlcBackend.Controllers;
using TlcBackend.Exceptions;
using TlcBackend.Interface;
using TlcBackend.Tests.Mock;
using Xunit;

namespace TlcBackend.Tests;

public class RedemptionServiceTests
{
    private readonly Mock<IRedemptionService> _mockService;
    private readonly RedemptionsController _controller;

    public RedemptionServiceTests()
    {
        _mockService = new Mock<IRedemptionService>();

        _controller = new RedemptionsController(
            _mockService.Object,
            new Mock<ILogger<RedemptionsController>>().Object);
    }

    [Fact]
    public async Task Create_ValidRequest_Returns201()
    {
        // Arrange
        _mockService
            .Setup(s => s.CreateAsync(MockData.ValidRequest))
            .ReturnsAsync(MockData.SuccessfulRedemption);

        // Act
        var result = await _controller.Create(MockData.ValidRequest);

        // Assert
        var response = Assert.IsType<ObjectResult>(result);
        Assert.Equal(201, response.StatusCode);

        _mockService.Verify(
            s => s.CreateAsync(MockData.ValidRequest),
            Times.Once);
    }

    [Fact]
    public async Task Create_InsufficientPoints_Returns400()
    {
        // Arrange
        _mockService
            .Setup(s => s.CreateAsync(MockData.ValidRequest))
            .ThrowsAsync(new RedemptionException(
                "Insufficient points.", 400));

        // Act
        var result = await _controller.Create(MockData.ValidRequest);

        // Assert
        var response = Assert.IsType<ObjectResult>(result);
        Assert.Equal(400, response.StatusCode);
    }

    [Fact]
    public async Task Create_UnexpectedError_Returns500()
    {
        // Arrange
        _mockService
            .Setup(s => s.CreateAsync(MockData.ValidRequest))
            .ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _controller.Create(MockData.ValidRequest);

        // Assert
        var response = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, response.StatusCode);
    }
}
