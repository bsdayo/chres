namespace ResolutionChanger;

public static class Program
{
    public static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            WriteError("Incorrect argument count. Example:\n  chres 1920x1080@60");
            return;
        }

        try
        {
            var target = DEVMODEA.Parse(args[0]);
            var mode = new DEVMODEA();
            var id = 0u;
            while (Native.EnumDisplaySettingsA(null, id++, ref mode))
                if (mode == target)
                {
                    Native.ChangeDisplaySettingsA(ref mode, 0);
                    return;
                }

            WriteError("Your display does not support this resolution.");
        }
        catch (Exception e)
        {
            WriteError(e.Message);
        }
    }

    private static void WriteError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Error.WriteLine(message);
        Console.ResetColor();
    }
}