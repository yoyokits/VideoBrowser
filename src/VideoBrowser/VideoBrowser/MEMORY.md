# MEMORY.md - VideoBrowser Project Facts

## Project Identity
- **Name:** VideoBrowser (also known as Cekli Browser)
- **Type:** WPF Desktop Application (.NET Framework 4.8)
- **Purpose:** Web browser with integrated YouTube video downloading
- **License:** MIT
- **Primary Author:** yoyokits (Yohanes Wahyu Nurcahyo)

## Repository Structure
```
VideoBrowser/
├── src/
│   └── VideoBrowser/
│       ├── VideoBrowser/          # Main WPF application
│       │   ├── ViewModels/        # MVVM ViewModels
│       │   ├── Views/             # XAML views
│       │   ├── Controls/          # Custom controls (CefSharpBrowser, etc.)
│       │   ├── Core/              # Business logic, youtube-dl wrapper
│       │   ├── Models/            # Data models
│       │   ├── Helpers/           # Utilities (FFmpeg, Process, File)
│       │   ├── Common/            # Shared (RelayCommand, NotifyPropertyChanged)
│       │   ├── Converters/        # Value converters
│       │   ├── Resources/         # Icons, CSS, images
│       │   ├── VideoBrowser.csproj
│       │   ├── App.xaml
│       │   ├── MainWindow.xaml
│       │   ├── Binaries/          # ffmpeg.exe, youtube-dl.exe (embedded)
│       │   └── ...
│       ├── VideoBrowser.sln       # Solution file
│       ├── VideoBrowser.Test/     # Unit tests
│       ├── VideoBrowserSetup/    # Installer project
│       └── VideoBrowserTestApp/   # Test harness app
├── docs/                         # Jekyll site (Ruby-based)
│   ├── Gemfile
│   └── Gemfile.lock
└── README.md
```

## Technology Stack (Main Project)
- **Framework:** .NET Framework 4.8 (Windows-only, requires full framework)
- **UI:** WPF with XAML
- **Architecture:** MVVM (Model-View-ViewModel)
- **Browser:** CefSharp.Wpf 99.2.120 (Chromium-based)
- **Styling:** MahApps.Metro 1.6.5.1, Dragablz
- **Logging:** log4net
- **Utilities:** Ookii.Dialogs.Wpf, Newtonsoft.Json
- **Native Binaries:**
  - `youtube-dl.exe` (Python-based; auto-updates on startup)
  - `ffmpeg.exe` (for video post-processing)

## Build Requirements
- **OS:** Windows 7+ (for WPF + CefSharp)
- **IDE:** Visual Studio 2019/2022 (recommended) or MSBuild command-line
- **SDK:** .NET Framework 4.8 SDK
- **NuGet:** CefSharp.Wpf 99.2.120 (packages may be restored on build)

### Build Commands
```bash
# Using MSBuild
msbuild VideoBrowser.sln /p:Configuration=Debug
msbuild VideoBrowser.sln /p:Configuration=Release

# Using Visual Studio
devenv VideoBrowser.sln /Build Debug
```

**Note:** WSL/Linux cannot build or run WPF applications.

## Key Classes & Architecture

### ViewModel Base
- `Common/NotifyPropertyChanged.cs` - Implements `INotifyPropertyChanged`
- `Common/RelayCommand.cs` - Delegate command implementation (`ICommand`)

### Main Application Flow
- `App.xaml` → `App.xaml.cs`: Startup calls `UpdateYoutubeDl()` (async youtube-dl update)
- `MainWindow.xaml` → `MainWindowViewModel.cs`: Central hub managing:
  - `WebBrowserTabControlViewModel` - Tabbed browser
  - `DownloadQueueViewModel` - Download management
  - `DownloadFlyoutViewModel` - Download flyout panel
  - `AboutViewModel` - About dialog

### Browser Control (`Controls/CefSharpBrowser/`)
- `CefSharpBrowser.xaml` - UserControl hosting Chromium browser
- `VideoBrowserViewModel` - ViewModel for browser tab
- `SettingsViewModel` - Browser settings (proxy, cache, user-agent)
- `DownloadHandler` - Captures downloads initiated from browser
- `TabItem` / `InterTabClient` - Dragablz tab integration

### Download System (`Core/`)
- `YoutubeDl.cs` - Wrapper for youtube-dl.exe CLI
- `FileDownloader.cs` - Downloads files using WebClient/HttpClient
- `DownloadQueueHandler.cs` - Manages concurrent downloads (max = `MaxSimDownloads`)
- `Operation` - Represents a single download operation with progress events
- `PlayList.cs` - Parses youtube-dl JSON output (video info)

