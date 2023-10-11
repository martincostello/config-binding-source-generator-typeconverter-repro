# Config binding source generator TypeConverter repro

A repository which reproduces [dotnet/runtime#CHANGE_ME](https://github.com/dotnet/runtime/issues/CHANGE_ME) when using the configuration binding source generator.

To reproduce the issue, run the following command:

```console
$ dotnet run --project .\Repro\

Unhandled exception. System.InvalidOperationException: Cannot create instance of type 'Repro.Geolocation' because parameter 'latitude' has no matching config. Each parameter in the constructor that does not have a default value must have a corresponding config entry.
   at Microsoft.Extensions.Configuration.Binder.SourceGeneration.<BindingExtensions_g>FC276FD7C89D1ED0B2B3E96BAC8F157FABB86A642EAABC54639B357A857C01D08__BindingExtensions.InitializeGeolocation(IConfiguration configuration, BinderOptions binderOptions) in .\config-binding-source-generator-typeconverter-repro\Repro\Microsoft.Extensions.Configuration.Binder.SourceGeneration\Microsoft.Extensions.Configuration.Binder.SourceGeneration.ConfigurationBindingGenerator\BindingExtensions.g.cs:line 174
   at Microsoft.Extensions.Configuration.Binder.SourceGeneration.<BindingExtensions_g>FC276FD7C89D1ED0B2B3E96BAC8F157FABB86A642EAABC54639B357A857C01D08__BindingExtensions.BindCore(IConfiguration configuration, IDictionary`2& instance, Boolean defaultValueIfNotFound, BinderOptions binderOptions) in .\config-binding-source-generator-typeconverter-repro\Repro\Microsoft.Extensions.Configuration.Binder.SourceGeneration\Microsoft.Extensions.Configuration.Binder.SourceGeneration.ConfigurationBindingGenerator\BindingExtensions.g.cs:line 134
   at Microsoft.Extensions.Configuration.Binder.SourceGeneration.<BindingExtensions_g>FC276FD7C89D1ED0B2B3E96BAC8F157FABB86A642EAABC54639B357A857C01D08__BindingExtensions.BindCore(IConfiguration configuration, SiteOptions& instance, Boolean defaultValueIfNotFound, BinderOptions binderOptions) in .\config-binding-source-generator-typeconverter-repro\Repro\Microsoft.Extensions.Configuration.Binder.SourceGeneration\Microsoft.Extensions.Configuration.Binder.SourceGeneration.ConfigurationBindingGenerator\BindingExtensions.g.cs:line 160
   at Microsoft.Extensions.Configuration.Binder.SourceGeneration.<BindingExtensions_g>FC276FD7C89D1ED0B2B3E96BAC8F157FABB86A642EAABC54639B357A857C01D08__BindingExtensions.BindCoreMain(IConfiguration configuration, Object instance, Type type, Action`1 configureOptions) in .\config-binding-source-generator-typeconverter-repro\Repro\Microsoft.Extensions.Configuration.Binder.SourceGeneration\Microsoft.Extensions.Configuration.Binder.SourceGeneration.ConfigurationBindingGenerator\BindingExtensions.g.cs:line 83
   at Microsoft.Extensions.Configuration.Binder.SourceGeneration.<BindingExtensions_g>FC276FD7C89D1ED0B2B3E96BAC8F157FABB86A642EAABC54639B357A857C01D08__BindingExtensions.<>c__DisplayClass1_0`1.<Configure>b__0(TOptions instance) in .\config-binding-source-generator-typeconverter-repro\Repro\Microsoft.Extensions.Configuration.Binder.SourceGeneration\Microsoft.Extensions.Configuration.Binder.SourceGeneration.ConfigurationBindingGenerator\BindingExtensions.g.cs:line 58
   at Microsoft.Extensions.Options.OptionsFactory`1.Create(String name)
   at Microsoft.Extensions.Options.UnnamedOptionsManager`1.get_Value()
   at Program.<Main>$(String[] args) in .\config-binding-source-generator-typeconverter-repro\Repro\Program.cs:line 11
```
