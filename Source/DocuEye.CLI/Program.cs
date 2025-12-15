using DocuEye.CLI.Commands;


Console.WriteLine("DocuEye CLI");
var command = new MainCommand();
return await command.Parse(args).InvokeAsync();