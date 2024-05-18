// ReSharper disable InconsistentNaming

using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace ResolutionChanger;

[StructLayout(LayoutKind.Explicit)]
public partial record struct DEVMODEA()
{
    [FieldOffset(36)]
    public ushort dmSize = (ushort)Marshal.SizeOf<DEVMODEA>();

    [FieldOffset(108)]
    public uint dmPelsWidth;

    [FieldOffset(112)]
    public uint dmPelsHeight;

    [FieldOffset(120)]
    public uint dmDisplayFrequency;

    public override string ToString()
    {
        return $"{dmPelsWidth}x{dmPelsHeight}@{dmDisplayFrequency}Hz";
    }

    [GeneratedRegex(@"^(\d+)x(\d+)@(\d+)([Hh][Zz])?$")]
    private static partial Regex ParseRegex();

    public static DEVMODEA Parse(string s)
    {
        var match = ParseRegex().Match(s);
        if (!match.Success)
            throw new ArgumentException("Invalid resolution format.");
        return new DEVMODEA
        {
            dmPelsWidth = uint.Parse(match.Groups[1].Value),
            dmPelsHeight = uint.Parse(match.Groups[2].Value),
            dmDisplayFrequency = uint.Parse(match.Groups[3].Value)
        };
    }
}

public static partial class Native
{
    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool EnumDisplaySettingsA(
        [MarshalAs(UnmanagedType.LPStr)] string? lpszDeviceName,
        uint iModeNum,
        ref DEVMODEA lpDevMode
    );

    [LibraryImport("user32.dll")]
    public static partial long ChangeDisplaySettingsA(
        ref DEVMODEA lpDevMode,
        uint dwFlags
    );
}