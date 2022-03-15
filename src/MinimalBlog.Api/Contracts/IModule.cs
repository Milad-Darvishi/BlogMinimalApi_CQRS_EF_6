namespace MinimalBlog.Api.Contracts;

public interface IModule
{
    IEndpointRouteBuilder RegisterEndpoints(IEndpointRouteBuilder endpoints);
}