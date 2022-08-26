using Microsoft.AspNetCore.Mvc;
using Udemy.Services.Catalog.Services;

namespace Udemy.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : Controller
    {
        private readonly ICourseService _courseService;
        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [HttpGet("{id}")]
        
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _courseService.GetByIdAsync(id);
            if (response.StatusCode==404)
            {
                return NotFound(response.Errors);
            }
            return View(response);
        }
    }
}
