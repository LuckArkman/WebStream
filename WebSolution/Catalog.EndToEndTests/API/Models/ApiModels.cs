namespace Catalog.EndToEndTests.API.Models;

public class ApiResponse<TData>
{
    public TData Data { get; private set; }

    public ApiResponse(TData data) 
        => Data = data;
}