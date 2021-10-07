using System;
using System.IO;
using System.Linq;

namespace ChocoWrapper
{
    static class FileHelper
    {
        public static bool ExistInPath(string fileName)
        {
            return !string.IsNullOrEmpty(Where(fileName));
        }
    
        public static string Where(string fileName)
        {
            var paths = Environment.GetEnvironmentVariable("PATH")?.Split(';') ?? Array.Empty<string>();
            string[] extensions;
            if (Path.HasExtension(fileName))
            {
                extensions = Array.Empty<string>();
            }
            else
            {
                extensions = Environment.GetEnvironmentVariable("PATHEXT")?.Split(';') ?? Array.Empty<string>();
            }

            var allDirs = new[] {Environment.CurrentDirectory}.Concat(paths);

            foreach (var dir in allDirs)
            {
                var actualExtension = extensions.FirstOrDefault(extension =>
                    File.Exists(Path.Combine(dir.Trim(), fileName + extension.ToLower())));
                if (actualExtension != null)
                {
                    return Path.Combine(dir.Trim(), fileName + actualExtension.ToLower());
                }
            }
            return "";
        }
    }
}