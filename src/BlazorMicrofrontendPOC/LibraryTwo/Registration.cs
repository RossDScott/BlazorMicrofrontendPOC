using Microfrontend.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryTwo;
public class Registration : IRegistration
{
    public void Register(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ISaySomething, SayHowYouDoing>();
    }
}
