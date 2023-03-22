using kiemtraVoDuyet.Model;
using kiemtraVoDuyet.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace kiemtraVoDuyet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
       
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProductController( ILogger<ProductController> logger, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;

            _hostingEnvironment = hostEnvironment;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                return NoContent(); // 204 No Content
            }
            long maxFileSize = 20 * 1024 * 1024; // 20MB
            var folderPath = Path.Combine(_hostingEnvironment.WebRootPath, "images");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            foreach (var file in files)
            {
                if (file.Length > maxFileSize)
                {
                    return UnprocessableEntity("Kích thước file vượt quá giới hạn cho phép"); // 422 Unprocessable Entity
                }
                var fileExtension = Path.GetExtension(file.FileName).ToLower();
                if (fileExtension != ".jpg" && fileExtension != ".png")
                {
                    return UnprocessableEntity("Định dạng file không hợp lệ"); // 422 Unprocessable Entity
                }
                var fileName = $"{Guid.NewGuid()}{fileExtension}";
                var fullPath = Path.Combine(folderPath, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            return Ok(new { filePath = folderPath });
        }

      
        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Get list data");
            var db = new ContextDB();
            var service = new ProductService(db);
            var ProductList = service.GetAll();
            return Ok(ProductList);
        }
        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            _logger.LogInformation($"Get data Id = {id}");
            var db = new ContextDB();
            var service = new ProductService(db);
            var ProductList = service.GetAll();
            var Product = ProductList.FirstOrDefault(e => e.Id == id);
            if (Product != null)
            {
                return Ok(Product);
            }
            return NotFound();
        }

        // POST api/<ProductController>

        public class ProductCreateDro
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string? Code { get; set; }
            public double Price { get; set; }
            public bool? Continue { get; set; }
            public int CategoryId { get; set; }
        }
        [HttpPost]
        public IActionResult Post([FromBody] ProductCreateDro Product)
        {
            var db = new ContextDB();
            var service = new ProductService(db);

            Product product = new Product
            {
                Id = Product.Id,
                Name = Product.Name,
                Code = Product.Code,
                Price = Product.Price,
                Continue = Product.Continue,
                CategoryId = Product.CategoryId,
            };

            var rs = service.Add(product);
            if (rs)
                return Ok("Da them doi tuong thanh cong");
            else
                return BadRequest("Trung ma hoat trung ten");
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product Product)
        {
            if (id == Product.Id)
            {
                var db = new ContextDB();
                var service = new ProductService(db);
                var rs = service.Update(Product);
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

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
