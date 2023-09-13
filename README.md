# BlazorMicrofrontendPOC

This repo demonstrates some of the main concepts in creating a Microfrontend.  

The HostSite is responsible for loading the assemblies configured in MicrofrontendRegistration.json. For this POC it only loads assemblies from a local folder, however a proper system could use various sources such as blob storage, cdn etc.   
The RegistrationBuilder is responsible loading the asemblies, their dependencies and calling any required DI.   
App.razor becomes aware of dynamically loaded pages using AdditionalAssemblies="RegistrationBuilder.Assemblies"   
Index.razor demonstrates dynamically loading a component either by name or by interface implementation.   

LibraryOne has a component and a couple of Pages in.   
LibraryTwo has a dependency on a different version of MudBlazor than the HostSite. It also using DI for a local service.

If you want to run this locally you will need to update the AssemblyFolderPaths in MicrofrontendRegistration.json
