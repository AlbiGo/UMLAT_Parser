using UMLAT_Parser;

Console.WriteLine("Hello, World!");

var str = "JAEAESKELAEINEN";

// RUESSWUSSRUEMSSUE 

Console.WriteLine("Step 1");

var uamltCounterpart = str.ConvertToUMLAT();

Console.WriteLine(uamltCounterpart);

Console.WriteLine("----------------------------------------------------------------------------------");

Console.WriteLine("Step 2");

var allUAML = str.GetAllUAMLTVariations();

allUAML.ForEach(u =>
{
    Console.WriteLine(u.ToString());
});
Console.WriteLine("----------------------------------------------------------------------------------");

Console.WriteLine("Step 3");

var sqlStatement = str.GenerateUAMLTSqlStatement();

Console.WriteLine(sqlStatement);
Console.WriteLine("----------------------------------------------------------------------------------");
Console.ReadLine();

