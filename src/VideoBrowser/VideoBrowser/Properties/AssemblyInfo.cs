using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("VideoBrowser")]
[assembly: AssemblyDescription("Online video browser and downloader")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Cekli")]
[assembly: AssemblyProduct("VideoBrowser")]
[assembly: AssemblyCopyright("Copyright©2020")]
[assembly: AssemblyTrademark("Cekli")]
[assembly: AssemblyCulture("")]
[assembly: InternalsVisibleTo("VideoBrowser.Test, " +
"PublicKey=" +
"0024000004800000940000000602000000240000525341310004000001000100ed44e06f93266b" +
"4081b776c95b40fbed5c1d5011b0314f3d5b9dc1553b0d3ceca80e09bf92977cdfcb3eebc16000" +
"2008922eab8cbd5f7a14e2b45ddc3b9c86e446a627299ad4b1ec1d87c05a005e99e25fff77b1c4" +
"a9a1c85365ab65579bd74a18ab20e2c7cfd25a822736bf9b0710aad118ccace978c511bbf2c9d6" +
"053c74c5")]
[assembly: InternalsVisibleTo("VideoBrowserTestApp, " +
"PublicKey=" +
"0024000004800000940000000602000000240000525341310004000001000100ed44e06f93266b" +
"4081b776c95b40fbed5c1d5011b0314f3d5b9dc1553b0d3ceca80e09bf92977cdfcb3eebc16000" +
"2008922eab8cbd5f7a14e2b45ddc3b9c86e446a627299ad4b1ec1d87c05a005e99e25fff77b1c4" +
"a9a1c85365ab65579bd74a18ab20e2c7cfd25a822736bf9b0710aad118ccace978c511bbf2c9d6" +
"053c74c5")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

//In order to begin building localizable applications, set
//<UICulture>CultureYouAreCodingWith</UICulture> in your .csproj file
//inside a <PropertyGroup>.  For example, if you are using US english
//in your source files, set the <UICulture> to en-US.  Then uncomment
//the NeutralResourceLanguage attribute below.  Update the "en-US" in
//the line below to match the UICulture setting in the project file.

//[assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.Satellite)]

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                                     //(used if a resource is not found in the page,
                                     // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                              //(used if a resource is not found in the page,
                                              // app, or any theme specific resource dictionaries)
)]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("0.1.5.0")]
[assembly: AssemblyFileVersion("0.1.5.0")]
[assembly: NeutralResourcesLanguage("en-US")]
[assembly: Guid("06AEED36-193A-434D-B5CA-27BCAB38F15C")]