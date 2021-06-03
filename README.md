### Table of Contents

- [Getting Started](#getting-started)


# TaskManager

![Project Manager Dashboard](https://user-images.githubusercontent.com/2252884/120699256-c88f8a00-c475-11eb-923c-fc8a5c6c3462.png)

## Getting Started

1. Run the following commands in `Windows PowerShell` to get a copy of this project.

```shell
Invoke-WebRequest -Uri https://github.com/katbdesrosiers/task-manager/archive/refs/heads/main.zip -OutFile TeamMightyDucks.zip;
Expand-Archive TeamMightyDucks.zip -DestinationPath '.\Team Mighty Ducks\';
rm TeamMightyDucks.zip;
explorer.exe '.\Team Mighty Ducks\task-manager-main\TaskManager.sln';
```

2. In Visual Studio, when the [wait cursor](https://en.wikipedia.org/wiki/Windows_wait_cursor) disappears, open the
NuGet Package Manager Console with `Tools -> NuGet Package Manager -> Package Manager Console`

There should be a message at the top of the console pane:

> Some NuGet packages are missing from this solution. Click to restore from your online package sources.

3. Click the `Restore` button on this message to install the missing packages
4. In the console, run the command `Update-Database`
5. Clean the solution with `Build -> Clean Solution`
6. Press `ctrl + F5` to run the project, it should open in your default browser
