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
        
        ShowExitConfirmation();
    }

    public static void ClearStack() => screenStack.Clear();

    public static void ShowExitConfirmation()
    {
        int selectedIndex = 0;
        ConsoleKey key;

        do
        {
            Console.Clear();
            Console.WriteLine("Are you sure you want to exit?\n");

            if (selectedIndex == 0)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("[ Cancel ]");
                Console.ResetColor();
                Console.Write("   Exit   ");
            }
            else
            {
                Console.Write("  Cancel   ");
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("[ Exit ]");
                Console.ResetColor();
            }

            key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.LeftArrow && selectedIndex > 0)
                selectedIndex--;
            else if (key == ConsoleKey.RightArrow && selectedIndex < 1)
                selectedIndex++;

        } while (key != ConsoleKey.Enter);

        Console.Clear();
        if (selectedIndex == 1)
        {
            Console.WriteLine("Exiting program...");
            Environment.Exit(0);
        }
        else
        {
            if (screenStack.Count > 0)
            {
                IScreen current = screenStack.Peek();
                current.Start();
            }
        }
    }

}