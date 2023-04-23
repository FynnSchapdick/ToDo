using OpenQA.Selenium.Interactions;

namespace ToDo.App.IntegrationTests.PageTests;

public sealed class ToDosPageTests : SeleniumTestBase, IClassFixture<ToDoAppFactory>
{
    private readonly string _webUrl = "https://localhost:7112";
    private readonly TimeSpan _waitTime = TimeSpan.FromSeconds(1);

    public ToDosPageTests(ToDoAppFactory factory) : base(new WebDriverOptions())
    {
        factory.HostUrl = _webUrl;
        factory.CreateDefaultClient();
    }

    [Fact]
    public void FindTitle_ShouldFindTitle()
    {
        try
        {
            // Arrange
            WebDriver.Navigate().GoToUrl(_webUrl);
            Thread.Sleep(_waitTime);

            // Act + Assert
            new WebDriverWait(WebDriver, _waitTime)
                .Until(ExpectedConditions.TitleIs("ToDo App"))
                .Should()
                .BeTrue();

            TakeScreenShot(nameof(FindTitle_ShouldFindTitle));
        }
        catch
        {
            TakeScreenShot(nameof(FindTitle_ShouldFindTitle));
            throw;
        }
    }

    [Fact]
    public void CancelCreatingToDo_ShouldCancel()
    {
        try
        {
            // Arrange
            WebDriver.Navigate().GoToUrl(_webUrl);
            Thread.Sleep(_waitTime);

            // Act + Assert
            new WebDriverWait(WebDriver, _waitTime)
                .Until(ExpectedConditions.ElementToBeClickable(WebDriver.FindElement(
                    By.XPath(
                        "//button[@class='mud-button-root mud-button mud-button-outlined mud-button-outlined-primary mud-button-outlined-size-medium mud-ripple rounded-lg py-2 align-self-center']"))))
                .Click();

            new WebDriverWait(WebDriver, _waitTime)
                .Until(ExpectedConditions.ElementExists(By.CssSelector("div.mud-dialog-container.mud-dialog-center")))
                .FindElement(By.CssSelector("button.mud-button-text-error"))
                .Click();

            new WebDriverWait(WebDriver, _waitTime)
                .Until(ExpectedConditions.ElementToBeClickable(WebDriver.FindElement(
                    By.XPath(
                        "//button[@class='mud-button-root mud-button mud-button-outlined mud-button-outlined-primary mud-button-outlined-size-medium mud-ripple rounded-lg py-2 align-self-center']"))))
                .Should()
                .NotBeNull();

            TakeScreenShot(nameof(CancelCreatingToDo_ShouldCancel));
        }
        catch
        {
            TakeScreenShot(nameof(CancelCreatingToDo_ShouldCancel));
            throw;
        }
    }

    [Fact]
    public void CreateToDo_ShouldSave()
    {
        try
        {
            // Arrange
            WebDriver.Navigate().GoToUrl(_webUrl);
            Thread.Sleep(_waitTime);

            // Act + Assert
            new WebDriverWait(WebDriver, _waitTime)
                .Until(ExpectedConditions.ElementToBeClickable(WebDriver.FindElement(
                    By.XPath(
                        "//button[@class='mud-button-root mud-button mud-button-outlined mud-button-outlined-primary mud-button-outlined-size-medium mud-ripple rounded-lg py-2 align-self-center']"))))
                .Click();

            string? text = new WebDriverWait(WebDriver, _waitTime)
                .Until(ExpectedConditions.ElementExists(By.CssSelector("div.mud-dialog-container.mud-dialog-center")))
                .FindElement(By.CssSelector("div.mud-dialog-title > h6")).Text;

            text.Should().Be("Create ToDo");

            new WebDriverWait(WebDriver, _waitTime)
                .Until(ExpectedConditions.ElementExists(By.CssSelector("div.mud-input-control-input-container input")))
                .SendKeys("This is some text");

            new WebDriverWait(WebDriver, _waitTime)
                .Until(ExpectedConditions.ElementToBeClickable(By.XPath(
                    "//button[@class='mud-button-root mud-button mud-button-text mud-button-text-primary mud-button-text-size-medium mud-ripple']")))
                .Click();

            TakeScreenShot(nameof(CreateToDo_ShouldSave));
        }
        catch
        {
            TakeScreenShot(nameof(CreateToDo_ShouldSave));
            throw;
        }
    }

