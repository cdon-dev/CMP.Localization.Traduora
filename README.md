# CMP.Localization.Traduora
.NET Core Localizers backed by Traduora

## Getting the sample project running

#### Configure Traduora
1. Go to https://traduora.azurewebsites.net/ and create an account
2. Add a few languages, terms and translations for a project
3. Create an API Key and keep the Client ID and Secret in some file on your local computer (do not commit secrets into a git repo)

#### Configure Secrets
1. Open the CMP.Localization.Traduora solution in Visual Studio
2. Right click on the `Web` project and click on `Manage User Secrets`
3. You can safely store your Client ID and Secret from Traduora here and then the code makes use of it through IConfiguration
4. Change the _config keys in the `sample\Web\Controllers\HomeController` file 

#### In Visual Studio
Build and run

#### Command Line
````
dotnet restore
dotnet build
dotnet run -p sample\Web\Web.csproj
````

#### URL's to explore
* http://localhost:52512
* http://localhost:52512/?culture=en
* http://localhost:52512/?culture=de-DE