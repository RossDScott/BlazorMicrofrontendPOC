using System.Reflection;
using System.Runtime.Loader;

namespace HostSite.Microfrontend;

public record AssemblyActivation
{
    public AssemblyLoadContext Context { get; init; } = default!;
    public Assembly MainAssembly { get; init; } = default!;
    public IEnumerable<Assembly> Dependencies { get; set; } = Enumerable.Empty<Assembly>();
}