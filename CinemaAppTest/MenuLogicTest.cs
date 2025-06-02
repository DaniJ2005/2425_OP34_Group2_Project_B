[TestClass]
public class MenuLogicTest
{
    private IScreen dummyScreen1;
    private IScreen dummyScreen2;

    [TestInitialize]
    public void Setup()
    {
        // Arrange
        dummyScreen1 = new DummyScreen1();
        dummyScreen2 = new DummyScreen2();

        MenuLogic.screenStack.Clear();

        MenuLogic.NavigateTo(dummyScreen1);
        MenuLogic.NavigateTo(dummyScreen2);
    }

    [TestMethod]
    public void NavigateTo_PushesNewScreenOntoStack()
    {
        // Assert
        Assert.AreEqual(dummyScreen2, MenuLogic.screenStack.Peek(), "Top of stack should be dummyScreen2");
        Assert.AreEqual(2, MenuLogic.screenStack.Count, "Stack should contain 2 screens after navigation.");
    }

    [TestMethod]
    public void NavigateToPrevious_ReturnsToPreviousScreen()
    {
        // Act
        MenuLogic.NavigateToPrevious();

        // Assert
        Assert.AreEqual(dummyScreen1, MenuLogic.screenStack.Peek(), "Top of stack should be dummyScreen1 after navigating back.");
        Assert.AreEqual(1, MenuLogic.screenStack.Count, "Stack should contain 1 screen after navigating back.");
    }

    [TestMethod]
    public void NavigateTo_WithClearStack_ReplacesStackWithSingleScreen()
    {
        // Arrange
        IScreen dummyScreen1 = new DummyScreen1();
        IScreen dummyScreen2 = new DummyScreen2();

        MenuLogic.screenStack.Clear();
        MenuLogic.NavigateTo(dummyScreen2);

        // Act
        MenuLogic.NavigateTo(dummyScreen1, clearStack: true);

        // Assert
        Assert.AreEqual(1, MenuLogic.screenStack.Count, "Stack should contain only 1 screen after clearing.");
        Assert.AreEqual(dummyScreen1, MenuLogic.screenStack.Peek(), "Top of stack should be dummyScreen1 after clearing.");
    }
}
