public static class MenuLogic
{
    static Stack<IScreen> screenStack = new Stack<IScreen>();

    public static void NavigateTo(IScreen screen, bool clearStack = false)
    {
        if (clearStack)
            screenStack.Clear();
        
        screenStack.Push(screen);
        Console.Clear();
        screen.Start();
    }

    public static void NavigateToPrevious()
    {
        if (screenStack.Count > 1)
        {
            screenStack.Pop();
            Console.Clear();
            IScreen previous = screenStack.Peek();
            previous.Start();
            return;
        }
        
        NavigateTo(new ExitScreen());
    }

    public static void RestartScreen()
    {
        IScreen current = screenStack.Peek();
        current.Start();
    }

    public static void ClearStack() => screenStack.Clear();
}