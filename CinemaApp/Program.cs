// Enable UTF-8 output
Console.OutputEncoding = System.Text.Encoding.UTF8;

//hide cursor
Console.CursorVisible = false;

//Dev only
// Db.DeleteTables();

// Initialize db tables
// Db.InitTables();

// Fill db with some data
// Db.PopulateTables();

// Start application
if (!SessionDataLogic.HasPassedSymbolCheck())
    MenuLogic.NavigateTo(new SymbolCheckScreen());

SessionDataLogic.TryAutoLogin();

MenuLogic.NavigateTo(new HomeScreen());