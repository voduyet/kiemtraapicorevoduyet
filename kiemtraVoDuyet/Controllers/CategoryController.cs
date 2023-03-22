
using kiemtraVoDuyet.Model;
using kiemtraVoDuyet.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFcoer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(ILogger<CategoryController> logger)
        {
            _logger = logger;
        }
        // GET: api/<CategoryController>
        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Lấy danh sách dữ liệu");
            var db = new ContextDB();
            var service = new CategoryService(db);
            var Category = service.GetAll();
            return Ok(Category);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            _logger.LogInformation($"Lấy dữ liệu có ID là {id}");
            var db = new ContextDB();
            var service = new CategoryService(db);
            var categoryList = service.GetAll();
            var category = categoryList.FirstOrDefault(e => e.Id == id);
            if (category != null)
            {
                return Ok(category);
            }
            return NotFound();
        }

        // POST api/<CategoryController>
        [HttpPost]
        public IActionResult Post([FromBody] Category category)
        {
            var db = new ContextDB();
            var service = new CategoryService(db);
            var rs = service.Add(category);
            if (rs)
                return Ok("Đã thêm đối tượng thành công");
            else
                return BadRequest("Trùng mã hoặc trùng tên");
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Category category)
        {
            if (id == category.Id)
            {
                var db = new ContextDB();
                var service = new CategoryService(db);
                var rs = service.Update(category);
                if (rs)
                    return Ok("Đã cập nhật thành công");
                else
                    return BadRequest("Cập nhật thất bại");
            }
            else
            {
                return BadRequest("ID đối tượng không đúng");
            }
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}
