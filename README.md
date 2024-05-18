# chres

Super tiny display resolution changer for Windows.

## Features

- Change display resolution directly in terminal.
- Tiny executable size (~1.5MB) with fast startup time (thanks to .NET NativeAOT).

## Usage

Download `chres.exe` from [Releases](https://github.com/bsdayo/chres/releases/latest).

```powershell
chres.exe 1920x1080@60     # Set resolution to 1920x1080 at 60Hz
```

## Building

Building chres needs .NET 8 SDK, Visual Studio 2022 including the Desktop development with C++ workload with all default
components.

```powershell
dotnet publish -o publish
# chres.exe will be in publish folder
```

## License

[MIT](LICENSE)
