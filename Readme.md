###  Installation 

```
dotnet new install Avalonia.Templates
```

### Prerequisites 

- .NET 8.0
- Semi.Avalonia framework

### Steps 1. Clone the repository:   

```
git clone https://github.com/ailen1002/TechMatrix.git
cd TechMatrix
```

### Steps 2. Build and run the project:   

```
dotnet build
dotnet run
```

### 项目特点

**核心项目**：放置与平台无关的业务逻辑、数据模型、视图模型等。

**平台项目**：实现平台特定的功能，如文件系统、推送通知、设备信息等。

**依赖注入**：使用依赖注入或服务定位器模式将平台特定的实现注入到核心项目中，保持核心代码的跨平台能力。