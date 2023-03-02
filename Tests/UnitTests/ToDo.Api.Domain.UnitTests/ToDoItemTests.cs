using ToDo.Api.Domain;
using ToDo.Api.Domain.Exceptions;

namespace ToDo.Core.UnitTests;

public sealed class ToDoItemTests
{
    private const string ExpectedInvalidTextExceptionMessageOnInit = "Violated rules in 'Init' with value '{0}'.";
    private const string ExpectedInvalidTextExceptionMessageOnCreate = "Violated rules in 'Create' with value '{0}'.";
    private const string ExpectedInvalidTextExceptionMessageOnUpdateText = "Violated rules in 'UpdateText' with value '{0}'.";
    private const string ExpectedInvalidGuidExceptionMessage = "Violated rules in 'Init' with value '00000000-0000-0000-0000-000000000000'.";
    
    [Fact]
    public void ToDoItem_Create_ReturnsToDoItem_WithValidText()
    {
        string initialText = "LoremIpsum";
        ToDoItem toDoItem = ToDoItem.Create(initialText);
        Assert.Equal(initialText, toDoItem.Text);
        Assert.NotEqual(Guid.Empty, toDoItem.Id);
        Assert.False(toDoItem.Completed);
    }
    
    [Fact]
    public void ToDoItem_Create_ThrowsException_WithInvalidText()
    {
        Action createToDoItem = () => ToDoItem.Create(string.Empty);
        Assert.Throws<ToDoItemException>(createToDoItem);
        Assert.Equal(string.Format(ExpectedInvalidTextExceptionMessageOnCreate, string.Empty),
            Assert.Throws<ToDoItemException>(createToDoItem).Message);
    }

    [Fact]
    public void ToDoItem_Init_ThrowsException_WithInvalidText()
    {
        Action createToDoItem = () => ToDoItem.Init(Guid.NewGuid(),string.Empty);
        Assert.Equal(string.Format(ExpectedInvalidTextExceptionMessageOnInit, string.Empty),
            Assert.Throws<ToDoItemException>(createToDoItem).Message);
    }
    
    [Fact]
    public void ToDoItem_Init_ThrowsException_WithInvalidGuid()
    {
        Action createToDoItem = () => ToDoItem.Init(Guid.Empty, "LoremIpsum");
        Assert.Equal(string.Format(ExpectedInvalidGuidExceptionMessage),
            Assert.Throws<ToDoItemException>(createToDoItem).Message);
    }
    
    [Fact]
    public void ToDoItem_ToggleComlete_ReturnVoid_CompletedShouldBeToggled()
    {
        Guid id = Guid.NewGuid();
        ToDoItem toDoItem = ToDoItem.Init(id, "LoremIpsum");
        Assert.False(toDoItem.Completed);
        toDoItem.ToggleComplete();
        Assert.True(toDoItem.Completed);
    }
    
    [Fact]
    public void ToDoItem_UpdateText_ReturnVoid_TextShouldBeUpdated()
    {
        Guid id = Guid.NewGuid();
        string initialText = "LoremIpsum";
        string updatedText = "LoremIpsumLoremIpsum";
        ToDoItem toDoItem = ToDoItem.Init(id, initialText);
        Assert.Equal(initialText,toDoItem.Text);
        toDoItem.UpdateText(updatedText);
        Assert.Equal(updatedText,toDoItem.Text);
    }
    
    [Fact]
    public void ToDoItem_UpdateText_ThrowsException_WithInvalidText()
    {
        Guid id = Guid.NewGuid();
        string initialText = "LoremIpsum";
        string updateText = string.Empty;
        Action initToDoItemAndTryToUpdateText = ()
            => ToDoItem.Init(id, initialText).UpdateText(updateText);
        Assert.Equal(string.Format(ExpectedInvalidTextExceptionMessageOnUpdateText, updateText),
            Assert.Throws<ToDoItemException>(initToDoItemAndTryToUpdateText).Message);
    }
}