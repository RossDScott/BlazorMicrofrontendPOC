namespace HostSite.Options;

public record MicrofrontendRegistrationOptions
{
    public const string MicrofrontendRegistration = "MicrofrontendRegistration";
    public IEnumerable<LocalMicrofrontRegistration> Local { get; set; }
}

public record LocalMicrofrontRegistration
{
    public string Name { get; set; }
    public string AssemblyFolderPath { get; set; }
    public string AssemblyFileName { get; set; }
    public IEnumerable<string> DependencyFileNames { get; set; } = Enumerable.Empty<string>();
    public IEnumerable<MenuItem> MenuItems { get; set; } = Enumerable.Empty<MenuItem>();
}

public record MenuItem
{
    public string Name { get; set; }
    public string Path { get; set; }

    public IEnumerable<MenuItem> Items { get; set; } = Enumerable.Empty<MenuItem>();
}