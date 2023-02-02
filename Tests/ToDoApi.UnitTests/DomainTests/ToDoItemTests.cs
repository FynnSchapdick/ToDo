using FluentAssertions;
using ToDoApi.Domain;
using System.Collections;

namespace ToDoApi.UnitTests.DomainTests;

public sealed class ValidUpdateToDoItemTestData : IEnumerable<object[]>
{
    private static readonly ToDoItem _validToDoItem = ToDoItem.Create("IchBinEinGültigesToDo");
    
    private readonly List<object[]> _data = new()
    {
        new object[]
        {
            _validToDoItem, "IchBinAuchGültig"
        },
    };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
    
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public sealed class InValidUpdateToDoItemTestData : IEnumerable<object[]>
{
    private static readonly ToDoItem _validToDoItem = ToDoItem.Create("IchBinEinGültigesToDo");
    
    private readonly List<object[]> _data = new()
    {
        new object[]
        {
            _validToDoItem, "Nein"
        },
    };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
    
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public sealed class CompleteToDoItemTestData : IEnumerable<object[]>
{
    private static readonly ToDoItem _notCompletedToDoItem = ToDoItem.Create("IchBinEinGültigesToDo");

    private static readonly ToDoItem _completedToDoItem = new ToDoItem
    {
        Id = Guid.NewGuid(),
        Text = "IchBinEinGültigesToDo",
        Completed = true
    };
    
    private readonly List<object[]> _data = new()
    {
        new object[]
        {
            _notCompletedToDoItem
        },
        new object[]
        {
            _completedToDoItem
        }
    };
    
    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
    
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}


public sealed class ToDoItemTests
{
    [InlineData("Dies ist ein Text")]
    [Theory]
    public void ToDoItem_Create_ShouldReturnToDoItem(string text)
    {
        ToDoItem toDoItem = ToDoItem.Create(text);
        toDoItem.Id.Should().NotBeEmpty();
        toDoItem.Text.Should().Be(text);
        toDoItem.Completed.Should().BeFalse();
    }

    [InlineData("Test")]
    [InlineData("Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata ")]
    [Theory]
    public void ToDoItem_Create_ShouldThrowArgumentException(string text)
    {
        Action createToDoItemAction = () => ToDoItem.Create(text);
        createToDoItemAction.Should().ThrowExactly<ArgumentException>();
    }

    [Theory]
    [ClassData(typeof(ValidUpdateToDoItemTestData))]
    public void ToDoItem_Update_ShouldReturnToDoItem(ToDoItem toDoItem, string text)
    {
        ToDoItem updatedToDoItem = ToDoItem.Update(toDoItem, text);
        updatedToDoItem.Id.Should().NotBeEmpty();
        updatedToDoItem.Text.Should().Be(text);
        updatedToDoItem.Completed.Should().BeFalse();
    }
    
    [Theory]
    [ClassData(typeof(InValidUpdateToDoItemTestData))]
    public void ToDoItem_Update_ShouldThrowArgumentException(ToDoItem toDoItem, string text)
    {
        Action updateToDoItemAction = () => ToDoItem.Update(toDoItem, text);
        updateToDoItemAction.Should().ThrowExactly<ArgumentException>();
    }
    
    [Theory]
    [ClassData(typeof(CompleteToDoItemTestData))]
    public void ToDoItem_Complete_ReturnToDoItem(ToDoItem toDoItem)
    {
        ToDoItem completedToDoItem = ToDoItem.Complete(toDoItem);
        completedToDoItem.Completed.Should().BeTrue();
    }
}