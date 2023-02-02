using FluentAssertions;
using Framework.Core.Helpers;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using ToDoApi.Application.Features;
using ToDoApi.Domain;
using ToDoApi.Domain.Interfaces;

namespace ToDoApi.UnitTests.ApplicationTests;

public sealed class GetPaginatedToDoItemsHandlerTests
{
    private readonly GetPaginatedToDoItemsQueryHandler _handler;
    private readonly Mock<IToDoItemRepository> _repoMock = new();
    private readonly Mock<ISieveProcessor> _sieveMock = new();

    public GetPaginatedToDoItemsHandlerTests()
    {
        _handler = new GetPaginatedToDoItemsQueryHandler(_sieveMock.Object, _repoMock.Object);
    }

    [Fact]
    public async Task GetPaginatedToDoItemsQueryHandler_Handle_ShouldHandle()
    {
        Guid id = Guid.NewGuid();
        SieveModel sieveModel = new SieveModel {Page = 1, PageSize = 10};
        GetPaginatedToDoItemsQuery query = new(sieveModel);
        Mock<DbSet<ToDoItem>>? mockDbSet = new List<ToDoItem>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Completed = false,
                Text = "HamloIchBims"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Completed = false,
                Text = "HamloIchBims"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Completed = false,
                Text = "HamloIchBims"
            },
        }.AsQueryable().BuildMockDbSet();
        
        IQueryable<ToDoItem> queryable = new List<ToDoItem>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Completed = false,
                Text = "HamloIchBims"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Completed = false,
                Text = "HamloIchBims"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Completed = false,
                Text = "HamloIchBims"
            },
        }.AsQueryable();
        CancellationToken cancellationToken = new();
        _repoMock.Setup(x => x.ToDoItems.AsNoTracking())
            .Returns(queryable);

        _sieveMock.Setup(x => x.Apply(sieveModel, queryable, null, true, true, false))
            .Returns(queryable);
        
        _sieveMock.Setup(x => x.Apply(sieveModel, queryable, null, true, true, true))
            .Returns(queryable);

        PaginatedList<ToDoItemDto> result = await _handler.Handle(query, cancellationToken);
        result.Should().NotBeEmpty();
        result.PageSize.Should().Be(10);
        result.CurrentPage.Should().Be(1);
        result.HasNext.Should().BeFalse();
    }
}