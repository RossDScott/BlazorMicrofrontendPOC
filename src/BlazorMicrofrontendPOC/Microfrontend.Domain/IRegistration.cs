using Microsoft.Extensions.DependencyInjection;

namespace Microfrontend.Domain;
public interface IRegistration
{
    public void Register(IServiceCollection serviceCollection);
}
