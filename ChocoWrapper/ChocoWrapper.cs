using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Text;
using Instances;

namespace ChocoWrapper
{
    public class ChocoWrapper
    {
        private static bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        static ChocoWrapper()
        {
            var chocoExistPath = FileHelper.ExistInPath("choco");
            if (chocoExistPath)
            {
                return;
            }

            const string chocoInstallScript =
                "Set-ExecutionPolicy Bypass -Scope Process -Force; [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; iex ((New-Object System.Net.WebClient).DownloadString('https://community.chocolatey.org/install.ps1'))";
            ExecutePowerShellCommand(chocoInstallScript);
        }

        public void InstallPackage(string packageName, string version = "", string source = "")
        {
            var versionParam = "--version ";
            var sourceParam = "--source ";
            if (string.IsNullOrEmpty(version))
            {
                versionParam = "";
            }
            else
            {
                versionParam += version;
            }

            if (string.IsNullOrEmpty(source))
            {
                sourceParam = "";
            }
            else
            {
                sourceParam += source;
            }

            ExecutePowerShellCommand($"choco install {packageName} {versionParam} {sourceParam} -y");
        }

        private static void ExecutePowerShellCommand(string command)
        {
            if (!IsAdministrator())
            {
                throw new Exception("Please run this program with administrator.");
            }

            var startInfo = new ProcessStartInfo
            {
                FileName = "powershell",
                Arguments = command,
                StandardOutputEncoding = Encoding.UTF8,
                StandardErrorEncoding = Encoding.UTF8,
                EnvironmentVariables =
                {
                    ["PATH"] = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Machine)
                }
            };
            var instance = new Instance(startInfo);
            instance.DataReceived += (_, receivedData) =>
            {
                var (type, data) = receivedData;
                switch (type)
                {
                    case DataType.Output:
                        Console.WriteLine(data);
                        break;
                    case DataType.Error:
                        Console.Error.WriteLine(data);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            };
            var exitCode = instance.FinishedRunning().Result;
            if (exitCode != 0)
            {
                throw new ChocoWrapperException(ChocoWrapperExceptionType.Process,
                    $"powershell exited with non-zero exit-code ({exitCode} - {string.Join("\n", instance.ErrorData)})",
                    null, string.Join("\n", instance.ErrorData));
            }
        }
    }
}