public static class MenuLogic
{
    public static Stack<IScreen> screenStack = new Stack<IScreen>();

    public static void NavigateTo(IScreen screen, bool clearStack = false)
    {
        if (clearStack)
            screenStack.Clear();
        
        screenStack.Push(screen);
        screen.Start();
    }

    public static void NavigateToPrevious()
    {
        if (screenStack.Count > 1)
        {
            screenStack.Pop();
            IScreen previous = screenStack.Peek();
            previous.Start();
            return;
        }
        
        NavigateTo(new ExitLogoutScreen(false));
    }

    public static void RestartScreen()
    {
        IScreen current = screenStack.Peek();
        current.Start();
    }

    public static void ClearStack() => screenStack.Clear();
}