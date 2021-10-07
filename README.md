# ChocoWrapper

[![](https://img.shields.io/github/license/li-zhixin/ChocoWrapper.svg?style=flat-square)](https://github.com/li-zhixin/ChocoWrapper/blob/main/LICENSE)
[![](https://img.shields.io/github/commit-activity/y/li-zhixin/ChocoWrapper.svg?style=flat-square)](https://github.com/li-zhixin/ChocoWrapper/commits/master)
[![](https://img.shields.io/github/issues/li-zhixin/ChocoWrapper.svg?style=flat-square)](https://github.com/li-zhixin/ChocoWrapper/issues)

A chocolatey wrapper in windows.

## Notice

**Please run as administrator.**

## How to use
```c#
var choco = new ChocoWrapper.ChocoWrapper();
choco.InstallPackage("ffmpeg","4.4");
choco.InstallPackage("captura");
```

