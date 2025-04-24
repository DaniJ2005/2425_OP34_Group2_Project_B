 //Dev only
Db.DeleteTables();

// Initialize db tables
Db.InitTables();

// Fill db with some data
Db.PopulateTables();

// Start application
MenuLogic.NavigateTo(new Home());