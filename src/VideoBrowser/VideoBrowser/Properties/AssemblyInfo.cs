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
"0024000004800000940000000602000000240000525341310004000001000100cdcc451381a6e3" +
"01e1d2dfe70b45affeff210f5142c073fde58773874293e4226e9a975eb543df6ce1a5ed0ddde2" +
"5ca816a0dce3720d7b0253885aa774780aad1a9b86fab0fdc9aed5425321514bc14af8f575a307" +
"11fa6a307345ef20adc9584a670ce418723cc2856e83dcdf1cf1fb1c75248794d1fe7e215f63f4" +
"fb6972d2")]
[assembly: InternalsVisibleTo("VideoBrowserTestApp, " +
"PublicKey=" +
"0024000004800000940000000602000000240000525341310004000001000100cdcc451381a6e3" +
"01e1d2dfe70b45affeff210f5142c073fde58773874293e4226e9a975eb543df6ce1a5ed0ddde2" +
"5ca816a0dce3720d7b0253885aa774780aad1a9b86fab0fdc9aed5425321514bc14af8f575a307" +
"11fa6a307345ef20adc9584a670ce418723cc2856e83dcdf1cf1fb1c75248794d1fe7e215f63f4" +
"fb6972d2")]

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