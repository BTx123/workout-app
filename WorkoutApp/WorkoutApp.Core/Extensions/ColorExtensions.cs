using Color = System.Drawing.Color;

namespace WorkoutApp.Core.Extensions;

public static class ColorExtensions
{
    public static string ToArgbString(this Color color)
    {
        return $"#{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}";
    }
}