using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using Microsoft.WindowsAPICodePack.Shell;
using System.Reflection;

namespace GetVideoDuration.web
{
    public static class Getduration
    {
        public static string videoduration(string path)
        {
            string result;
            try
            {
                ShellObject shell = ShellObject.FromParsingName(path);
                IShellProperty prop = shell.Properties.System.Media.Duration;
                string duration = prop.FormatForDisplay(PropertyDescriptionFormatOptions.None);
                result = duration;
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
    }
}
