// Enable UTF-8 output
Console.OutputEncoding = System.Text.Encoding.UTF8;

//hide cursor
Console.CursorVisible = false;

// Allow Dapper to correctly map snake_case columns (min_age) to PascalCase properties (MinAgeDb)
Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

//Dev only
// Db.DeleteTables();

// Initialize db tables to ensure they exist
Db.InitTables();

// Fill db with some data
Db.PopulateTables();

// Start application
if (!SessionDataLogic.HasPassedSymbolCheck())
    MenuLogic.NavigateTo(new SymbolCheckScreen());

SessionDataLogic.TryAutoLogin();

MenuLogic.NavigateTo(new HomeScreen());
