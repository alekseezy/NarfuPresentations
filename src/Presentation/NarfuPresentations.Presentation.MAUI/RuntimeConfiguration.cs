namespace NarfuPresentations.Presentation.MAUI;

public static class RuntimeConfiguration
{
    public static bool IsDesktop
    {
        get
        {
#if WINDOWS || MACCATALYST
            return true;
#else
            return false;
#endif
        }
    }
}
