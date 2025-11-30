using System.CommandLine;

namespace DocuEye.CLI.Commands
{
    public static class CommandLineCommonOptions
    {
        public static Option<string> DocueyeAddressOption { get; private set; } = new("--docueye-address", "-a")
        {
            Description = "DocuEye address ex. http://localhost:8080.",
            Required = true,
            Validators =
                {
                    result =>
                    {
                        var value = result.GetValueOrDefault<string>();
                        if (!Uri.IsWellFormedUriString(value, UriKind.Absolute))
                        {
                            result.AddError("The DocuEye address is not a valid absolute URI.");
                        }
                    }
                }
        };
        public static Option<string> AdminTokenOption { get; private set; } = new("--admin-token", "-t")
        {
            Description = "The Admin token from DocuEye configuration.",
            Required = true
        };

       
    }
}
