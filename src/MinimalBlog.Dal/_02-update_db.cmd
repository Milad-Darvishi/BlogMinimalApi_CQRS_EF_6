dotnet tool update --global dotnet-ef --version 6.0.2
dotnet build
dotnet ef --startup-project ../MinimalBlog.Api/ database update --context MinimalBlogDbContext
pause