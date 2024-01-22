using Microsoft.AspNetCore.Mvc;
using Search.Models;
using Search.Services;
using System.Threading.Tasks;

namespace Search.Controllers
{
    [ApiController]
    [Route("api/search")]

    public class SearchController : ControllerBase
    {
        private readonly ISearchService searchSvc;

        public SearchController(ISearchService searchSvc)
        {
            this.searchSvc = searchSvc;
            
        }

        [HttpPost]
        public async Task<ActionResult> Search(SearchTerm term)
        {
            var result = await searchSvc.SearchAsync(term.CustomerId);

            if(result.IsSuccess)
            {
                return Ok(result.SearchResult);
            }
            else
            {
                return NotFound();
            }

        }

    }
}
