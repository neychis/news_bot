using Microsoft.AspNetCore.Mvc;

namespace NewsAggregator.Controllers;

[ApiController]
[Route("api/articlelog")]
public class ArticleLogControllers: ControllerBase 
{
    [HttpGet]
    public IActionResult GetArticles(int userId) => throw new NotImplementedException("Implement GetArticles()");
}