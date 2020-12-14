# Readme

## VSCode generic debug dotnet
+ I found that this guy [vscode github issue](https://github.com/microsoft/vscode/issues/78316) requested that Microsoft make a `${fileDirBasename}` to the `launch.json` in vscode.  That made it to where this can work really well.
    + It got added to vscode in [v1.52.0](https://github.com/microsoft/vscode/commit/551db7ec94f02a4bdc8999092cf8bef642b3992d)
+ I make a new folder, then use the `dotnet new console` to start the new project.  Then you can do whatever you need to do.