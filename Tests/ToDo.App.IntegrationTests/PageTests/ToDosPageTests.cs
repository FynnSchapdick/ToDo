using FluentAssertions;
using Withywoods.Selenium;

namespace ToDo.App.IntegrationTests.PageTests;

public sealed class ToDosPageTests : SeleniumTestBase, IClassFixture<ToDoAppFactory>
{
    private readonly string _webUrl = "https://localhost:7112";
    
    public ToDosPageTests(ToDoAppFactory factory) : base(new WebDriverOptions())
    {
        factory.HostUrl = _webUrl;
        factory.CreateDefaultClient();
    }
    
    [Fact]
    public void GoToToDosPage_ShouldFindIndexTitle()
    {
        try
        {
            // Arrange & Act
            WebDriver.Navigate().GoToUrl($"{_webUrl}/todos");
            Thread.Sleep(TimeSpan.FromMilliseconds(300));

            // Assert
            WebDriver.Title.Should().Be("Todos");
            TakeScreenShot(nameof(GoToToDosPage_ShouldFindIndexTitle));
        }
        catch
        {
            TakeScreenShot(nameof(GoToToDosPage_ShouldFindIndexTitle));
            throw;
        }
    }
}