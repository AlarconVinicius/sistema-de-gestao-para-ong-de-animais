var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.SGONGA_WebAPI_API>("api");

builder.Build().Run();
