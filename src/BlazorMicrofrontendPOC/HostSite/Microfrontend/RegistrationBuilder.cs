using HostSite.Options;
using Microfrontend.Domain;
using System.Reflection;
using System.Runtime.Loader;

namespace HostSite.Microfrontend;

public class RegistrationBuilder
{
    private readonly MicrofrontendRegistrationOptions _options;
    private readonly List<AssemblyActivation> _activations = new();

    public IEnumerable<Assembly> Assemblies => _activations.Select(x => x.MainAssembly);
    public IEnumerable<MenuItem> MenuItems
        => _options.Local.Select(x => new MenuItem { Name = x.Name, Items = x.MenuItems })
            .ToList();

    public RegistrationBuilder(WebApplicationBuilder webApplicationBuilder)
    {
        var options = webApplicationBuilder.Configuration
            .GetRequiredSection(MicrofrontendRegistrationOptions.MicrofrontendRegistration)
            .Get<MicrofrontendRegistrationOptions>()!;

        _options = options;

        _activations.AddRange(_options.Local.Select(ops => LoadFromLocalFile(ops)));

        RegisterDependencies(webApplicationBuilder.Services);

    }

    public IEnumerable<Type> FetchExportedTypesFor<T>()
        => ExportedTypes
            .Where(type => typeof(T).IsAssignableFrom(type) && !type.IsInterface)
            .ToList();

    public Type FetchExportedTypeByFullName(string name)
        => ExportedTypes.Single(type => type.FullName == name);

    private AssemblyActivation LoadFromLocalFile(LocalMicrofrontRegistration options)
    {
        var context = new AssemblyLoadContext(options.Name);
        var assembly = context.LoadFromAssemblyPath(Path.Combine(options.AssemblyFolderPath, options.AssemblyFileName));

        var dependencies = options.DependencyFileNames?
            .Select(fileName => context.LoadFromAssemblyPath(Path.Combine(options.AssemblyFolderPath, fileName)))
            .ToList();

        return new AssemblyActivation
        {
            Context = context,
            MainAssembly = assembly,
            Dependencies = dependencies ?? Enumerable.Empty<Assembly>()
        };
    }

    private void RegisterDependencies(IServiceCollection services)
    {
        var registrationTypes = FetchExportedTypesFor<IRegistration>();
        foreach (var registrationType in registrationTypes)
        {
            var instance = (IRegistration)Activator.CreateInstance(registrationType)!;
            instance.Register(services);
        }
    }

    private IEnumerable<Type> ExportedTypes
        => _activations.SelectMany(x => x.MainAssembly.ExportedTypes);
}
