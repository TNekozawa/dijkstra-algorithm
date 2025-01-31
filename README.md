# ソリューション/プロジェクト生成
```bash
dotnet new sln --name DijkstraAlgorithm

dotnet new classlib -o Models
dotnet new classlib -o Controller
dotnet new wpf -o Viewer

dotnet sln add Models/Models.csproj
dotnet sln add Controller/Controller.csproj
dotnet sln add Viewer/Viewer.csproj

dotnet add Controller/Controller.csproj reference Models/Models.csproj
dotnet add Viewer/Viewer.csproj reference Models/Models.csproj
dotnet add Viewer/Viewer.csproj reference Controller/Controller.csproj
```