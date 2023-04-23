using ToDo.App.Core.Domain;
using ToDo.App.Infrastructure.Services;
using ToDo.Client;
using ToDo.Client.Contracts.Dtos;
using ToDo.Client.Contracts.Requests;
using ToDo.Client.Contracts.Responses;

namespace ToDo.App.Infrastructure.UnitTests;

public sealed class ToDoServiceTests
{
    [Fact]
    public async Task GetToDoItems_ReturnsToDoItems()
    {
        // Assert
        Mock<IToDoClient> mockToDoClient = new Mock<IToDoClient>();
        GetToDoItemsResponse content = new GetToDoItemsResponse
        {
            Data = new List<ToDoItemDto>
            {
                new(Guid.NewGuid(), "Task 1", "Pending"),
                new(Guid.NewGuid(), "Task 2", "Completed"),
            }
        };
        ApiResponse<GetToDoItemsResponse> expectedResponse = new ApiResponse<GetToDoItemsResponse>(new HttpResponseMessage(HttpStatusCode.OK), content, new RefitSettings());
        mockToDoClient.Setup(x => x.GetToDoItems()).ReturnsAsync(expectedResponse);

        // Act
        ToDoService toDoService = new ToDoService(mockToDoClient.Object);
        List<ToDoItem> result = (await toDoService.GetToDoItems()).ToList();

        // Assert
        Assert.IsType<List<ToDoItem>>(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Task 1", result.ElementAt(0).Text);
        Assert.Equal("Pending", result.ElementAt(0).Status);
        Assert.Equal("Task 2", result.ElementAt(1).Text);
        Assert.Equal("Completed", result.ElementAt(1).Status);
    }

    [Fact]
    public async Task CreateToDoItem_ShouldReturnExpectedId()
    {
        // Assert
        Guid toDoId = Guid.NewGuid();
        Mock<IToDoClient> mockToDoClient = new Mock<IToDoClient>();
        ApiResponse<Guid> expectedResponse = new ApiResponse<Guid>(new HttpResponseMessage(HttpStatusCode.OK), toDoId, new RefitSettings());
        mockToDoClient.Setup(x => x.CreateToDoItem(new PostToDoItemRequestBody("Task 1"))).ReturnsAsync(expectedResponse);

        // Act
        ToDoService toDoService = new ToDoService(mockToDoClient.Object);
        Guid result = await toDoService.CreateToDoItem("Task 1");
        
        // Assert
        Assert.Equal(toDoId, result);
    }

    [Fact]
    public async Task UpdateToDoItemText_ShouldReturnTrue()
    {
        // Assert
        Mock<IToDoClient> mockToDoClient = new Mock<IToDoClient>();
        Mock<IApiResponse> apiResponse = new Mock<IApiResponse>();
        apiResponse.Setup(x => x.IsSuccessStatusCode).Returns(true);
        mockToDoClient.Setup(x => x.PatchToDoItemText(It.IsAny<Guid>(), It.IsAny<PatchToDoItemTextRequestBody>())).ReturnsAsync(apiResponse.Object);

        // Act
        ToDoService toDoService = new ToDoService(mockToDoClient.Object);
        bool result = await toDoService.PatchToDoItemText(Guid.NewGuid(), "Task 1");
        
        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task UpdateToDoItemStatus_ShouldReturnTrue()
    {
        // Assert
        Mock<IToDoClient> mockToDoClient = new Mock<IToDoClient>();
        Mock<IApiResponse> apiResponse = new Mock<IApiResponse>();
        apiResponse.Setup(x => x.IsSuccessStatusCode).Returns(true);
        mockToDoClient.Setup(x => x.PatchToDoItemStatus(It.IsAny<Guid>(), It.IsAny<PatchToDoItemStatusRequestBody>())).ReturnsAsync(apiResponse.Object);
        
        // Act
        ToDoService toDoService = new ToDoService(mockToDoClient.Object);
        bool result = await toDoService.PatchToDoItemStatus(Guid.NewGuid(), "Completed");
        
        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteToDoItem_ShouldReturnTrue()
    {
        // Assert
        Mock<IToDoClient> mockToDoClient = new Mock<IToDoClient>();
        Mock<IApiResponse> apiResponse = new Mock<IApiResponse>();
        apiResponse.Setup(x => x.IsSuccessStatusCode).Returns(true);
        mockToDoClient.Setup(x => x.DeleteToDoItem(It.IsAny<Guid>())).ReturnsAsync(apiResponse.Object);
        
        // Act
        ToDoService toDoService = new ToDoService(mockToDoClient.Object);
        bool result = await toDoService.DeleteToDoItem(Guid.NewGuid());
        
        // Assert
        Assert.True(result);
    }
}
