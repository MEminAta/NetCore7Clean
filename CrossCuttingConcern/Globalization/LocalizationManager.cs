using System.Globalization;
using System.Resources;

namespace CrossCuttingConcern.Globalization;

public static class LocalizationManager
{
    private static readonly ResourceManager ResourceManager = new("CrossCuttingConcern.Globalization.Languages.Resources", typeof(LocalizationManager).Assembly);

    public static string GetString(string key) => ResourceManager.GetString(key, CultureInfo.CurrentCulture)!;
}