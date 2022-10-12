# ChocoWrapper

[![](https://img.shields.io/github/license/li-zhixin/ChocoWrapper.svg?style=flat-square)](https://github.com/li-zhixin/ChocoWrapper/blob/main/LICENSE)
[![](https://img.shields.io/github/commit-activity/y/li-zhixin/ChocoWrapper.svg?style=flat-square)](https://github.com/li-zhixin/ChocoWrapper/commits/master)
[![](https://img.shields.io/github/issues/li-zhixin/ChocoWrapper.svg?style=flat-square)](https://github.com/li-zhixin/ChocoWrapper/issues)

[![Build](https://github.com/li-zhixin/ChocoWrapper/actions/workflows/Build.yml/badge.svg)](https://github.com/li-zhixin/ChocoWrapper/actions/workflows/Build.yml)
[![NuGet](https://img.shields.io/nuget/v/ChocoWrapper.svg?color=blue&style=popout-square)](https://www.nuget.org/packages/ChocoWrapper)
[![NuGet](https://img.shields.io/nuget/dt/ChocoWrapper.svg)](https://www.nuget.org/packages/ChocoWrapper)

A chocolatey wrapper in windows.

## Notice

**Please run as administrator.**

## How to use
```c#
var choco = new ChocoWrapper.ChocoWrapper();
choco.InstallPackage("captura");
choco.InstallPackage("ffmpeg","4.4");
choco.InstallPackage("ffmpeg","4.4", "http://my-host:8624/nuget/private-chocolatey/");
```

## FAQ

1.  Environment variables not refreshed after installing dependencies.

   > A copy of the environment variables is made when the process is started, so any environment variable changes that occur in the middle process are not sensed. For newly started processes, you can pass the environment variables, for existing processes, you need to look them up manually.

   ```c#
   startInfo.EnvironmentVariables["PATH"] = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Machine);
   ```

   