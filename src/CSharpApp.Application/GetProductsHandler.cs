using MediatR;

namespace CSharpApp.Application;

public class GetProductsHandler : IRequestHandler<GetProductsQuery, string>
{
    public async Task<string> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return "hello there!";
    }
}
