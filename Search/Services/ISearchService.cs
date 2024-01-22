using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Search.Services
{
    public interface ISearchService
    {
        Task<(bool IsSuccess, dynamic SearchResult)> SearchAsync(int CustomerId);
    }
}