### Settings Persistence
- User settings: `Properties.Settings.Default` (auto-persisted)
  - `WindowPosition`, `WindowWidth`, `WindowHeight`, `WindowState`
  - `LastUrl` (default: welcome page)
  - `MaxSimDownloads` (default: 5)
  - `ShowMaxSimDownloads` (limit checkbox state)
- Settings saved on window closing (`MainWindowViewModel.OnClosing`)

## Important Files & Locations

### Configuration
- `App.config` - User settings defined here
- `Properties/Settings.settings` - Settings designer file
- `Properties/AssemblyInfo.cs` - Assembly metadata
- `VideoBrowser.csproj` - Project file, references, embedded resources

### Application Icon & Resources
- `Icon.ico` - Application icon (embedded)
- `Controls/CefSharpBrowser/Resources/` - Web assets for embedded help pages
- `Docs/Documentation.txt` - Bundled documentation (embedded)

### Binaries (Embedded)
- `Binaries/ffmpeg.exe` - FFmpeg binary (copied to output)
- `Binaries/youtube-dl.exe` - youtube-dl binary (auto-updates)
- These are marked as `EmbeddedResource` in .csproj with `CopyToOutputDirectory`

### Signing
- `key.snk` - Strong name key (do not expose in public commits)
- Assembly signing enabled: `<SignAssembly>true</SignAssembly>`

## Current Configuration Summary

### Default Settings
- Window: 1920x1080 at (0,0), Normal state
- LastUrl: `https://yoyokits.github.io/VideoBrowser/welcome.html`
- MaxSimDownloads: 5
- ShowMaxSimDownloads: false

### Dependencies (Notable Versions)
- CefSharp.Wpf: 99.2.120
- MahApps.Metro: 1.6.5.1
- Dragablz (unversioned in .csproj, DLL in Binaries)
- log4net (unversioned, DLL in Binaries)
- Newtonsoft.Json: 6.0.0.0 (unified)
- Microsoft.Windows.SDK.Contracts: 10.0.19041.1
- System.Runtime.WindowsRuntime: 4.7.0

### Build Outputs
- Debug: `bin\Debug\`
- Release: `bin\Release\`
- x64: `bin\x64\Debug\`, `bin\x64\Release\`

## Known Issues & TODOs

### Open Issue #15
- "Add In Buttons can Open only in First Window" (assigned to yoyokits, 2020-07-14)
- Affects multi-window support when using add-in buttons

### Potential Improvements
- Upgrade CefSharp to latest stable (check compatibility)
- Replace youtube-dl with yt-dlp (modern fork, more active)
- Consider moving to .NET 6/8 (WPF on .NET Core) for future-proofing
- Add unit tests for ViewModels (currently sparse)
- Implement async/await for youtube-dl operations (currently Task.Run)

## Development Tips

### Debugging
- Attach debugger to `VideoBrowser.exe` in `bin\Debug\`
- Use `Log4Net` configuration (check `App.config` for log4net section)
- `Helpers/DebugHelper.cs` contains debugging utilities

### Working with XAML
- Build before editing XAML to generate `*.g.cs` files for IntelliSense
- MahApps resource dictionaries: merge in `App.xaml`
- CefSharp namespace: `xmlns:cef="clr-namespace:CefSharp.Wpf;assembly=CefSharp.Wpf"`

### Adding New Dependencies
- Use NuGet Package Manager or edit `.csproj` with `<PackageReference>`
- For native DLLs, place in `Binaries/` and embed as `EmbeddedResource`
- Update `MEMORY.md` when adding significant dependencies

### Testing Download Features
- Ensure `youtube-dl.exe` and `ffmpeg.exe` are present in `Binaries/`
- Test with simple YouTube URLs first
- Check `log4net` output (logs to file or console depending on config)

## External Resources
- GitHub: https://github.com/yoyokits/VideoBrowser
- Website: https://yoyokits.github.io/VideoBrowser/
- youtube-dl docs: https://github.com/ytdl-org/youtube-dl
- CefSharp docs: https://github.com/cefsharp/CefSharp
- MahApps: https://mahapps.com/
- Dragablz: https://github.com/ButchersBoy/Dragablz

## Git Information
- Remote: `origin` → `https://github.com/yoyokits/VideoBrowser.git`
- Branches: `master` (stable), `development`, and 2 others
- Last Activity: Recent commits on master (2022: e68aaeb "Set theme jekyll-theme-slate" on docs)
- Note: Gemfile security updates were applied in 2026 (this year)

---

**Maintained by:** yoyokits
**Last Updated:** 2026-04-03
**Memory Version:** 1.0