using System;
using System.IO;
using System.Linq;

namespace ChocoWrapper
{
    static class FileHelper
    {
        public static bool ExistInPath(string fileName)
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
            return allDirs.Any(dir =>
            {
                return extensions.Any(extension =>
                    File.Exists(Path.Combine(dir.Trim(), fileName + extension.ToLower())));
            });
        }
    }
}