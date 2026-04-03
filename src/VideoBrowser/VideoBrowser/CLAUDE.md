# CLAUDE.md - VideoBrowser Project Guidelines

## Project Overview
**VideoBrowser** (Cekli Browser) - WPF .NET Framework 4.8 desktop application combining web browsing (CefSharp) with YouTube video downloading (youtube-dl).

**Technology Stack:**
- .NET Framework 4.8 (Windows-only)
- WPF with XAML
- MVVM pattern (ViewModel base: `NotifyPropertyChanged`, `RelayCommand`)
- CefSharp.Wpf 99.2.120 (Chromium embedding)
- MahApps.Metro 1.6.5.1 (UI styling)
- Dragablz (tabbed interface)
- log4net (logging)
- Embedded binaries: youtube-dl.exe, ffmpeg.exe

**Architecture:**
- `ViewModels/` - MVVM view models (MainWindowViewModel is central)
- `Views/` - XAML views
- `Controls/CefSharpBrowser/` - Custom browser control with integrated video features
- `Core/` - Business logic (download queue, youtube-dl wrapper, URL handlers)
- `Models/` - Data models
- `Helpers/` - Utility classes (FFmpeg, Process, File helpers)
- `Common/` - Shared utilities (RelayCommand, NotifyPropertyChanged, extensions)

## Coding Conventions

### C# Style
- PascalCase for public members, classes, methods
- camelCase for private fields and parameters
- XML documentation comments on public classes/methods
- 4-space indentation
- `var` keyword acceptable when type is obvious

### MVVM Best Practices
- ViewModels inherit from `NotifyPropertyChanged`
- Use `RelayCommand` for ICommand implementations
- Keep code-behind minimal; put logic in ViewModels
- Bindings use `INotifyPropertyChanged` to notify UI updates
- Central `GlobalBrowserData` holds shared state across windows

### Naming Conventions
- ViewModels: `*ViewModel.cs`
- Views: `*View.xaml` / `*View.xaml.cs`
- Models: `*Model.cs`
- Controls: `*Control.xaml` or custom folders
- Interfaces: `I*` prefix (e.g., `IUrlHandler`)

## Key Patterns

### RelayCommand
```csharp
public ICommand MyCommand { get; }
MyCommand = new RelayCommand(ExecuteMethod, CanExecuteMethod);
```

### Property Change Notification
```csharp
private string _myProperty;
public string MyProperty
{
    get => _myProperty;
    set
    {
        if (_myProperty != value)
        {
            _myProperty = value;
            OnPropertyChanged(nameof(MyProperty));
        }
    }
}
```

### Download Queue Pattern
- `DownloadQueueHandler` manages concurrent downloads
- `Operation` represents a single download operation
- `FileDownloader` handles actual download process
- `GlobalBrowserData.DownloadItemModels` holds observable collection

## Build & Debug

### Prerequisites
- Visual Studio 2019/2022 (or MSBuild)
- .NET Framework 4.8 SDK
- Windows (WSL cannot run WPF UI)

### Build
- Open `VideoBrowser.sln` in Visual Studio
- Build configurations: `Debug` | `Release` | `Debug|x64` | `Release|x64`
- Output: `bin\Debug\` or `bin\Release\`

### Signing
- Assembly is strong-name signed with `key.snk`
- Keep `key.snk` secure; do not commit private keys

### Binaries Folder
- `Binaries/` contains native dependencies: `ffmpeg.exe`, `youtube-dl.exe`
- These are copied to output as EmbeddedResource
- Do not delete or rename these files

## Important Modules

### Youtube-dl Integration (`Core/YoutubeDl.cs`)
- Wraps youtube-dl command-line execution
- Path resolved: `youtube-dl.exe` from Binaries
- `UpdateYoutubeDl()` method auto-updates youtube-dl on startup

### CefSharp Browser (`Controls/CefSharpBrowser/`)
- Custom browser control with tab support
- Lifecycle: `Cef.Initialize()` at startup, `Cef.Shutdown()` on exit
- `DownloadHandler` intercepts downloads
- `SettingsViewModel` configures browser behavior (proxy, cache, etc.)

### Settings
- User settings stored via `Properties.Settings.Default` (user-scoped)
- Settings persist between sessions (window position, size, last URL, etc.)
- Access: `Properties.Settings.Default.<SettingName>`

## Testing
- Test projects: `VideoBrowser.Test`, `VideoBrowserTestApp`
- Use unit tests for ViewModels and Core logic
- UI tests require manual verification or automation frameworks

## Known Issues / Considerations

### Issue #15 (Open Issue)
- "Add In Buttons can Open only in First Window" - bug reported 2020-07-14
- Related to multi-window support with add-in buttons

### Dependencies
- CefSharp version pinned to 99.2.120 (check for updates)
- youtube-dl auto-updates itself on startup (no manual update needed)
- ffmpeg.exe is static build; verify licensing compliance

### Performance
- CefSharp initialization is heavy; avoid multiple browser instances if possible
- Download queue uses limited concurrency (`MaxSimDownloads`)
- Consider using async/await patterns for I/O operations

## Git Workflow
- Branch strategy: `master` (stable), `development` (active)
- Commits: concise, imperative mood
- Reference issues in commit messages when applicable

## Security
- Do not expose API keys or credentials in code
- youtube-dl `--no-check-certificate` flag currently used (consider removing for production)
- User downloads from external sources: ensure youtube-dl/ffmpeg binaries are authentic

## IDE Setup (Claude Code / Codex)
- Build solution before editing XAML (generates `g.cs` files)
- Use MSBuild: `msbuild VideoBrowser.sln /p:Configuration=Debug`
- For IntelliSense: ensure reference assemblies are available (Visual Studio recommended)

## Resources
- CefSharp docs: https://github.com/cefsharp/CefSharp
- MahApps.Metro: https://mahapps.com/
- youtube-dl: https://github.com/ytdl-org/youtube-dl
- Dragablz: https://github.com/ButchersBoy/Dragablz