    [Fact]
    public void CreateToDoWithInvalidText_ShouldDisplayErrorText_AndSaveShouldDoNothing()
    {
        try
        {
            // Arrange
            WebDriver.Navigate().GoToUrl(_webUrl);
            Thread.Sleep(_waitTime);

            // Act + Assert
            new WebDriverWait(WebDriver, _waitTime)
                .Until(ExpectedConditions.ElementToBeClickable(WebDriver.FindElement(
                    By.XPath(
                        "//button[@class='mud-button-root mud-button mud-button-outlined mud-button-outlined-primary mud-button-outlined-size-medium mud-ripple rounded-lg py-2 align-self-center']"))))
                .Click();

            new WebDriverWait(WebDriver, _waitTime)
                .Until(ExpectedConditions.ElementExists(By.CssSelector("div.mud-dialog-container.mud-dialog-center")));

            new WebDriverWait(WebDriver, _waitTime)
                .Until(ExpectedConditions.ElementExists(By.CssSelector("div.mud-input-control-input-container input")))
                .SendKeys("blu");

            new WebDriverWait(WebDriver, _waitTime)
                .Until(ExpectedConditions.ElementExists(
                    By.CssSelector("div.mud-input-control-helper-container > p.mud-input-helper-text.mud-input-error")))
                .Text
                .Should()
                .NotBeEmpty();

            new WebDriverWait(WebDriver, _waitTime)
                .Until(ExpectedConditions.ElementToBeClickable(By.XPath(
                    "//button[@class='mud-button-root mud-button mud-button-text mud-button-text-primary mud-button-text-size-medium mud-ripple']")))
                .Click();

            TakeScreenShot(nameof(CreateToDoWithInvalidText_ShouldDisplayErrorText_AndSaveShouldDoNothing));
        }
        catch
        {
            TakeScreenShot(nameof(CreateToDoWithInvalidText_ShouldDisplayErrorText_AndSaveShouldDoNothing));
            throw;
        }
    }

    [Fact]
    public void UpdateToDoText_ShouldSave()
    {
        try
        {
            // Arrange
            WebDriver.Navigate().GoToUrl(_webUrl);
            Thread.Sleep(_waitTime);

            // Act + Assert
            new WebDriverWait(WebDriver, _waitTime)
                .Until(ExpectedConditions.ElementToBeClickable(
                    By.CssSelector("button[aria-label='edit'].mud-button-root")))
                .Click();

            new WebDriverWait(WebDriver, _waitTime)
                .Until(ExpectedConditions.ElementExists(By.CssSelector("div.mud-dialog-container.mud-dialog-center")));

            new WebDriverWait(WebDriver, _waitTime)
                .Until(ExpectedConditions.ElementExists(By.CssSelector("div.mud-input-control-input-container input")))
                .SendKeys("myNewText");

            new WebDriverWait(WebDriver, _waitTime)
                .Until(ExpectedConditions.ElementToBeClickable(By.XPath(
                    "//button[@class='mud-button-root mud-button mud-button-text mud-button-text-primary mud-button-text-size-medium mud-ripple']")))
                .Click();

            TakeScreenShot(nameof(UpdateToDoText_ShouldSave));
        }
        catch
        {
            TakeScreenShot(nameof(UpdateToDoText_ShouldSave));
            throw;
        }
    }

    [Fact]
    public void DeleteToDo_ShouldSave()
    {
        try
        {
            // Arrange
            WebDriver.Navigate().GoToUrl(_webUrl);
            Thread.Sleep(_waitTime);

            // Act + Assert
            new WebDriverWait(WebDriver, _waitTime)
                .Until(ExpectedConditions.ElementToBeClickable(
                    By.CssSelector("button[aria-label='delete'].mud-button-root")))
                .Click();

            new WebDriverWait(WebDriver, _waitTime)
                .Until(ExpectedConditions.ElementExists(By.CssSelector("div.mud-dialog-container.mud-dialog-center")));

            new WebDriverWait(WebDriver, _waitTime)
                .Until(ExpectedConditions.ElementToBeClickable(
                    By.CssSelector("button.mud-button-text-primary span.mud-button-label")))
                .Click();

            TakeScreenShot(nameof(DeleteToDo_ShouldSave));
        }
        catch
        {
            TakeScreenShot(nameof(DeleteToDo_ShouldSave));
            throw;
        }
    }

    [Fact]
    public void DeleteToDo_ShouldCancel()
    {
        try
        {
            // Arrange
            WebDriver.Navigate().GoToUrl(_webUrl);
            Thread.Sleep(_waitTime);

            // Act + Assert
            new WebDriverWait(WebDriver, _waitTime)
                .Until(ExpectedConditions.ElementToBeClickable(
                    By.CssSelector("button[aria-label='delete'].mud-button-root")))
                .Click();

            new WebDriverWait(WebDriver, _waitTime)
                .Until(ExpectedConditions.ElementExists(By.CssSelector("div.mud-dialog-container.mud-dialog-center")));

            new WebDriverWait(WebDriver, _waitTime)
                .Until(ExpectedConditions.ElementToBeClickable(
                    By.CssSelector("button.mud-button-text-error span.mud-button-label")))
                .Click();

            TakeScreenShot(nameof(DeleteToDo_ShouldCancel));
        }
        catch
        {
            TakeScreenShot(nameof(DeleteToDo_ShouldCancel));
            throw;
        }
    }

    [Fact]
    public void UpdateToDoStatus_ShouldBeCompleted()
    {
        try
        {
            // Assert
            WebDriver.Navigate().GoToUrl(_webUrl);
            Thread.Sleep(_waitTime);
        
            // Act + Assert
            new Actions(WebDriver)
                .ClickAndHold(new WebDriverWait(WebDriver, _waitTime)
                    .Until(ExpectedConditions.ElementExists(By.CssSelector(".mud-drop-item"))))
                .MoveToElement(new WebDriverWait(WebDriver, _waitTime)
                    .Until(ExpectedConditions.ElementExists(By.CssSelector("[identifier='Completed']"))))
                .Release()
                .Perform();

            TakeScreenShot(nameof(UpdateToDoStatus_ShouldBeCompleted));
        }
        catch
        {
            TakeScreenShot(nameof(UpdateToDoStatus_ShouldBeCompleted));
            throw;
        }
    }
}